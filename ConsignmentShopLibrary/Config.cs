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

        public void Initiliaze()
        {
            var databaseSetting = Configuration.GetSection("DatabaseType").Value;

            // Set database type
            if (databaseSetting == "MSSQL")
            {
                SqlDb sql = new SqlDb(this);
                Connection = sql;
                DBType = DatabaseType.MSSQL;
            }
            else
            {
                throw new InvalidOperationException($"{databaseSetting} is not a supported database type.");
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
