using Specify.Configuration;
using System;

namespace Specs.Library.ToDo.Data
{
    public class DatabaseApplicationAction : IPerApplicationAction
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDb _db;

        public DatabaseApplicationAction(IDbFactory dbFactory, IDb db)
        {
            _dbFactory = dbFactory;
            _db = db;
        }

        public void Before()
        {
            var dbNotReady = true;
            var count = 0;
            while (dbNotReady)
            {
                try
                {
                    _dbFactory.CreateDatabase();
                    dbNotReady = false;
                }
                catch (Exception ex)
                {
                    if (++count >= 5)
                    {
                        throw new Exception(ex.Message + " : DatabaseApplicationAction.Before: try to create DB");
                    }
                    System.Threading.Thread.Sleep(10000);
                }
            }
            EntityExtensions.Db = _db;
        }

        public void After()
        {

        }

        public int Order { get; set; } = 3;
    }
}