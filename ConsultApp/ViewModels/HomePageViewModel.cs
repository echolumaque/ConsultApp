using ConsultApp.API.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System;
using Refit;
using ConsultApp.API.Interfaces;
using ConsultApp.API;
using ConsultApp.API.Intefaces;
using ConsultApp.Helpers.Images;
using System.Reflection;
using ConsultApp.Helpers.CustomRenderers;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using ConsultApp.Dialogs.Views;
using ConsultApp.Helpers.Interfaces;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AppCenter.Crashes;

namespace ConsultApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor { get; }
        private ILocation location { get; }
        private IToast toast { get; }

        public DelegateCommand<NavigationParameters> PassDiseaseForInfo 
        {
            get => new DelegateCommand<NavigationParameters>(async (nav) => await navigationService.NavigateAsync("SymptomsInfo", nav));
        }
        public DelegateCommand ConsultationCommand { get; }
        public DelegateCommand PendingConsultationCommand { get; }
        public DelegateCommand ConsultAppDoctorsCommand { get; }
        public DelegateCommand AboutUsCommand { get; }


        public HomePageViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor, ILocation location, IToast toast) : base(navigationService)
        {
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.FromHex("E6EDFF"));

            this.location = location;
            this.toast = toast;
            this.navigationService = navigationService;

            Carousel = new ObservableCollection<HomeCarouselModel>
            {
                new HomeCarouselModel
                {
                    Title = "Stay Home! Consult virtually via ConsultApp",
                    Contents = "Use ConsultApp to virtually diagnose what you feel today.",
                    DoctorImage = ImageSource.FromResource("ConsultApp.Helpers.Images.doc1.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new HomeCarouselModel
                {
                    Title = "Stay Home, Stay Safe, Stay Strong!",
                    Contents = "Avoid crowded medical centers and gain ease of mind amidst of dangers nowadays.",
                    DoctorImage = ImageSource.FromResource("ConsultApp.Helpers.Images.doc2.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new HomeCarouselModel
                {
                    Title = "Stay Bright!",
                    Contents = "ConsultApp offers an array of informations about different diseases.",
                    DoctorImage = ImageSource.FromResource("ConsultApp.Helpers.Images.doc3.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new HomeCarouselModel
                {
                    Title = "Stay Calm!",
                    Contents = "Schedule an e-visit and discuss the plan with a doctor.",
                    DoctorImage = ImageSource.FromResource("ConsultApp.Helpers.Images.doc4.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
            };
            ChangeCarouselPosition();

            ConsultationCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("ConsultPage"));
            PendingConsultationCommand = new DelegateCommand(async () => await PendingConsultation());
            ConsultAppDoctorsCommand = new DelegateCommand(async () => await this.navigationService.NavigateAsync("ConsultAppDoctors"));
            AboutUsCommand = new DelegateCommand(async () => await AboutUs());

            Time = DateTime.Now.Hour > 0 && DateTime.Now.Hour < 12 ? Time = "Good morning," : DateTime.Now.Hour > 11 && DateTime.Now.Hour < 19 ? Time = "Good afternoon," : Time = "Good evening,";
            Name = Preferences.Get("name", "User");

            Image = Preferences.Get("gender", "Male").Equals("Male") ? Image = ImageSource.FromResource("ConsultApp.Helpers.Images.avatar.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly) : Preferences.Get("gender", "Female").Equals("Female") ? Image = ImageSource.FromResource("ConsultApp.Helpers.Images.avatar1.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly) : Image = ImageSource.FromResource("ConsultApp.Helpers.Images.avatar.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Register", new System.Collections.Generic.Dictionary<string, string>
            {
                    { "HomePage", "HomePageVisits" }
            });
        }

        public override void OnNavigatedTo(INavigationParameters parameters) => setStatusBarColor.SetStatusBarColor(Color.FromHex("E6EDFF"));

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                await App.RetryPolicy(async () => await Authenticate()).ContinueWith(async x => await App.RetryPolicy(async () => await GetDisease()));
                await App.RetryPolicy(async () => await GetLocation());
                await Crashes.HasReceivedMemoryWarningInLastSessionAsync();
                await Crashes.HasCrashedInLastSessionAsync();
                await Crashes.GetLastSessionCrashReportAsync();
            }
            catch (Exception)
            {
                toast.ShowToast("Could not connect to server. Please make sure that you have an internet connection and restart the application");
            }
        }
      
        #region Properties

        private ObservableCollection<DiseaseInfoModel> disease;
        public ObservableCollection<DiseaseInfoModel> Disease
        {
            get { return disease; }
            set { SetProperty(ref disease, value); }
        }

        private string placeHolder;
        public string Placeholder
        {
            get { return placeHolder; }
            set { SetProperty(ref placeHolder, value); }
        }

        private ObservableCollection<HomeCarouselModel> carousel;
        public ObservableCollection<HomeCarouselModel> Carousel
        {
            get { return carousel; }
            set { SetProperty(ref carousel, value); }
        }

        private int position;
        public int Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private object image;
        public object Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        #endregion

        #region Methods
        private async Task Authenticate()
        {
            var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(CalculateHMACMD5())
            };
            var authApi = RestService.For<IAuthApi>(APIConfig.AuthApi, refitSettings);
            var authResponse = await authApi.GetToken();
            APIConfig.Token = authResponse.Token;

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

        private async Task GetDisease()
        {
            var getSymptomsRequest = RestService.For<IGetDiseaseList>(APIConfig.HealthApi);
            var response = await getSymptomsRequest.GetSymptoms(APIConfig.Token);

            Disease = new ObservableCollection<DiseaseInfoModel>(response);
            Placeholder = $"Get to know {response[App.rnd.Next(Disease.Count)].Name.ToLower()} more {new EmojiHelper(0x1F50D)}..";
        }

        private void ChangeCarouselPosition()
        {
            
            Device.StartTimer(TimeSpan.FromSeconds(4.0), () =>
            {
                Position = Position != Carousel.Count - 1 ? Position++ : Position = 0;
                return true;
            });
        }

        private async Task GetLocation()
        {
            try
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                await Permissions.RequestAsync<Permissions.NetworkState>();
                await Permissions.RequestAsync<Permissions.StorageWrite>();

                await location.DisplayLocationSettingsRequest().ContinueWith(async x => await SetLocation());
            }
            catch (Exception)
            {
                await PopupNavigation.Instance.PushAsync(new LocationError(), true);
            }
        }

        private async Task SetLocation()
        {
            App.CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.High,
                Timeout = TimeSpan.FromSeconds(5)
            });

            if (App.CurrentLocation == null)
            {
                App.CurrentLocation = await Geolocation.GetLastKnownLocationAsync();
            }
        }

        private async Task PendingConsultation()
        {
            if (Preferences.Get("navigatable", false) == true)
                await navigationService.NavigateAsync("PendingConsultationPage");
            else
                toast.ShowToast("Your don't have any pending consultations.");

        }

        private async Task AboutUs()
        {
            await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert
                ("Group 4 DICT 2-1 Members",
                "Lumaque, Jhericoh Janquill N. - Lead Programmer, Designer\n\nRecreo, Ann Bernadette B. - Project Manager\n\nHernandez, Johannah Marie R. - Project Manager\n\n" +
                "Guisadio, Ana Beatriz A. - Designer\n\nCorales, Mark Reinell S. - Financial Manager\n\nFernandez, Kathrina Bianca R. - Business Analyst\n\n" +
                "Bagasbas, Leziel Faith A. - Business Analyst\n\nCorpuz, Bithiah Pelaiah T. Programmer, Developer", "Okay");
        }
        #endregion
    }
}