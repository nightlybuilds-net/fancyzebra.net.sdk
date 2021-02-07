using fancyzebra.net.sdk.forms;
using Xamarin.Forms;

namespace fancyzebra.net.sdk.sample
{
    public partial class App : Application
    {
        public static IFancyForms FancyPrivacy { get; private set; }
        
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            FancyPrivacy = FancyBuilder
                .New()
                .WithAppId("b7013b4421c94758a606f968baed342f")
                .WithApp(this)
                .Build();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
