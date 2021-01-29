using System.Reflection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ConsultApp.API.Models;
using Xamarin.Forms;
using ConsultApp.Helpers.Images;
using ConsultApp.Helpers.Interfaces;

namespace ConsultApp.ViewModels
{
    public class OnboardingViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public OnboardingViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.navigationService = navigationService;
            setStatusBarColor.SetStatusBarColor(Color.White);
            Onboarding = new ObservableCollection<OnboardingModel>
            {
                new OnboardingModel
                {
                    Title = "Stay Home! Consult virtually via ConsultApp",
                    Contents = "Use ConsultApp to virtually diagnose what you feel today.",
                    Image = ImageSource.FromResource("ConsultApp.Helpers.Images.doc1.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                    NextPage = new DelegateCommand(async () => await NextPosition()),
                },
                new OnboardingModel
                {
                    Title = "Stay Home, Stay Safe, Stay Strong!",
                    Contents = "Avoid crowded medical centers and gain ease of mind amidst of dangers nowadays.",
                    Image = ImageSource.FromResource("ConsultApp.Helpers.Images.doc2.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                    NextPage = new DelegateCommand(async () => await NextPosition()),
                },
                new OnboardingModel
                {
                    Title = "Stay Bright!",
                    Contents = "ConsultApp offers an array of informations about different diseases.",
                    Image = ImageSource.FromResource("ConsultApp.Helpers.Images.doc3.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                    NextPage = new DelegateCommand(async () => await NextPosition()),
                },
                new OnboardingModel
                {
                    Title = "Stay Calm!",
                    Contents = "Schedule an e-visit and discuss the plan with a doctor.",
                    Image = ImageSource.FromResource("ConsultApp.Helpers.Images.doc4.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly),
                    NextPage = new DelegateCommand(async () => await NextPosition()),
                },
            };

        }


        #region Properties

        private ObservableCollection<OnboardingModel> onboarding;
        public ObservableCollection<OnboardingModel> Onboarding
        {
            get { return onboarding; }
            set { SetProperty(ref onboarding, value); }
        }

        private int position;
        public int Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }

        #endregion

        #region Methods

        private async Task NextPosition()
        {
            if (Position != Onboarding.Count - 1)
            {
                Position++;
            }
            else
            {
                await navigationService.NavigateAsync("Register");
            }
        }

        #endregion
    }
}
