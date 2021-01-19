using ConsultApp.ViewModels;
using ConsultApp.Views;
using ConsultApp.Helpers;
using Xamarin.Forms;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using ConsultApp.API;
using ConsultApp.API.Intefaces;
using System.Text;
using System.Security.Cryptography;
using System;
using FFImageLoading;
using System.Net.Http.Headers;
using FFImageLoading.Config;
using Xamarin.Essentials;
using ConsultApp.Dialogs.Views;
using ConsultApp.Dialogs.ViewModels;
using Prism.Services.Dialogs;

[assembly: ExportFont("Roboto-Bold.ttf", Alias = "Bold")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Regular")]
[assembly: ExportFont("Font Awesome 5 Brands-Regular-400.otf", Alias = "fa-brand")]
[assembly: ExportFont("Font Awesome 5 Free-Regular-400.otf", Alias = "fa-reg")]
[assembly: ExportFont("Font Awesome 5 Free-Solid-900.otf", Alias = "fa-solid")]


namespace ConsultApp
{
    public partial class App
    {
        public static readonly HttpClient httpClient = new HttpClient();

        public static Location CurrentLcoation = new Location();

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc5NTU0QDMxMzgyZTM0MmUzMENkT0k5UnNDeHdPM1MzcFZSZVRYQWZCNDR4YnBmeW9wbktXNmNOODFlQTA9");

            await NavigationService.NavigateAsync("CustomNavigationPage/HomePage");
            await GetToken();
            await GetLocation();

            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            ImageService.Instance.Initialize(new Configuration
            {
                HttpClient = httpClient,
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<CustomNavigationPage>();

            containerRegistry.RegisterDialog<LocationError, LocationErrorViewModel>();

            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<ConsultPage, ConsultPageViewModel>();
            containerRegistry.RegisterForNavigation<DiagnosisPage, DiagnosisPageViewModel>();
            containerRegistry.RegisterForNavigation<DoctorsPage, DoctorsPageViewModel>();
        }

        private async Task GetToken()
        {
            var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(CalculateHMACMD5())
            };
            var authApi = RestService.For<IAuthApi>(APIConfig.AuthApi, refitSettings);
            var response = await authApi.GetToken();
            APIConfig.Token = response.Token;
        }

        private string CalculateHMACMD5()
        {
            string uri = APIConfig.AuthApi + "/login";
            string api_key = APIConfig.Username;
            string secret_key = APIConfig.SecretKey;
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret_key);
            string computedHashString = "";
            using (var hmac = new HMACMD5(secretBytes))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(uri);
                byte[] computedHash = hmac.ComputeHash(dataBytes);
                computedHashString = Convert.ToBase64String(computedHash);
            }

            return string.Concat(api_key, ":", computedHashString);
        }

        private async Task GetLocation()
        {
            try
            {
                CurrentLcoation = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.High,
                    Timeout = TimeSpan.FromSeconds(10)
                });
            }
            catch (Exception)
            {
            }
        }
    }
}
