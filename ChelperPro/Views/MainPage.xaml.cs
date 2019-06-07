using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChelperPro.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            this.SelectedTabColor = Color.FromHex("#FF4E18");
            InitializeComponent();
        }
    }
}