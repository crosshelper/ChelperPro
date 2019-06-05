using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Transfer;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Plugin.Media;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class AccountPage : ContentPage
    {

        public TransferUtility s3transferUtility;
        IAmazonS3 s3client;

        UserInfo _usr;
        Uac _ac;
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        UserInfoHelper uih = new UserInfoHelper();
        UserSettingHelper ush = new UserSettingHelper();
        
        public AccountPage(UserInfo currentUser)
        {
            _usr = currentUser;
            InitializeComponent();
            NameCell.Title = _usr.FirstName + " " + _usr.LastName;
            NameCell.IconSource = _usr.Icon;
            FirstName = _usr.FirstName;
            LastName = _usr.LastName;
            _ac = ush.GetUacByID(_usr.UserID);
            Email = _ac.Email;
            PhoneNumber = _ac.ContactNo;
            BindingContext = this;
            SetupAWSCredentials();
        }

        private void SetupAWSCredentials()
        {
            // 初始化 Amazon Cognito 凭证提供程序
            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                "us-east-1:be56bffa-67eb-40f0-b7cf-18caf9df0a20", // 身份池 ID
                RegionEndpoint.USEast1 // 区域
            );

            this.s3client = new AmazonS3Client(credentials, RegionEndpoint.USEast1);//new AmazonS3Client("your awsAccessKeyId", "your awsSecretKeyId", RegionEndpoint.USEast1);
            var config = new AmazonS3Config() { RegionEndpoint = Amazon.RegionEndpoint.USEast1, Timeout = TimeSpan.FromSeconds(30), UseHttp = true };

            AWSConfigsS3.UseSignatureVersion4 = true;
            this.s3transferUtility = new TransferUtility(s3client);
        }

        void Handle_Saved(object sender, System.EventArgs e)
        {
            _usr.FirstName = FirstName;
            _usr.LastName = LastName;
            _ac.Email = Email;
            _ac.ContactNo = PhoneNumber;

            uih.UpdateUserInfo(_usr);
            ush.UpdateUac(_ac);

            Navigation.PopAsync(false);
        }

        void Handle_ResetPassword(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ResetPasswordPage(_ac));
        }

        async void Handle_ChangePhoto(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Change the photo", "Cencel", null, "Take photo", "From album");
            switch (action)
            {
                case "Take photo":
                    await TakePhotoFromCameraAsync();
                    break;
                case "From album":
                    await SelectFromImageLibrary();
                    break;
            }
        }

        private Task TakePhotoFromCameraAsync()
        {
            throw new NotImplementedException();
        }

        async Task SelectFromImageLibrary()
        {
            var media = CrossMedia.Current;
            var file = await media.PickPhotoAsync();

            try
            {
                TransferUtilityUploadRequest request =
                    new TransferUtilityUploadRequest
                    {
                        BucketName = "imagetest123bibi",
                        FilePath = file.Path,
                        Key = string.Format("Login Picture"),
                        ContentType = "image/png"
                    };
                await this.s3transferUtility.UploadAsync(request).ContinueWith(((x) =>
                {
                    Debug.WriteLine("Image Uploaded");
                }));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
