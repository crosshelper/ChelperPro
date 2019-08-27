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
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Permission = Plugin.Permissions.Abstractions.Permission;

namespace ChelperPro.Views
{
    public partial class AccountPage : ContentPage
    {
        private string filename = Settings.ChatID + "_ProfileIcon.png";// + DateTime.Now.ToShortDateString();

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
            Email = _usr.Email;
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
            _usr.Email = Email;
            _ac.ContactNo = PhoneNumber;
            _usr.Icon = GetIconUrlFromS3();
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

                        NameCell.IconSource = ImageSource.FromStream(() => {
                            var stream = file.GetStream();
                            file.Dispose();
                            return stream;
                        });
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

                NameCell.IconSource = ImageSource.FromStream(() => {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                await Task.Delay(5000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string GetIconUrlFromS3()
        {
            var s3 = Properties.Resources.S3_BUCKETNAME;
            string url = "https://" + s3 + ".s3.amazonaws.com/" + filename;
            return url;
        }

    }
}
