using System;

namespace DbMigrator
{
    class Program
    {
        static int Main(string connectionString, bool dropAndRebuildDatabase = false)
        {
            if (string.IsNullOrEmpty(connectionString)){
                throw new ArgumentException("'connectionString' must be provided.");
            }
         

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
