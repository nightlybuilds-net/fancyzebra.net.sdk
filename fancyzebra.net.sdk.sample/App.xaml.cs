using fancyzebra.net.sdk.forms;
using Xamarin.Forms;

namespace fancyzebra.net.sdk.sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            await GracePrivacy
                .Instance
                .WithAppId("b7013b4421c94758a606f968baed342f")
                .WithUserId("mjm1")
                .WithApp(this)
                .Init();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
