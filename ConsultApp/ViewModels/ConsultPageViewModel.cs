using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using ConsultApp.API;
using ConsultApp.API.Interfaces;
using ConsultApp.API.Models;
using ConsultApp.Helpers;
using ConsultApp.Helpers.Images;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Refit;
using Xamarin.Forms;

namespace ConsultApp.ViewModels
{
    public class ConsultPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public DelegateCommand<object> RemoveSymptom
        {
            get => new DelegateCommand<object>(symptom => SymptomID.Remove(symptom));   
        }
        public DelegateCommand<object> RemoveAllSymptom
        {
            get => new DelegateCommand<object>(symptom => SymptomID.Clear());
        }
        public DelegateCommand DiagnoseCommand { get; set; }
        private ISetStatusBarColor setStatusBarColor { get; }

        public ConsultPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);

            this.navigationService = navigationService;
            eventAggregator.GetEvent<PassSymptom>().Subscribe(IDs);
            eventAggregator.GetEvent<ButtonEnabled>().Subscribe(ButtonEnabled);

            DiagnoseCommand = new DelegateCommand(async() => await Diagnose());
            IsEnabled = false;
        }

        #region Properties
        private ObservableCollection<SymptomsModel> symptoms;
        public ObservableCollection<SymptomsModel> Symptoms
        {
            get => symptoms;
            set => SetProperty(ref symptoms, value);
        }


        private ObservableCollection<object> symptomid;
        public ObservableCollection<object> SymptomID
        {
            get { return symptomid; }
            set { SetProperty(ref symptomid, value); }
        }


        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }
        #endregion

        #region Methods

        private void IDs(ObservableCollection<object> id) => SymptomID = new ObservableCollection<object>(id);

        private void ButtonEnabled(bool enabled) => IsEnabled = enabled;

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var getSymptomsRequest = RestService.For<IGetSymptomsList>(APIConfig.HealthApi);
            var response = await getSymptomsRequest.GetSymptoms(APIConfig.Token);

            var symptomsWithPic = new ObservableCollection<SymptomsModel>()
            {
                new SymptomsModel
                {
                   Name = "Cough", ID = 15, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.cough.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Fever", ID = 11, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.fever.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Cold sweats", ID = 139, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.sweat.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Weakness", ID = 56, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.weak.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Wheezing", ID = 30, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.tired.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Asthma", ID = 15, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.asthma.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Back pain", ID = 104, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.backpain.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Chest pain", ID = 17, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.chestpain.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Dizziness", ID = 207, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.dizziness.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
                new SymptomsModel
                {
                   Name = "Headache", ID = 9, Photo = ImageSource.FromResource("ConsultApp.Helpers.Images.headache.png",typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                },
            };

            Symptoms = new ObservableCollection<SymptomsModel>(symptomsWithPic.Union(response));
        }

        private async Task Diagnose()
        {
            var casting = SymptomID.Cast<SymptomsModel>();
            var IDs = casting.Select(id => id.ID).ToList();
            string symptoms = string.Concat("[", string.Join(",", IDs), "]");

            var diagnosisRequest = RestService.For<IDiagnosis>(APIConfig.HealthApi);
            var response = await diagnosisRequest.GetDisease(APIConfig.Token, symptoms);

            var parameters = new NavigationParameters()
            {
                { "Diseases", response }
            };
            await navigationService.NavigateAsync("DiagnosisPage", parameters);
        }

        #endregion
    }
}