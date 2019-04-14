using System;
namespace ChelperPro.Models
{
    public class PickNewcomerViewcellItem
    {
        public PickNewcomerViewcellItem()
        {
        }
        public int PanelID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Language { get; set; }
        public string Baseprice { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string ProblemdDscription { get; set; }

        public override string ToString()
        {
            return Language;
        }
    }
}
