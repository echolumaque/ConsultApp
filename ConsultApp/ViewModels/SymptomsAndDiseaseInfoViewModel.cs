using System.Threading.Tasks;
using ConsultApp.API.Models;
using Prism.Commands;
using Prism.Navigation;
using Refit;
using ConsultApp.API.Interfaces;
using ConsultApp.API;
using Xamarin.Forms;
using ConsultApp.Helpers.Interfaces;
using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;

namespace ConsultApp.ViewModels
{
    public class SymptomsAndDiseaseInfoViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor { get; }
        public DelegateCommand GoBack { get; set; }
        public SymptomsAndDiseaseInfoViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.navigationService = navigationService;

            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);

            GoBack = new DelegateCommand(async () => await this.navigationService.GoBackAsync());
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Loading = true;
            ViewsLoaded = false;
            
            var disease = parameters["disease"] as DiseaseInfoModel;
            await App.RetryPolicy(async () => await GetInfo(disease.ID));

            Loading = false;
            ViewsLoaded = true;

            Analytics.TrackEvent("SymptomsAndDiseaseInfoPage", new Dictionary<string, string>
            {
                    { "Value", "SymptomsAndDiseaseInfoPageVisits" }
            });
        }

        #region Properties
        private string diseaseName;
        public string DiseaseName
        {
            get { return diseaseName; }
            set { SetProperty(ref diseaseName, value); }
        }

        private string longDesc;
        public string LongDesc
        {
            get { return longDesc; }
            set { SetProperty(ref longDesc, value); }
        }

        private string profName;
        public string ProfName
        {
            get { return profName; }
            set { SetProperty(ref profName, value); }
        }

        private string cause;
        public string Cause
        {
            get { return cause; }
            set { SetProperty(ref cause, value); }
        }

        private string treatment;
        public string Treatment
        {
            get { return treatment; }
            set { SetProperty(ref treatment, value); }
        }

        private string possibleSymptoms;
        public string Symptoms
        {
            get { return possibleSymptoms; }
            set { SetProperty(ref possibleSymptoms, value); }
        }

        private bool loading;
        public bool Loading
        {
            get { return loading; }
            set { SetProperty(ref loading, value); }
        }

        private bool viewsLoaded;
        public bool ViewsLoaded
        {
            get { return viewsLoaded; }
            set { SetProperty(ref viewsLoaded, value); }
        }
        #endregion

        #region Methods

        private async Task GetInfo(int id)
        {
            var request = RestService.For<IDiseaseInfo>(APIConfig.HealthApi);
            var response = await request.GetSymptomsDescription(id, APIConfig.Token);

            DiseaseName = response.Name;
            ProfName = response.ProfName;
            LongDesc = response.Description;
            Cause = response.MedicalCondition;
            Treatment = response.TreatmentDescription;
            Symptoms = response.PossibleSymptoms;
        }

        #endregion
    }
}
