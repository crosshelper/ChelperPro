using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChelperPro.Helpers
{
    public class KeyChainHelper
    {
        public KeyChainHelper()
        {
        }
        public async void SavetoSecureStorage(string oauth_token, string secret_value)
            {
                try
                {
                    await SecureStorage.SetAsync(oauth_token, secret_value);
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.
                }
            }

            public async Task<string> GetFromSecureStorage(string oauth_token)
            {
                try
                {
                    var oauthToken = await SecureStorage.GetAsync(oauth_token);
                    return oauthToken;
                }
                catch (Exception ex)
                {
                    return "";
                    // Possible that device doesn't support secure storage on device.
                }
            }

            public void DeleteTheKeyInSecureStorage(string oauth_token)
            {
                SecureStorage.Remove(oauth_token);
            }
        
    }
}

