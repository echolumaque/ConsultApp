using ConsultApp.ViewModels;
using ConsultApp.Views;
using ConsultApp.Helpers;
using Xamarin.Forms;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using System.Net.Http;
using FFImageLoading;
using System.Net.Http.Headers;
using FFImageLoading.Config;
using Xamarin.Essentials;

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

        public static Location CurrentLocation = new Location();

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzg3MDkwQDMxMzgyZTM0MmUzMEdWa2p2MG1GbXJRbGJrQjVicUJUc2FIL2wrOUFzbWU2OE1DMENIR3FZdDg9");

            await NavigationService.NavigateAsync("CustomNavigationPage/HomePage");

            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            ImageService.Instance.Initialize(new Configuration
            {
                HttpClient = httpClient,
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //base pages
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<CustomNavigationPage>();

            //views
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<ConsultPage, ConsultPageViewModel>();
            containerRegistry.RegisterForNavigation<DiagnosisPage, DiagnosisPageViewModel>();
            containerRegistry.RegisterForNavigation<DoctorsPage, DoctorsPageViewModel>();
            containerRegistry.RegisterForNavigation<SymptomsAndDiseaseInfo, SymptomsAndDiseaseInfoViewModel>("SymptomsInfo");
            containerRegistry.RegisterForNavigation<DoctorsAvailability, DoctorsAvailabilityViewModel>();
        }
    }
}
