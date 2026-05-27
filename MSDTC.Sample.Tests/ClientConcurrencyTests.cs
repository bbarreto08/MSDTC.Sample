using MSDTC.Sample.Api.DbContext.Repository;
using MSDTC.Sample.Api.Services;
using System.Threading.Tasks;
using Xunit;

namespace MSDTC.Sample.Tests
{
    public class ClientConcurrencyTests
    {
        private readonly string _connectionString = "Server=localhost,1433;Database=SampleA;User Id=sa;Password=sa";

        [Fact]
        [Microsoft.Coyote.SystematicTesting.Test]
        public void ValidateInsertNoDuplication()
        {
            var repository = new ClientRepository(_connectionString);
            
            int result1 = 0, result2 = 0;

            Task t1 = Task.Run(() => result1 = repository.AddClient("Client_Concurrency"));
            Task t2 = Task.Run(() => result2 = repository.AddClient("Client_Concurrency"));

            Task.WaitAll(t1, t2);

            bool inserted = result1 > 0 && result2 > 0;

            Microsoft.Coyote.Specifications.Specification.Assert(!inserted, "Both tasks should not have inserted the same client.");
        }
    }
}
