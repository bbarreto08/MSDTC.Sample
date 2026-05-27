using MSDTC.Sample.Api.Interfaces.Services;
using System.Web.Http;

namespace MSDTC.Sample.Api.Controllers
{
    public class ClientController : ApiController
    {
        public readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IHttpActionResult GetClients()
        {
            return Ok(_clientService.AddClient("Test Client API"));
        }
    }
}