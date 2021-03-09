using System;
using System.Collections.Generic;
using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Support;

namespace DbMigrator
{
    public class MigrationResult
    {
        public MigrationResult(IEnumerable<SqlScript> scripts, bool successful, Exception error)
        {
            ScriptsThatWereExecuted = scripts;
            Successful = successful;
            Error = error;
        }

        public IEnumerable<SqlScript> ScriptsThatWereExecuted { get; }

        public bool Successful { get; }

        public Exception Error { get; }
    }

    public class Db
    {
        public static MigrationResult Migrate(string connectionString, bool dropAndRebuildDatabase = false)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("'connectionString' must be provided.");
            }

            try
            {
                if (dropAndRebuildDatabase)
                {
                    DropDatabase.For.SqlDatabase(connectionString);
                    EnsureDatabase.For.SqlDatabase(connectionString);
                }

                var upgradeEngineBuilder = DeployChanges.To
                    .SqlDatabase(connectionString, null)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                        x => x.StartsWith("DbMigrator.PreDeployment"),
                        new SqlScriptOptions { ScriptType = ScriptType.RunAlways, RunGroupOrder = 0 })
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                        x => x.StartsWith("DbMigrator.Migrations"),
                        new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 1 })
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                        x => x.StartsWith("DbMigrator.PostDeployment"),
                        new SqlScriptOptions { ScriptType = ScriptType.RunAlways, RunGroupOrder = 2 })
                    .WithTransactionPerScript()
                    .LogToConsole();

                var upgrader = upgradeEngineBuilder.Build();

                Console.WriteLine("Is upgrade required: " + upgrader.IsUpgradeRequired());

                var result = upgrader.PerformUpgrade();

                return new MigrationResult(result.Scripts, result.Successful, result.Error);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " : MigrationResult.Migrate");
            }

            
        }
    }
}