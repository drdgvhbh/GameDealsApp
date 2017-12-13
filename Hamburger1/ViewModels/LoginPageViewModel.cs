namespace Hamburger1.ViewModels {
    using System;
    using System.Text.RegularExpressions;

    using MetroLog;

    using Windows.Security.Authentication.Web;

    using Hamburger1.Messages;

    using Template10.Mvvm;
    using Template10.Services.SerializationService;

    public class LoginPageViewModel : ViewModelBase {
        /// <summary>
        ///     The log.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<LoginPageViewModel>();

        public bool IsNotAuthenticating { get; private set; }

        public LoginPageViewModel() {
            this.IsNotAuthenticating = true;
        }

        public async void Authenticate() {
            this.IsNotAuthenticating = false;
            var startUri = new UriBuilder {
                Scheme = "https",
                Host = "api.isthereanydeal.com",
                Path = "oauth/authorize",
                Query = "client_id=e2a73869af4edb42"
                        + "&response_type=token"
                        + "&state=Authorization%20code"
                        + "&scope=wait_read%20wait_write"
                        + "&redirect_uri=https://github.com/drdgvhbh"
            };
            var endUri = new Uri("https://github.com/drdgvhbh");
            try {
                var webAuthenticationResult =
                    await WebAuthenticationBroker.AuthenticateAsync(
                        WebAuthenticationOptions.None,
                        startUri.Uri,
                        endUri);
                switch (webAuthenticationResult.ResponseStatus) {
                    case WebAuthenticationStatus.Success: {
                        Log.Debug(webAuthenticationResult.ResponseData);
                        var responseToken = new Uri(
                            webAuthenticationResult.ResponseData)
                                .Fragment.Split('&')[0].Split('=')[1];

                        this.NavigationService.Navigate(
                            typeof(Views.MainPage),
                            SerializationService.Json.Serialize(
                                new AccessToken(responseToken)));
                        break;
                    }

                    case WebAuthenticationStatus.ErrorHttp: {
                        Log.Error(
                            "[Failure] Encountered HTTP error when trying to "
                            + "create user authentication\n {0}",
                            webAuthenticationResult.ResponseErrorDetail);
                        break;
                    }

                    case WebAuthenticationStatus.UserCancel: {
                        break;
                    }

                    default: {
                        break;
                    }
                }
            }
            catch (Exception e) {
                Log.Error("[Failure] Unable to create user authentication", e);
            }
        }
    }
}