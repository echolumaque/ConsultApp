using Prism.Commands;

namespace ConsultApp.API.Models
{
    public class OnboardingModel
    {
        public string Title { get; set; }
        public object Image { get; set; }
        public string Contents { get; set; }
        public DelegateCommand NextPage { get; set; }
    }
}
