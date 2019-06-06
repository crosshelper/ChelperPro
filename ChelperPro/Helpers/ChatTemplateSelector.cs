using ChelperPro.Views.Cells;
using SendBird;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChelperPro.Helpers
{
    class ChatTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is UserMessage messageVm))
                return null;
            return (messageVm.Sender.UserId == Settings.ChatID) ? outgoingDataTemplate : incomingDataTemplate;
        }
    }
}
