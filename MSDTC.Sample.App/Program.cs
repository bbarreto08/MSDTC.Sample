using System;
using System.Data.SqlClient;
using System.Transactions;
using System.Configuration;

namespace MSDTC.Sample.App
{
    internal class Program
    {
        private readonly static string connectionSampleA = ConfigurationManager.ConnectionStrings["connectionSampleA"].ConnectionString;
        private readonly static string connectionSampleB = ConfigurationManager.ConnectionStrings["connectionSampleB"].ConnectionString;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the transaction...");

            try
            {
                ExecuteTransaction();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        private static void ExecuteTransaction()
        {
            using (var scope = new TransactionScope())
            {
                using (var connectionA = new SqlConnection(connectionSampleA))
                {
                    connectionA.Open();
                    var commandA = connectionA.CreateCommand();
                    commandA.CommandText = @"   INSERT INTO Client
                                                VALUES('Client Name');";

                    commandA.ExecuteNonQuery();
                }

                if (FeatureFlags.IsAuditClientQueue)
                {
                    Console.WriteLine("Feature flag 'AuditClientQueue' is enabled.");
                    SendToQueue();
                }
                else
                {
                    using (var connectionB = new SqlConnection(connectionSampleB))
                    {
                        connectionB.Open();
                        var commandB = connectionB.CreateCommand();
                        commandB.CommandText = @"   INSERT INTO AuditClient
                                                    VALUES('Client AuditClient');";

                        commandB.ExecuteNonQuery();
                    }
                }

                scope.Complete();
                Console.WriteLine("Transaction completed successfully.");
            }
        }

        private static void SendToQueue()
        {
            Console.WriteLine("Send to Queue");
        }
    }
}
