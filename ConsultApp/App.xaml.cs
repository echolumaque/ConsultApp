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
using SQLite;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Polly;

[assembly: ExportFont("Roboto-Bold.ttf", Alias = "Bold")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Regular")]
[assembly: ExportFont("Font Awesome 5 Brands-Regular-400.otf", Alias = "fa-brand")]
[assembly: ExportFont("Font Awesome 5 Free-Regular-400.otf", Alias = "fa-reg")]
[assembly: ExportFont("Font Awesome 5 Free-Solid-900.otf", Alias = "fa-solid")]

namespace ConsultApp
{
    public partial class App
    {

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzg4MTg2QDMxMzgyZTM0MmUzMGhsWmVpcHBNcUVkZzlIRXVsNHNEcE5rNEJPclMwKzVvaWVwZHp1L0VxTVk9");
            InitializeComponent();

            if (Settings.FirstRun)
            {
                await NavigationService.NavigateAsync("CustomNavigationPage/Onboarding");
                Settings.FirstRun = false;
            }
            else
            {
                await NavigationService.NavigateAsync("CustomNavigationPage/HomePage");
            }

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
            containerRegistry.RegisterForNavigation<ConsultAppDoctors, ConsultAppDoctorsViewModel>();
            containerRegistry.RegisterForNavigation<Onboarding, OnboardingViewModel>();
            containerRegistry.RegisterForNavigation<Register, RegisterViewModel>();
        }

        protected override void OnStart()
        {
            const string ios = "b9f253ab-47e9-40bd-892b-f1687efe3ebc";
            const string android = "382f69c3-84d5-4037-9743-dbab28403e1a";
            AppCenter.Start($"ios={ios};android={android};", typeof(Analytics), typeof(Crashes), typeof(Distribute));
            Distribute.CheckForUpdate();
        }
        //static properties and methods
        public static readonly Random rnd = new Random();

        public static readonly HttpClient httpClient = new HttpClient();

        public static Location CurrentLocation = new Location();

        private static readonly Lazy<SQLiteAsyncConnection> dbConnection = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(SQLiteConstants.DatabasePath, SQLiteConstants.Flags));

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

        public static async Task RetryPolicy(Func<Task> action)
        {
            await Policy.Handle<Exception>().WaitAndRetryAsync(5, tries => TimeSpan.FromSeconds(Math.Pow(2, tries))).ExecuteAsync(action);
        }

        public static string[] hospitals = { "Saint Luke's Medical Center", "University of Santo Tomas Hospital", "Makati Medical Center", "The Medical City", "Manila Doctors Hospital" ,"Ospital ng Makati",
            "Pasig General Hospital", "Rizal Medical Center", "Mandaluyong Medical Center", "Asian Hospital and Medical Center", "Olivarez General Hospital"};

        public static DayOfWeek[] days = { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
    }
}