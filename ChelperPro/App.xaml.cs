using ChelperPro.Views;
using Stripe;

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
            //SendBirdClient.Init(ChelperPro.Properties.Resources.APP_ID);
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
