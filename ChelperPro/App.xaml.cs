using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChelperPro.Services;
using ChelperPro.Views;

namespace ChelperPro
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage((Page)Activator.CreateInstance(typeof(SignInPage))) {
            //BarBackgroundColor = Color.FromHex("#FF4E18"),
            //BarTextColor = Color.White };
            //DependencyService.Register<MockDataStore>();
            MainPage = new SignInPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
