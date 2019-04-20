using System;
using Xamarin.Forms;
namespace ChelperPro.Models
{
    public class HistorypageViewcellItem
    {
        public HistorypageViewcellItem()
        {
        }
        public int PanelID { get; set; }
        public string ImageUrl { get; set; }
        public string GuestName { get; set; }
        public string Date { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Emergency { get; set; }
        public string ServiceFee { get; set; }
        public string EquipmentFee { get; set; }
        public string CycbisFee { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
        public int Rating { get; set; }

        public override string ToString()
        {
            return Language;
        }
    }
}
