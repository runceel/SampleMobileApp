using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using PrismMobileApp.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using UIKit;

namespace PrismMobileApp.iOS.Models
{
    class Authenticator : IAuthenticator
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
                var user = await this.Client.LoginAsync(
                    UIApplication.SharedApplication.KeyWindow.RootViewController,
                    MobileServiceAuthenticationProvider.Twitter);
                return user != null;
            }
            catch
            {
                return false;
            }
        }
    }
}