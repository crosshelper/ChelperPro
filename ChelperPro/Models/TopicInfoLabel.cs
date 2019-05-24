using System;
namespace ChelperPro.Models
{
    public class TopicInfoLabel
    {
        public int TopicID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Zipcode { get; set; }
        public string Language { get; internal set; }
        public string Description { get; internal set; }
        public string Status { get; internal set; }

        public TopicInfoLabel()
        {
        }
    }
}
