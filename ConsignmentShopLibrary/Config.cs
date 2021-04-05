using ConsignmentShopLibrary.DataAccess;
using Microsoft.Extensions.Configuration;
using System;

namespace ConsignmentShopLibrary
{
    public partial class Config : IConfig
    {

        public IDataAccess Connection { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public DatabaseType DBType { get; private set; }

        public Config()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
        }

        public void Initiliaze(DatabaseType db)
        {
            // Set database type
            if (db == DatabaseType.MSSQL)
            {
                SqlDb sql = new SqlDb(this);
                Connection = sql;
                DBType = db;
            }
            else
            {
                throw new ArgumentException("Invalid Database", nameof(db));
            }
        }

        public string ConnectionString()
        {
            if (DBType == DatabaseType.MSSQL)
            {
                return Configuration.GetConnectionString("MSSQL");
            }

            throw new InvalidOperationException("DBType is not valid");
        }
    }
}
