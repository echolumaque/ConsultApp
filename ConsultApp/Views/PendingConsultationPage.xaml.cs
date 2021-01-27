using Xamarin.Forms;
using ConsultApp.ViewModels;

namespace ConsultApp.Views
{
    public partial class PendingConsultationPage : ContentPage
    {
        public PendingConsultationPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var pending = button.BindingContext;
            var vm = BindingContext as PendingConsultationPageViewModel;
            vm.RemovePending.Execute(pending);
        }
    }
}