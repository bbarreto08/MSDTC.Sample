namespace MSDTC.Sample.Api.Interfaces.Utils
{
    public interface IFeatureToggleManager
    {
        bool IsEnabled(string featureName);
    }
}