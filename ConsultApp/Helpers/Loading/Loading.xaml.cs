using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConsultApp.Helpers.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentView
    {
        public Loading()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create("IsBusy", typeof(bool), typeof(Loading), false, BindingMode.TwoWay);

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }
    }
}