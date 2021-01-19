using Prism.Events;
using System.Collections.ObjectModel;
using ConsultApp.API.Models;

namespace ConsultApp.Helpers
{
    public class PassDisease : PubSubEvent<ObservableCollection<DiagnosisModel>>
    {
    }
}
