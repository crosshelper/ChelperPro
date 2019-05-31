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

            SendBirdClient.Init(ChelperPro.Properties.Resources.APP_ID);
            StripeConfiguration.SetApiKey(ChelperPro.Properties.Resources.PB_KEY);
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
