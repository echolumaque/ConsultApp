using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ConsultApp.API.Models;
using ConsultApp.Helpers.Interfaces;
using ConsultApp.Helpers;
using Xamarin.Forms;

namespace ConsultApp.ViewModels
{
    public class PendingConsultationPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public DelegateCommand<object> RemovePending
        {
            get => new DelegateCommand<object>(async (pending) => await RemovePendings(pending));
        }
        private IToast toast { get; }
        private ISetStatusBarColor setStatusBarColor { get; }
        public PendingConsultationPageViewModel(INavigationService navigationService, IToast toast, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Pendings = new ObservableCollection<PendingConsultationsModel>(await App.ConnectionString.Table<PendingConsultationsModel>().ToListAsync());
            foreach (var item in Pendings)
            {
                item.RemoveConsultation = new DelegateCommand<PendingConsultationsModel>(async (pending) => await RemovePendings(pending));
            }
        }

        #region Properties

        private ObservableCollection<PendingConsultationsModel> pendings;
        public ObservableCollection<PendingConsultationsModel> Pendings
        {
            get { return pendings; }
            set { SetProperty(ref pendings, value); }
        }

        #endregion

        #region Methods
        private async Task RemovePendings(object pending)
        {
            var popup = await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert("Cancel consultation?", "Would you like to cancel your pending consultation?", "Yes", "No");

            if (popup)
            {
                Pendings.Remove((PendingConsultationsModel)pending);
                await App.ConnectionString.DeleteAsync(pending);
                toast.ShowToast("Successfully removed pending consultation!");
            }
            Pendings = new ObservableCollection<PendingConsultationsModel>(await App.ConnectionString.Table<PendingConsultationsModel>().ToListAsync());
        }
        #endregion
    }
}
