using MSDTC.Sample.Api.Interfaces.Repositories;
using MSDTC.Sample.Api.Interfaces.Services;
using System.Transactions;

namespace MSDTC.Sample.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAuditClientRepository _auditClientRepository;

        public ClientService(IClientRepository clientRepository, IAuditClientRepository auditClientRepository)
        {
            _clientRepository = clientRepository;
            _auditClientRepository = auditClientRepository;
        }

        public int AddClient(string name)
        {
            int id;
            using (var scope = new TransactionScope())
            {
                var checkClient = _clientRepository.GetClientByName(name);

                if (checkClient != 0)
                    return 0;

                id = _clientRepository.AddClient(name);

                _auditClientRepository.AddAuditClient($"New client added with id: {id}");
                
                scope.Complete();
            }

            return id;
        }
    }
}