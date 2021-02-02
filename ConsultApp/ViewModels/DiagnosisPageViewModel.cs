using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ConsultApp.API.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using ConsultApp.Helpers;
using ConsultApp.Helpers.Interfaces;

namespace ConsultApp.ViewModels
{
    public partial class DiagnosisPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor { get; }

        public DiagnosisPageViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);
            this.navigationService = navigationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Loading = true;
            ViewsLoaded = false;
            var items = parameters["Diseases"] as ObservableCollection<DiagnosisModel>;
            Diseases = new ObservableCollection<DiagnosisModel>(items);
            foreach (var item in Diseases)
            {
                item.DoctorsPageCommand = new DelegateCommand<string>(async (specialty) => await DoctorsPage(specialty));
            }

            if (Diseases.Count == 0)
            {
                Empty = true;
                ViewsLoaded = false;
            }
            else
            {
                Empty = false;
                ViewsLoaded = true;
            }

            Loading = false;
            ViewsLoaded = true;

            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DiagnosisPage", new System.Collections.Generic.Dictionary<string, string>
            {
                    { "Value", "DiagnosisPageVisits" }
            });
        }

        #region Properties

        private bool empty;
        public bool Empty
        {
            get { return empty; }
            set { SetProperty(ref empty, value); }
        }

        private ObservableCollection<DiagnosisModel> diseases;
        public ObservableCollection<DiagnosisModel> Diseases
        {
            get { return diseases; }
            set { SetProperty(ref diseases, value); }
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
        private async Task DoctorsPage(string specialty)
        {
            Loading = true;
            ViewsLoaded = false;
            var parameters = new NavigationParameters
            {
                { "Major", specialty }
            };
            await navigationService.NavigateAsync("DoctorsPage", parameters);
            ViewsLoaded = true;
            Loading = false;
        }
        #endregion
    }
}
