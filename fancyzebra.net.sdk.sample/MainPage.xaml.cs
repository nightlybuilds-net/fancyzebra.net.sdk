using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fancyzebra.net.sdk.sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Username.Text))
            {
                await this.DisplayAlert("Warning", "Enter a user name/ID", "OK");
                return;
            }

            await App.FancyPrivacy.EnsureDocumentsForUser(this.Username.Text);
        }
    }
}
