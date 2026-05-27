using MSDTC.Sample.Api.Interfaces.Services;
using MSDTC.Sample.Api.Utils;
using System.Web.Http;

namespace MSDTC.Sample.Api.Controllers
{
    /// <summary>
    /// Sample controller, to use MSDTC in the service layer and test the distributed transaction.
    /// </summary>
    public class ClientController : ApiController
    {
        public readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// This controller action will call the service layer to add a new client and an audit record in the database,
        /// using MSDTC to ensure both operations are part of the same transaction.
        /// </summary>
        /// <returns>Just the Id and status code 200</returns>
        [HttpGet]
        [FeatureToggle("Feature.CreateUsingMSDTC")]
        public IHttpActionResult CreateUsingMSDTC()
        {
            return Ok(_clientService.AddClient("Test Client API"));
        }
    }
}