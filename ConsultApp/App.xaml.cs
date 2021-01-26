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
using ConsultApp.Database;
using ConsultApp.Helpers.Interfaces;
using SQLite;
using System;
using System.Threading.Tasks;
using System.Linq;

[assembly: ExportFont("Roboto-Bold.ttf", Alias = "Bold")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Regular")]
[assembly: ExportFont("Font Awesome 5 Brands-Regular-400.otf", Alias = "fa-brand")]
[assembly: ExportFont("Font Awesome 5 Free-Regular-400.otf", Alias = "fa-reg")]
[assembly: ExportFont("Font Awesome 5 Free-Solid-900.otf", Alias = "fa-solid")]

namespace ConsultApp
{
    public partial class App
    {
        private ILocation location { get; }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            //this.location = location;
        }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzg4MTg2QDMxMzgyZTM0MmUzMGhsWmVpcHBNcUVkZzlIRXVsNHNEcE5rNEJPclMwKzVvaWVwZHp1L0VxTVk9");
            InitializeComponent();

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
            containerRegistry.RegisterForNavigation<DoctorsSchedule, DoctorsScheduleViewModel>();
            containerRegistry.RegisterForNavigation<PendingConsultationPage, PendingConsultationPageViewModel>();
        }

        //static properties and methods

        public static readonly HttpClient httpClient = new HttpClient();

        public static Location CurrentLocation = new Location();

        private static readonly Lazy<SQLiteAsyncConnection> dbConnection =
            new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(SQLiteConstants.DatabasePath, SQLiteConstants.Flags));

        public static SQLiteAsyncConnection ConnectionString = dbConnection.Value;

        public static async Task<SQLiteAsyncConnection> CreateDatabaseTable<T>()
        {
            if (!ConnectionString.TableMappings.Any(x => x.MappedType == typeof(T)))
            {
                await ConnectionString.EnableWriteAheadLoggingAsync();
                await ConnectionString.CreateTablesAsync(CreateFlags.None, typeof(T));
            }
            return ConnectionString;
        }

        //private async Task GetLocation()
        //{
        //    try
        //    {
        //        await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
        //        await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        //        await Permissions.CheckStatusAsync<Permissions.NetworkState>();
        //        await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

        //        await Permissions.RequestAsync<Permissions.LocationAlways>();
        //        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        //        await Permissions.RequestAsync<Permissions.NetworkState>();
        //        await Permissions.RequestAsync<Permissions.StorageWrite>();
        //        //TODO: ADD EACH PLATFORM IMPLEMENTATION
        //        await location.DisplayLocationSettingsRequest().ContinueWith(async x => await SetLocation());
        //    }
        //    catch (Exception)
        //    {
        //        await PopupNavigation.Instance.PushAsync(new LocationError(), true);
        //    }
        //}

        //private async Task SetLocation()
        //{
        //    App.CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest
        //    {
        //        DesiredAccuracy = GeolocationAccuracy.High,
        //        Timeout = TimeSpan.FromSeconds(10)
        //    });
        //}
    }
}
