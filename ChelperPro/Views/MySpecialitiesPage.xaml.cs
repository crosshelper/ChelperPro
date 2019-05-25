using System;
using System.Collections;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class MySpecialitiesPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        void Handle_Save(object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }

       void Tagload(List<String> ts)
        {
            for (int i =0; i<ts.Count;i++)
            {
                Label lb = new Label
                {
                    Text = ts[i],
                    BackgroundColor = Color.Blue,
                    FontSize = 16
                    //Margin = new Thickness(10),
                };
            }
        }
        public MySpecialitiesPage()
        {
            List <String> Taglist = new List<String>()
            {
                "Language",
                "General",
                "etnjrtnrnrtn",
                "wer",
                "dsfsd",
                "bergergergergerg",
                "uwlwl;",
                "cw",
                "Pet",
                "bweij"
            };
            InitializeComponent();
            Tagload(Taglist);
        }
    }
}
