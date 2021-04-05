using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary.DataAccess
{
    public class SqlDb : IDataAccess
    {
        private readonly IConfig _config;

        public SqlDb(IConfig config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(_config.ConnectionString()))
            {
                var rows = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                return rows.ToList();
            }
        }

        public async Task<int> SaveData<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(_config.ConnectionString()))
            {
                return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<T>> QueryRawSQL<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(_config.ConnectionString()))
            {
                var res = await connection.QueryAsync<T>(sql, parameters);
                return res.ToList();
            }
        }

        public async Task<int> ExecuteRawSQL<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(_config.ConnectionString()))
            {
                var res = await connection.ExecuteAsync(sql, parameters);
                return res;
            }
        }
    }
}
