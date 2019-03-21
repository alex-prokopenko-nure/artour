using Artour.Domain.Constants;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artour.Domain.Dapper.Context
{
    public class DapperDbContext
    {
        private readonly String _connectionString;

        public DapperDbContext()
        {
            _connectionString = ConnectionStrings.DatabaseConnectionString;
        }

        private IDbConnection CreateDBConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task UpdateAsync(String query, Object parameters = null)
        {
            if (String.IsNullOrWhiteSpace(query)) { throw new Exception(); }

            using (var connection = CreateDBConnection())
            {
                try
                {
                    connection.Open();

                    await connection.ExecuteAsync(query, parameters);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<T>> QueryAsync<T>(String query, Object parameters = null)
        {
            if (String.IsNullOrWhiteSpace(query)) { throw new Exception(); }

            using (var connection = CreateDBConnection())
            {
                try
                {
                    connection.Open();

                    var result = await connection.QueryAsync<T>(query, parameters);

                    return result.AsList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<T> CallStoredProcedure<T>(String procedureName, Object parameters)
        {
            if (String.IsNullOrWhiteSpace(procedureName)) { throw new Exception(); }

            using (var connection = CreateDBConnection())
            {
                try
                {
                    connection.Open();

                    var result = await connection.QuerySingleAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public async Task<T> QuerySingleAsync<T>(String query, Object parameters = null)
        {
            if (String.IsNullOrWhiteSpace(query)) { throw new Exception(); }

            using (var connection = CreateDBConnection())
            {
                try
                {
                    connection.Open();

                    var result = await connection.QueryAsync<T>(query, parameters);

                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
