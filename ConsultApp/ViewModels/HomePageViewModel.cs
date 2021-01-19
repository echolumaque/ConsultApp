using ConsultApp.API;
using ConsultApp.API.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using ConsultApp.Helpers;
using Xamarin.Forms;
using Prism.Services.Dialogs;
using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

namespace ConsultApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;

        private ISetStatusBarColor setStatusBarColor { get; }

        public HomePageViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor, IDialogService dialogService) : base(navigationService)
        {
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.FromHex("E6EDFF"));

            this.navigationService = navigationService;
            this.dialogService = dialogService;

            Buttons = new ObservableCollection<ButtonsModel>()
            {
                new ButtonsModel
                {
                    Title = "Diagnostic",
                    FontIcon = "\uf0f1",
                    Commands = new DelegateCommand(async() => await this.navigationService.NavigateAsync("ConsultPage")),
                },
                new ButtonsModel
                {
                    Title = "Shots",
                    FontIcon = "\uf48e",
                    Commands = new DelegateCommand(async () => await this.dialogService.ShowDialogAsync("LocationError")),
                },
                new ButtonsModel
                {
                    Title = "Consultation",
                    FontIcon = "\uf095",
                    Commands = new DelegateCommand(async() => await navigationService.NavigateAsync(""))
                },
                new ButtonsModel
                {
                    Title = "Ambulance",
                    FontIcon = "\uf0f9",
                    Commands = new DelegateCommand(async() => await navigationService.NavigateAsync(""))
                },
                new ButtonsModel
                {
                    Title = "Nurse",
                    FontIcon = "\uf0f0",
                    Commands = new DelegateCommand(async() => await navigationService.NavigateAsync(""))
                },
                new ButtonsModel
                {
                    Title = "Lab Work",
                    FontIcon = "\uf0f8",
                    Commands = new DelegateCommand(async() => await navigationService.NavigateAsync(""))
                },
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            DependencyService.Get<ISetStatusBarColor>().SetStatusBarColor(Color.FromHex("E6EDFF"));
        }

        

        #region Properties

        private ObservableCollection<ButtonsModel> buttons;
        public ObservableCollection<ButtonsModel> Buttons
        {
            get { return buttons; }
            set { SetProperty(ref buttons, value); }
        }

        #endregion

        #region Methods
        #endregion
    }
}