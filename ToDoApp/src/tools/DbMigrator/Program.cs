using System;

namespace DbMigrator
{
    class Program
    {
        static int Main(string connectionString, bool dropAndRebuildDatabase = false)
        {
            connectionString ??= "Server=localhost,1433;Database=ToDoDb-Test;Password=Password#01;Trusted_Connection=false;MultipleActiveResultSets=true";

            var result = Db.Migrate(connectionString, dropAndRebuildDatabase);

            // Display the result
            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.WriteLine("Failed!");

                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

            return 0;
        }
    }
}
