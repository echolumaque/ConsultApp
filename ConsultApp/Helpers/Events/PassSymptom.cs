using System.Collections.ObjectModel;
using ConsultApp.API.Models;
using Prism.Events;
using System.Collections.Generic;

namespace ConsultApp.Helpers
{
    public class PassSymptom : PubSubEvent<ObservableCollection<object>> { }
    
    public class ButtonEnabled : PubSubEvent<bool> { }
}
