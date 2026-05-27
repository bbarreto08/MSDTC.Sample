using MSDTC.Sample.Api.Interfaces.Repositories;
using System;
using System.Data.SqlClient;

namespace MSDTC.Sample.Api.DbContext.Repository
{
    public class AuditClientRepository : IAuditClientRepository
    {
        public readonly string _connectionString;

        public AuditClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddAuditClient(string message)
        {
            int newId = 0;

            using (var connectionA = new SqlConnection(_connectionString))
            {
                connectionA.Open();
                var commandA = connectionA.CreateCommand();
                commandA.CommandText = $@"  INSERT INTO AuditClient
                                            VALUES(@message);
                                            
                                            SELECT SCOPE_IDENTITY();
                                        ";

                commandA.Parameters.AddWithValue("@message", message);

                newId = Convert.ToInt32(commandA.ExecuteScalar());
            }

            return newId;
        }
    }
}