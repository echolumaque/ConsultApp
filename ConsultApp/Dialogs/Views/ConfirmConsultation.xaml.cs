using Rg.Plugins.Popup.Pages;
using Prism.Events;
using System;
using ConsultApp.Dialogs.ViewModels;
using ConsultApp.Helpers.Events;

namespace ConsultApp.Dialogs.Views
{
    public partial class ConfirmConsultation : PopupPage
    {
        public ConfirmConsultation(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.GetEvent<PassSelectedDate>().Subscribe(AssignDate);
        }

        private void AssignDate(DateTime selectedDate)
        {
            var vm = BindingContext as ConfirmConsultationViewModel;
            vm.SelectedDate = selectedDate;
        }
    }
}