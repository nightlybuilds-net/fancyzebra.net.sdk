using fancyzebra.net.sdk.core.Services;

namespace fancyzebra.net.sdk.core
{
    public class FancyZebra
    {
        public static IPrivacyService Create(string appId)
        {
            return new PrivacyService();
        }
    }
}