using fancyzebra.net.sdk.core.Dtos;
using fancyzebra.net.sdk.core.Services;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace fancyzebra.net.sdk.forms.Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FancyAcceptPage
    {
        public FancyAcceptPage(NavigationProxy navigationProxy, IPrivacyService privacyService,
            IStringLocalizer stringLocalizer, DocumentToAcceptDto[] documentToAcceptDtos)
        {
            this.InitializeComponent();
            this.BindingContext = new FancyAcceptPageViewModel(navigationProxy, this, privacyService, stringLocalizer, documentToAcceptDtos);
        }
    }
}