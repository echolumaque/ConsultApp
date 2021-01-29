using System.Collections.Generic;
using System.Collections.ObjectModel;
using ConsultApp.ViewModels;
using ConsultApp.Helpers;
using Prism.Events;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConsultApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultPage : ContentPage
    {
        private readonly IEventAggregator eventAggregator;
        public ConsultPage(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            InitializeComponent();
        }

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var symptoms = new ObservableCollection<object>();
            bool enabled;

            if (e.Value == "")
            {
                var cleared = sender as Syncfusion.XForms.ComboBox.SfComboBox;
                var selectedSymptoms = cleared.BindingContext;
                var vm = BindingContext as ConsultPageViewModel;
                enabled = false;
                eventAggregator.GetEvent<ButtonEnabled>().Publish(enabled);
                vm.RemoveAllSymptom.Execute(selectedSymptoms);
            }
            else
            {
                enabled = true;
                for (int i = 0; i < (e.Value as List<object>).Count; i++)
                {
                    var collectionofsymptoms = (e.Value as List<object>)[i];
                    symptoms.Add(collectionofsymptoms);
                }
            }
            eventAggregator.GetEvent<PassSymptom>().Publish(symptoms);
            eventAggregator.GetEvent<ButtonEnabled>().Publish(enabled);
        }
    }
}
