using System.Collections.ObjectModel;
using System.Linq;
using ConsultApp.API.Models;
using Prism.Navigation;
using ConsultApp.Helpers;
using Xamarin.Forms;
using System;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

namespace ConsultApp.ViewModels
{
    public class DoctorsPageViewModel : ViewModelBase
    {
        private ISetStatusBarColor setStatusBarColor { get; }

        public DoctorsPageViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);

            Triage = new ObservableCollection<DoctorsAndSpecializationsModel>()
            {
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. ALCANTARA, Danilo M",
                    Specialization = "Cardiology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = " Dr. DE JESUS, Olivia G.",
                    Specialization = "Acupuncture",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. FRAN, Mary Anne S.",
                    Specialization = "Allergology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. ANTOLIN, Marco",
                    Specialization = "Anesthesia",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = " Dr. CACAS, Rowena G.",
                    Specialization = "Angiology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. MANUEL, Ronald C.",
                    Specialization = "Anthroposophical medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. ARANDIA, Christene Pearl",
                    Specialization = "Child psychiatry",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AVECILLA, Guia",
                    Specialization = "Plastic surgery",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. DEL MORAL",
                    Specialization = "Dermatology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. BACCAY, Michael Martin C.",
                    Specialization =  "Endocrinology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. CERCADO, Ephraim",
                    Specialization =  "Forensic medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. DELA CRUZ, Juan",
                    Specialization =  "Gastroenterology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. DESALES, Rey A.",
                    Specialization =  "General practice",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr.ESTIOCO, Francis Irving S.",
                    Specialization =  "Geriatric medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "BAYLON, HONORATA G.",
                    Specialization =  "Gynecology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. BONIFACIO, LYNN BAQUIRAN",
                    Specialization =  "Internal medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. BAYLON, HONORATA G.",
                    Specialization =  "Hand surgery",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. CAGUIOA, LESLIE G.",
                    Specialization =  "Hematology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. ALCANTARA, DANILO G.",
                    Specialization =  "Homeopathy",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. SELUDO, BERNADETTE T.",
                    Specialization =  "Infectiology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor =  "Dr. VERGARA, NONILON",
                    Specialization =  "Manual medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AMPARO, JOSE ROBERT",
                    Specialization =  "Maxillo Surgery"
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor =  "Dr. AGRAVA, MA. AMPARO C.",
                    Specialization =  "Nephrology"
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. CRUZ, ROMULUS EMMANUEL",
                    Specialization = "Neurology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. CRUZ, ROMULUS EMMANUEL",
                    Specialization = "Occupational medicine"
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. MORA, JOSELITO A.",
                    Specialization = "Oncology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor =  "Dr. PINE, FLORENCIO J.",
                    Specialization = "Ophthalmology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. RENALES, MA. LINA",
                    Specialization = "Orthopedics",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor =  "Dr. VILLANUEVA, MARLENE E.",
                    Specialization = "Otolaryngology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AGNO, MAY N.",
                    Specialization = "Pathology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AYUYAO, FERNANDO",
                    Specialization = "Pediatrics",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AYUYAO, FERNANDO",
                    Specialization = "Pulmonology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor =  "Dr. VERGARA, NONILON",
                    Specialization =  "Psychiatry",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AMPARO, JOSE ROBERT",
                    Specialization =  "Tropical medicine"
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor =  "Dr. AGRAVA, MA. AMPARO C.",
                    Specialization =  "Sports medicine"
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. CRUZ, ROMULUS EMMANUEL",
                    Specialization = "Surgery",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. CRUZ, ROMULUS EMMANUEL",
                    Specialization = "Radiology"
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. MORA, JOSELITO A.",
                    Specialization = "Rheumatology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor =  "Dr. PINE, FLORENCIO J.",
                    Specialization = "Urology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. RENALES, MA. LINA",
                    Specialization = "Dentistry",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor =  "Dr. VILLANUEVA, MARLENE E.",
                    Specialization = "Orthodontics",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AGNO, MAY N.",
                    Specialization = "Oral surgery",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AYUYAO, FERNANDO",
                    Specialization = "Periodontology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AYUYAO, FERNANDO",
                    Specialization = "Reconstructive dentistry",
                },
            };
            AssignAvailabilityAndHospital();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var specialty = parameters["Major"] as string;
            Major = specialty;
            DocsWithMajor = new ObservableCollection<DoctorsAndSpecializationsModel>(Triage.Where(x => x.Specialization.Equals(specialty)));
        }
        #region Properties

        private ObservableCollection<DoctorsAndSpecializationsModel> Triage;


        private string major;
        public string Major
        {
            get { return major; }
            set { SetProperty(ref major, value); }
        }

        private ObservableCollection<DoctorsAndSpecializationsModel> docsWithMajor;
        public ObservableCollection<DoctorsAndSpecializationsModel> DocsWithMajor
        {
            get { return docsWithMajor; }
            set { SetProperty(ref docsWithMajor, value); }
        }

        #endregion

        #region Methods

        private void AssignAvailabilityAndHospital()
        {
            string[] hospitals = { "Saint Luke's Medical Center", "University of Santo Tomas Hospital", "Makati Medical Center", "The Medical City", "Manila Doctors Hospital" ,"Ospital ng makati"};
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "By appointment", "manila hospital"};
        

            var rnd = new Random();
           
            foreach (var Doctors in Triage)
            {
                Doctors.Hospital = hospitals[rnd.Next(6)];
                Doctors.DaysAvailable = days[rnd.Next(7)];

                if (Doctors.Hospital.Equals(hospitals[0]))
                {
                    var hosp = new Location(14.5549, 121.0482);
                    Doctors.Distance = Location.CalculateDistance(hosp, App.CurrentLocation, DistanceUnits.Kilometers);
                }
                else if (Doctors.Hospital.Equals(hospitals[1]))
                {
                    var hosp = new Location(14.6114, 120.9902);
                    
                    Doctors.Distance = Location.CalculateDistance(hosp, App.CurrentLocation, DistanceUnits.Kilometers);
                }
                else if (Doctors.Hospital.Equals(hospitals[2]))
                {
                    var hosp = new Location(14.5590, 121.0146);
                    
                    Doctors.Distance = Location.CalculateDistance(hosp, App.CurrentLocation, DistanceUnits.Kilometers);
                }
                else if (Doctors.Hospital.Equals(hospitals[3]))
                {
                    var hosp = new Location(14.5895, 121.0693);
                    
                    Doctors.Distance = Location.CalculateDistance(hosp, App.CurrentLocation, DistanceUnits.Kilometers);
                }
                else if (Doctors.Hospital.Equals(hospitals[4]))
                {
                    var hosp = new Location(14.5820, 120.9829);
                    
                    Doctors.Distance = Location.CalculateDistance(hosp, App.CurrentLocation, DistanceUnits.Kilometers);
                }
            }

            //Distance = Array.FindIndex(hospitals, hosp => hosp.Equals(hospitals[0]))
        }

        #endregion
    }
}
