using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ConsultApp.API.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using ConsultApp.Helpers;

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
            var items = parameters["Diseases"] as ObservableCollection<DiagnosisModel>;
            Diseases = new ObservableCollection<DiagnosisModel>(items);

            foreach (var item in Diseases)
            {
                item.DoctorsPageCommand = new DelegateCommand<string>(async (specialty) => await DoctorsPage(specialty));
            }
        }

        #region Properties

        private ObservableCollection<DiagnosisModel> diseases;
        public ObservableCollection<DiagnosisModel> Diseases
        {
            get { return diseases; }
            set { SetProperty(ref diseases, value); }
        }
        #endregion

        #region Methods
        private async Task DoctorsPage(string specialty)
        {
            var parameters = new NavigationParameters
            {
                { "Major", specialty }
            };
            await navigationService.NavigateAsync("DoctorsPage", parameters);
        }
        #endregion
    }
}
