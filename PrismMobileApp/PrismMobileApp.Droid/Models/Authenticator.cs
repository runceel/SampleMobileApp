using Android.Util;
using Microsoft.WindowsAzure.MobileServices;
using PrismMobileApp.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismMobileApp.Droid.Models
{
    public class Authenticator : IAuthenticator
    {
        private MobileServiceClient Client { get; }

        public Authenticator(MobileServiceClient client)
        {
            this.Client = client;
        }

        public async Task<bool> AuthenticateAsync()
        {
            try
            {
                var user = await this.Client.LoginAsync(Forms.Context, MobileServiceAuthenticationProvider.Twitter);
                return user != null;
            }
            catch (Exception ex)
            {
                Log.Debug(nameof(Authenticator), ex.ToString());
                return false;
            }
        }
    }
}