using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using Plugin.Messaging;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class AboutUsPage : ContentPage
    {
        string supportsEmail = "bo.chen@cycbis.com";
        string emailTitle = "[Supports Ticket] Feedback from Customer: " + Settings.ChatID;

        async void Handle_Submit(object sender, System.EventArgs e)
        {
            try
            {
                var emailTask = CrossMessaging.Current.EmailMessenger;
                if (emailTask.CanSendEmail)
                {
                    var emailBody = emaileditor.Text;
                    emailTask.SendEmail(supportsEmail, emailTitle, emailBody);

                    /*var email = new EmailMessageBuilder()
                          .To("william.liang@cycbis.com")
                          .Cc("william.liang@cycbis.com")
                          .Bcc(new[] { "william.liang@cycbis.com", "william.liang@cycbis.com" })
                          .Subject("Xamarin Messaging Plugin")
                          .Body("Well hello there from Xam.Messaging.Plugin")
                          .Build();
                    emailTask.SendEmail(email);*/
                }

                else
                {
                    await DisplayAlert("Error", "This device can't send emails", "OK");
                }
            }
            catch
            {
                await DisplayAlert("Error", "Unable to perform action", "OK");
            }
            //Navigation.PopAsync(false);
        }
        public AboutUsPage()
        {
            InitializeComponent();
        }
    }
}
