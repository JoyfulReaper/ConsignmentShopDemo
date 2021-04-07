using ConsignmentShopLibrary;
using ConsignmentShopLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopTests
{
    public abstract class DBTest
    {
        protected readonly IConfig _config;
        protected readonly string _dbFile;

        protected DBTest(string dbFile)
        {
            _dbFile = dbFile;

            _config = new Config();
            _config.Initiliaze(DatabaseType.SQLite, $"Data Source={dbFile};Version=3;");

            Seed();
        }

        protected abstract void Seed();
    }
}
