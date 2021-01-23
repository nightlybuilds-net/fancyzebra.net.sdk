using fancyzebra.net.sdk.core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace fancyzebra.net.sdk.forms.Xaml
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GracePrivacyPage
    {
        public GracePrivacyPage(NavigationProxy navigationProxy, IPrivacyService privacyService,
            IStringLocalizer stringLocalizer)
        {
            this.InitializeComponent();
            this.BindingContext = new GracePrivacyViewModel(navigationProxy, this, privacyService, stringLocalizer);
        }
    }
}