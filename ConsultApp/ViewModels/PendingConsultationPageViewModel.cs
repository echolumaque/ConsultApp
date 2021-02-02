using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ConsultApp.API.Models;
using ConsultApp.Helpers.Interfaces;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;

namespace ConsultApp.ViewModels
{
    public class PendingConsultationPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IToast toast { get; }
        private ISetStatusBarColor setStatusBarColor { get; }
        public PendingConsultationPageViewModel(INavigationService navigationService, IToast toast, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.setStatusBarColor = setStatusBarColor;
            this.toast = toast;
            this.setStatusBarColor.SetStatusBarColor(Color.White);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                if (await App.ConnectionString.Table<PendingConsultationsModel>().CountAsync() > 0)
                {
                    Empty = false;
                    View = true;
                    Pendings = new ObservableCollection<PendingConsultationsModel>(await App.ConnectionString.Table<PendingConsultationsModel>().ToListAsync());
                    foreach (var item in Pendings)
                    {
                        item.RemoveConsultation = new DelegateCommand<PendingConsultationsModel>(async (pending) => await RemovePendings(pending));

                        item.Pins = new ObservableCollection<Pins>
                        {
                            new Pins
                            {
                                Label = item.Label,
                                Address = item.Address,
                                Position = new Xamarin.Forms.Maps.Position(item.Latitude, item.Longitude),
                            }
                        };
                    }
                }
                else
                {
                    Empty = true;
                    View = false;
                }
            }
            catch (System.Exception)
            {
                Empty = true;
                View = false;
            }
        }

        #region Properties

        private ObservableCollection<PendingConsultationsModel> pendings;
        public ObservableCollection<PendingConsultationsModel> Pendings
        {
            get { return pendings; }
            set { SetProperty(ref pendings, value); }
        }

        private bool empty;
        public bool Empty
        {
            get { return empty; }
            set { SetProperty(ref empty, value); }
        }

        private bool view;
        public bool View
        {
            get { return view; }
            set { SetProperty(ref view, value); }
        }

      
        #endregion

        #region Methods
        private async Task RemovePendings(PendingConsultationsModel pending)
        {
            var popup = await Prism.PrismApplicationBase.Current.MainPage.DisplayAlert
                ("Cancel consultation?", $"Would you like to cancel your pending consultation with {pending.Name} on {pending.SelectedDay}?", "Yes", "No");

            if (popup)
            {
                Pendings.Remove(pending);
                await App.RetryPolicy(async () => await App.ConnectionString.DeleteAsync(pending));
                toast.ShowToast("Successfully removed pending consultation!");
                
                if (await App.ConnectionString.Table<PendingConsultationsModel>().CountAsync() == 0)
                {
                    Empty = true;
                    View = false;
                }

                Analytics.TrackEvent("Remove Consultation", new Dictionary<string, string>
                {
                    { "Registered Users", "No. of times consultation is removed" }
                });
            }
        }
        #endregion
    }
}
