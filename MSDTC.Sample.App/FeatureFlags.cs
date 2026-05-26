using System;
using System.Configuration;

namespace MSDTC.Sample.App
{
    public static class FeatureFlags
    {
        public static bool IsAuditClientQueue
        {
            get 
            {
                string value = ConfigurationManager.AppSettings["Feature.AuditClientQueue"];
                return bool.TryParse(value, out bool result) && result;
            }
        }
    }
}
