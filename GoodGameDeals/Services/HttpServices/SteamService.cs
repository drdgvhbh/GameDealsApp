using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Services.HttpServices {
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.Web.Http;

    using GoodGameDeals.Models.Steam;

    public class SteamService {
        private readonly HttpClient client;

        private Func<string, GetAppListResponse> appListDeserializer;

        public SteamService(
                HttpClient client,
                Func<string, GetAppListResponse> appListDeserializer) {
            this.client = client;
            this.appListDeserializer = appListDeserializer;
        }

        public async Task<GetAppListResponse> AppList() {
            var uriBuilder = new UriBuilder {
                Scheme = "https",
                Host = "api.steampowered.com",
                Path = "ISteamApps/GetAppList/v0002"
            };
            var response = await this.client.GetAsync(uriBuilder.Uri);
            return this.appListDeserializer(response.Content.ToString());
        }

        public async Task<BitmapImage> GameLogo(long gameId) {

            var sb = new StringBuilder();
            sb.AppendFormat("steam/apps/{0}/header.jpg", gameId);
            var uriBuilder = new UriBuilder {
                Scheme = "http",
                Host = "cdn.akamai.steamstatic.com",
                Path = sb.ToString()
            };
            var response = await this.client.GetAsync(uriBuilder.Uri);
            if (response.IsSuccessStatusCode) {
                return new BitmapImage(
                    new Uri(uriBuilder.ToString(), UriKind.Absolute));
            }
            return new BitmapImage(
                new Uri("ms-appx:///Presentation/Assets/NoPreviewAvaliable.png"));
        }
    }
}
