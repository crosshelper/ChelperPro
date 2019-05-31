using ChelperPro.Views;
using Stripe;
using SendBird;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ChelperPro
{
    public partial class App : Xamarin.Forms.Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new SignInPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            SendBirdClient.Init("B0F51364-A66F-4EF8-B6AA-0A61629E45C7");//ChelperPro.Properties.Resources.APP_ID);
            StripeConfiguration.SetApiKey("sk_live_XXXXXXXXXXXXXXX");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
