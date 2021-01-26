using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace ConsultApp.Dialogs.ViewModels
{
    public class ConfirmConsultationViewModel : BindableBase
    {
        public ConfirmConsultationViewModel()
        {

        }



        #region Properties

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { SetProperty(ref selectedDate, value); }
        }

        #endregion

        #region Methods

        #endregion

    }
}
