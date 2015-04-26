using System;
using System.IO;
using System.Threading;
using Microsoft.Azure;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using VexTeamNetwork.AutomatedDownloader.Models;

namespace VexTeamNetwork.AutomatedDownloader
{
    public class Download
    {
        static TokenCloudCredentials credentials;
        private static TokenCloudCredentials GetAuthoriationCredentials()
        {
            AuthenticationResult result = null;

            var context = new AuthenticationContext(string.Format(
                Credentials.login,
                Credentials.tenantId));

            var thread = new Thread(() =>
            {
                result = context.AcquireToken(
                    Credentials.apiEndpoint,
                    Credentials.clientId,
                    new Uri(Credentials.redirectUri));
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token.");
            return new TokenCloudCredentials(Credentials.subscriptionId, result.AccessToken);
        }

        public static async void RefreshDatabase(TextWriter log)
        {
            if (credentials == null)
                credentials = GetAuthoriationCredentials();

            var teams = await VexDbDownloader.Download<Team>("http://api.vex.us.nallen.me/get_teams");

            Console.WriteLine(teams);
            Console.ReadKey();
        }
    }
}
