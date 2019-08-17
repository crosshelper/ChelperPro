using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Transfer;
using ChelperPro.Helpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using WebSocketSharp;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignUpTwoPage : ContentPage
    {
        public SignUpTwoPage()
        {
            InitializeComponent();
            SetupAWSCredentials();
        }
        void Handle_Canceled(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        //返回按钮 Go Back
        void SUPGoBack(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        UserAccess uac = new UserAccess();

        /* private bool CheckValid()
         {
             bool IsValid = true && !(EmailEntry.Text.IsNullOrEmpty());
             IsValid &= NoEntry.Text.Length >= 10;
             IsValid &= !(FLEntry.Text.IsNullOrEmpty());
             IsValid &= !(SLEntry.Text.IsNullOrEmpty());
             IsValid &= SocialEntry.Text.Length == 9;
             if (AddressEntry.Text.IsNullOrEmpty())
             {
                 IsValid = false;
             }
             return IsValid;
             
    }*/

    private string filename = Settings.ChatID + "_ProfileIcon.png";// + DateTime.Now.ToShortDateString();

        public TransferUtility s3transferUtility;
        IAmazonS3 s3client;


        async void Handle_Upload(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Upload ID", "Cencel", null, "Take photo", "From album");
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

        ////check Permisson method
        private async Task<bool> CheckPermisson()
        {
            var permissons = CrossPermissions.Current;
            var storageStatus = await permissons.CheckPermissionStatusAsync(Permission.Storage);
            if (storageStatus != PermissionStatus.Granted)
            {
                var results = await permissons.RequestPermissionsAsync(Permission.Storage);
                storageStatus = results[Permission.Storage];
            }
            return storageStatus == PermissionStatus.Granted;
        }

        private async Task TakePhotoFromCameraAsync()
        {
            var media = CrossMedia.Current;

            //check Permisson
            if (await CheckPermisson())
            {
                if (!media.IsCameraAvailable || !media.IsTakePhotoSupported)
                {
                    await DisplayAlert("Alert", "Can not access Camera", "OK");
                    return;
                }
                var file = await media.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    AllowCropping = true,
                    SaveToAlbum = true
                });
                //fileImage.Source = ImageSource.FromStream(() => { return file.GetStream(); });
                if (file == null)
                {
                    return;
                }
                else
                {
                    try
                    {
                        TransferUtilityUploadRequest request =
                            new TransferUtilityUploadRequest
                            {
                                BucketName = "imagetest123bibi",
                                FilePath = file.Path,
                                Key = string.Format(filename),
                                ContentType = "image/png"
                            };
                        //The cancellationToken is not used within this example, however you can pass it to the UploadAsync consutructor as well
                        //CancellationToken cancellationToken = new CancellationToken();
                        await this.s3transferUtility.UploadAsync(request).ContinueWith(((x) =>
                        {
                            Debug.WriteLine("Image Uploaded");
                        }));
                        await Task.Delay(5000);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
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
                        Key = string.Format(filename),
                        ContentType = "image/png"
                    };
                //The cancellationToken is not used within this example, however you can pass it to the UploadAsync consutructor as well
                //CancellationToken cancellationToken = new CancellationToken();
                await this.s3transferUtility.UploadAsync(request).ContinueWith(((x) =>
                {
                    Debug.WriteLine("Image Uploaded");
                }));
                await Task.Delay(5000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


       void Handle_CreateAccount(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpThreePage());
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
    }
}
