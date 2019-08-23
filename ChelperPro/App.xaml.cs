using ChelperPro.Views;
using SendBird;
using Plugin.Multilingual;
using Stripe;
using Xamarin.Forms.Xaml;
using ChelperPro;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ChelperPro
{
    public partial class App : Xamarin.Forms.Application
    {

        public App()
        {
            InitializeComponent();
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            StripeConfiguration.SetApiKey("sk_live_XXXXXXXXXXXXXXX");
            SendBirdClient.Init(ChelperPro.Properties.Resources.APP_ID);
            MainPage = new MySplashScreen();
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            SendBirdClient.Init(ChelperPro.Properties.Resources.APP_ID);
            //StripeConfiguration.SetApiKey(ChelperPro.Properties.Resources.PB_KEY);
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
