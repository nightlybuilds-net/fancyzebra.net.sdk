using System.Globalization;
using System.Threading.Tasks;
using fancyzebra.net.sdk.core.Services;
using Xamarin.Forms;

namespace fancyzebra.net.sdk.forms
{
    public interface IGracePrivacyBuilder
    {
        IGracePrivacyBuilder WithAppId(string id);
        IGracePrivacyBuilder WithUserId(string id);
        IGracePrivacyBuilder WithCulture(CultureInfo cultureInfo);
        IGracePrivacyBuilder WithViewDetails(ViewDetails details);
        IGracePrivacyBuilder WithApp(Application app);
        IGracePrivacyBuilder WithIStringLocalizer(IStringLocalizer stringLocalizer);

        Task Init();
    }

    public class ViewDetails
    {
        public double ClauseFontSize { get; set; }
        public double DocumentFontSize { get; set; }
        public Color ClauseFontColor { get; set; }
        public Color DocumentFontColor { get; set; }
    }
}