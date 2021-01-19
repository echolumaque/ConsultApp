using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ConsultApp.API.Models;
using ConsultApp.Helpers;
using ConsultApp.ViewModels;
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
            var enabled = true;
            if (e.Value == "")
            {
                var cleared = sender as Syncfusion.XForms.ComboBox.SfComboBox;
                var selectedSymptoms = cleared.BindingContext;
                var vm = BindingContext as ConsultPageViewModel;
                vm.RemoveAllSymptom.Execute(selectedSymptoms);
            }
            else
            {
                for (int i = 0; i < (e.Value as List<object>).Count; i++)
                {
                    var collectionofsymptoms = (e.Value as List<object>)[i];
                    symptoms.Add(collectionofsymptoms);
                }
            }
            eventAggregator.GetEvent<PassSymptom>().Publish(symptoms);
            eventAggregator.GetEvent<ButtonEnabled>().Publish(enabled);
        }

        private void SwipeItem_Invoked(object sender, System.EventArgs e)
        {
            var swipe = sender as SwipeItem;
            var symptom = swipe.BindingContext;
            var vm = BindingContext as ConsultPageViewModel;
            vm.RemoveSymptom.Execute(symptom);

            //var casting = vm.SymptomID.Cast<SymptomsModel>();
            //combobox.SelectedValue = item.Name;
            
            //System.Diagnostics.Debug.WriteLine("object type " + combobox.SelectedValue.GetType());
        }
    }
}
