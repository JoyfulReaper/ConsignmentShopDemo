using ConsignmentShopLibrary.DataAccess;
using Microsoft.Extensions.Configuration;

namespace ConsignmentShopLibrary
{
    public interface IConfig
    {
        IConfiguration Configuration { get; }
        IDataAccess Connection { get; }
        DatabaseType DBType { get; }

        public string ConnectionString { get; }
        void Initiliaze();

        public void Initiliaze(DatabaseType type, string connectionString);
    }
}