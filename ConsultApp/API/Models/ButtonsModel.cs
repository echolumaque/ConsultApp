using Xamarin.Forms;
using Prism.Commands;

namespace ConsultApp.API.Models
{
    public class ButtonsModel
    {
        public string Title { get; set; }
        public string FontIcon { get; set; }
        public DelegateCommand Commands { get; set; }
    }
}