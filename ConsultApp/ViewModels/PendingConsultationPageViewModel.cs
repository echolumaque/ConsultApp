using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ConsultApp.API.Models;

namespace ConsultApp.ViewModels
{
    public class PendingConsultationPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        public PendingConsultationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Pendings = new ObservableCollection<PendingConsultationsModel>(await App.ConnectionString.Table<PendingConsultationsModel>().ToListAsync());
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

        #endregion
    }
}
