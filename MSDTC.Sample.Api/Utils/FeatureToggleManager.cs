using MSDTC.Sample.Api.Interfaces.Utils;
using System.Configuration;

namespace MSDTC.Sample.Api.Utils
{
    public class FeatureToggleManager : IFeatureToggleManager
    {
        public bool IsEnabled(string featureName)
        {
            string valor = ConfigurationManager.AppSettings[featureName];
            return bool.TryParse(valor, out bool resultado) && resultado;
        }
    }
}