using MSDTC.Sample.Api.Interfaces.Utils;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MSDTC.Sample.Api.Utils
{
    public class FeatureToggleAttribute : AuthorizationFilterAttribute
    {
        private readonly string _featureName;
        public IFeatureToggleManager FeatureManager { get; set; }

        public FeatureToggleAttribute(string featureName)
        {
            _featureName = featureName;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (FeatureManager == null || !FeatureManager.IsEnabled(_featureName))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}