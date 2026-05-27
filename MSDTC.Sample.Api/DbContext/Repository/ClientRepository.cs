using MSDTC.Sample.Api.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace MSDTC.Sample.Api.DbContext.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddClient(string name)
        {
            int newId = 0;

            using (var connectionA = new SqlConnection(_connectionString))
            {
                connectionA.Open();
                var commandA = connectionA.CreateCommand();
                commandA.CommandText = $@"  INSERT INTO Client
                                            VALUES(@message);
                                            
                                            SELECT SCOPE_IDENTITY();
                                        ";

                commandA.Parameters.AddWithValue("@message", name);

                try
                {
                    newId = Convert.ToInt32(commandA.ExecuteScalar());
                }
                catch (Exception)
                {
                    return 0;
                }
                
            }

            return newId;
        }

        public int GetClientByName(string name)
        {
            int clientId = 0;

            using (var connectionA = new SqlConnection(_connectionString))
            {
                connectionA.Open();
                var commandA = connectionA.CreateCommand();
                commandA.CommandText = $@"  SELECT *
                                            FROM Client c
                                            WHERE c.Name = @name
                                        ";

                commandA.Parameters.AddWithValue("@name", name);

                var result = commandA.ExecuteReader();

                if (result.Read())
                {
                    clientId = Convert.ToInt32(result["ClientId"]);
                }
            }

            return clientId;
        }
    }
}