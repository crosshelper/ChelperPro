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

           /* sl.RowDefinitions = new RowDefinitionCollection{
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            };
            //sl.ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
                //new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                //new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) }
            };

            /*sl.Children.Add(new Label
            {
                Text = "Grid",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            }, 0, 3, 0, 1);

            sl.Children.Add(new Label
            {
                Text = "Autosized cell",
                TextColor = Color.White,
                BackgroundColor = Color.Blue
            }, 0, 1);

            sl.Children.Add(new BoxView
            {
                Color = Color.Silver,
                HeightRequest = 0
            }, 1, 1);

            sl.Children.Add(new BoxView
            {
                Color = Color.Teal
            }, 0, 2);

            sl.Children.Add(new Label
            {
                Text = "Leftover space",
                TextColor = Color.Purple,
                BackgroundColor = Color.Aqua,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            }, 1, 2);

            sl.Children.Add(new Label
            {
                Text = "Span two rows (or more if you want)",
                TextColor = Color.Yellow,
                BackgroundColor = Color.Navy,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            }, 2, 3, 1, 3);

            sl.Children.Add(new Label
            {
                Text = "Span 2 columns",
                TextColor = Color.Blue,
                BackgroundColor = Color.Yellow,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 2, 3, 4);

            sl.Children.Add(new Label
            {
                Text = "Fixed 100x100",
                TextColor = Color.Aqua,
                BackgroundColor = Color.Red,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            }, 2, 3);*/


            for (int i =0; i<ts.Count;i++)
            {
                Label lb = new Label
                {
                    Text = ts[i],
                    BackgroundColor = Color.Blue,
                    FontSize = 16
                    //Margin = new Thickness(10),
                };
               // sl.Children.Add(lb);
                //sl.SetRow(lb, 0);
                //sl.SetColume(lb,1);

                //RowDefinitionCollection h = new RowDefinitionCollection();
                //h.Add(lb);
                //sl.RowDefinitions =h;
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
