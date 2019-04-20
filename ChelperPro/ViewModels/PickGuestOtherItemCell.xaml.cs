using System;
using System.Collections.Generic;
using System.Linq;
using ChelperPro.Models;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChelperPro.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickGuestOtherItemCell : ViewCell
    {
        public IList<PickNewcomerViewcellItem> PanelContent { get; set; }
        public PickGuestOtherItemCell()
        {
            InitializeComponent();
            PanelContent = new List<PickNewcomerViewcellItem>();

            PanelContent.Add(new PickNewcomerViewcellItem
            {
                PanelID = 1,
                Name = "Foothill center",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ProblemdDscription = "I love youI love youI love youI love youI love youI love youI love youI love youI love you love youI love youlove youI love you love you I love you love you",
                ImageUrl = "https://s3-us-west-1.amazonaws.com/image.cycbis.com/recommendation/recom001.png"
            });
            PanelContent.Add(new PickNewcomerViewcellItem
            {
                PanelID = 0,
                Name = "Foothill center",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ProblemdDscription = "I love youI love youI love youI love youI love youI love youI love youI love youI love you love youI love youlove youI love you love you I love you love you",
                ImageUrl = "https://s3-us-west-1.amazonaws.com/image.cycbis.com/recommendation/recom001.png"
            });
            PanelContent.Add(new PickNewcomerViewcellItem
            {
                PanelID = 0,
                Name = "Foothill center",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ProblemdDscription = "I love youI love youI love youI love youI love youI love youI love youI love youI love you love youI love youlove youI love you love you I love you love you",
                ImageUrl = "https://s3-us-west-1.amazonaws.com/image.cycbis.com/recommendation/recom001.png"
            });

            //listView.ItemsSource = PanelContent;
            //listView.BackgroundColor = Color.Transparent;
            //listView.SelectionMode = ListViewSelectionMode.None;
        
    }
    }
}
