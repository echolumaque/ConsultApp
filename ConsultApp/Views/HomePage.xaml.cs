using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Navigation;
using ConsultApp.ViewModels;

namespace ConsultApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as Syncfusion.XForms.ComboBox.SfComboBox;
            //var disease = combobox.BindingContext;
            var vm = BindingContext as HomePageViewModel;
            vm.PassDiseaseForInfo.Execute(new NavigationParameters
            {
                { "disease", e.Value }
            });
        }
    }
}