using ConsignmentShopLibrary.DataAccess;
using Microsoft.Extensions.Configuration;

namespace ConsignmentShopLibrary
{
    public interface IConfig
    {
        IConfiguration Configuration { get; }
        IDataAccess Connection { get; }
        DatabaseType DBType { get; }

        string ConnectionString();
        void Initiliaze(DatabaseType db);
    }
}