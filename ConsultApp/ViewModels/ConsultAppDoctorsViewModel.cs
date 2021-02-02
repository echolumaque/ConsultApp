using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Navigation;
using ConsultApp.API.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using ConsultApp.Helpers.Interfaces;

namespace ConsultApp.ViewModels
{
    public class ConsultAppDoctorsViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor;
        public ConsultAppDoctorsViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);
            SelectedIndex = -1;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Loading = true;
            ViewsLoaded = false;
            ConsultAppDoctors = new ObservableCollection<DoctorsAndSpecializationsModel>(buffer);

           

            foreach (var Doctors in ConsultAppDoctors)
            {
                var dict = new Dictionary<int, DoctorsAndSpecializationsModel>
                {
                    {
                        0,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Saint Luke's Medical Center",
                            Distance = Location.CalculateDistance(14.5549, 121.0482, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.5549, 121.0482),
                                    Label = App.hospitals[0],
                                    Address = "St. Luke’s Medical Center is recognized as the leading and most respected healthcare institution in the Philippines. Its two facilities in Quezon City and Global City, Taguig are at par with the most advanced hospitals around the world. A testament to St. Luke's world class quality medical service is its accreditation with, and affiliation to, prestigious international organizations.",
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],

                        }
                    },
                    {
                        1,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "University of Santo Tomas Hospital",
                            Distance = Location.CalculateDistance(14.6114, 120.9902, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.6114, 120.9902),
                                    Label = App.hospitals[1],
                                    Address = "The University of Santo Tomas Hospital (simply UST Hospital or USTH) is a hospital located at the University of Santo Tomas. The hospital has two divisions, a clinical teaching hospital that offers inexpensive medical care for indigent patients and a private hospital for patients with financial means, which is partially used to subsidize the clinical division.",
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],
                        }
                    },
                    {
                        2,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Makati Medical Center",
                            Distance = Location.CalculateDistance(14.5590, 121.0146, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.5590, 121.0146),
                                    Label = App.hospitals[2],
                                    Address = "Makati Medical Center, also known as Makati Med, is a tertiary hospital in Makati, Metro Manila, Philippines with more than 600 beds."
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],

                        }
                    },
                    {
                        3,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "The Medical City",
                            Distance = Location.CalculateDistance(14.5895, 121.0693, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.5895, 121.0693),
                                    Label = App.hospitals[3],
                                    Address = "The Medical City has defined for itself the value proposition: 'Where Patients are Partners.' This phrase finds its fullest meaning when the patient is viewed not as a problem to be solved or a charge to be cared for, but as a partner in his own health."
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],
                        }
                    },
                    {
                        4,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Manila Doctors Hospital",
                            Distance = Location.CalculateDistance(14.5820, 120.9829, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.5820, 120.9829),
                                    Label = App.hospitals[4],
                                    Address = "Manila Doctors Hospital, simply referred to as MaDocs, is a tertiary hospital located in Ermita, Manila, Philippines. It was founded in the City of Manila in 1956 by the group of doctors. The hospital is currently owned by the Manila Medical Services, Inc.",
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],

                        }
                    },
                    {
                        5,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Ospital ng Makati",
                            Distance = Location.CalculateDistance(14.5465, 121.0618, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.5465, 121.0618),
                                    Label = App.hospitals[5],
                                    Address = "Ospital ng Makati",
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],
                        }
                    },
                    {
                        6,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Pasig General Hospital",
                            Distance = Location.CalculateDistance(14.5722, 121.0994, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.5722, 121.09942),
                                    Label = App.hospitals[6],
                                     Address = "Pasig City General Hospital",
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],

                        }
                    },
                    {
                        7,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Rizal Medical Center",
                            Distance = Location.CalculateDistance(14.5642, 121.0659, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.5642, 121.0659),
                                    Label = App.hospitals[7],
                                    Address = "Rizal Medical Center",
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],
                        }
                    },
                    {
                        8,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Mandaluyong Medical Center",
                            Distance = Location.CalculateDistance(14.5763, 121.0353, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.5763, 121.0353),
                                    Label = App.hospitals[8],
                                    Address = "Mandaluyong Medical Center",
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],

                        }
                    },
                    {
                        9,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Asian Hospital and Medical Center",
                            Distance = Location.CalculateDistance(14.4135, 121.0435, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.4135, 121.0435),
                                    Label = App.hospitals[9],
                                    Address = "The Asian Hospital and Medical Center, established on March 15, 2002 with Jorge Garcia, MD, an alumnus of the Faculty of Medicine & Surgery of the University of Santo Tomas, as its founding chairman, is the first private tertiary hospital built in the southern part of Metro Manila.",
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],
                        }
                    },
                    {
                        10,
                        new DoctorsAndSpecializationsModel
                        {
                            Hospital = "Olivarez General Hospital",
                            Distance = Location.CalculateDistance(14.4792298, 120.9968424, App.CurrentLocation, DistanceUnits.Kilometers),
                            MapPins = new ObservableCollection<MapPinModel>
                            {
                                new MapPinModel
                                {
                                    Location = new Position(14.4792298, 120.9968424),
                                    Label = App.hospitals[10],
                                    Address = "Olivarez General Hospital"
                                },
                            },
                            DaysAvailable = App.days[App.rnd.Next(App.days.Length)],

                        }
                    },
                };

                var random = App.rnd.Next(dict.Count);
                Doctors.Hospital = dict[random].Hospital;
                Doctors.DaysAvailable = dict[random].DaysAvailable;
                Doctors.Distance = dict[random].Distance;
                Doctors.MapPins = dict[random].MapPins;

            }

            Loading = false;
            ViewsLoaded = true;


            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("ConsultAppDoctorsPage", new Dictionary<string, string>
            {
                    { "Value", "ConsultAppDoctorsPageVisits" }
            });
        }

        #region Properties

        private ObservableCollection<DoctorsAndSpecializationsModel> buffer = new ObservableCollection<DoctorsAndSpecializationsModel>()
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
                    Specialization = "Endocrinology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. CERCADO, Ephraim",
                    Specialization = "Forensic medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. DELA CRUZ, Juan",
                    Specialization = "Gastroenterology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. DESALES, Rey A.",
                    Specialization = "General practice",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr.ESTIOCO, Francis Irving S.",
                    Specialization = "Geriatric medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "BAYLON, HONORATA G.",
                    Specialization = "Gynecology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. BONIFACIO, LYNN BAQUIRAN",
                    Specialization = "Internal medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. BAYLON, HONORATA G.",
                    Specialization = "Hand surgery",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. CAGUIOA, LESLIE G.",
                    Specialization = "Hematology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. ALCANTARA, DANILO G.",
                    Specialization = "Homeopathy",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. SELUDO, BERNADETTE T.",
                    Specialization = "Infectiology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. VERGARA, NONILON",
                    Specialization = "Manual medicine",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AMPARO, JOSE ROBERT",
                    Specialization = "Maxillo Surgery"
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AGRAVA, MA. AMPARO C.",
                    Specialization = "Nephrology"
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
                    Doctor = "Dr. PINE, FLORENCIO J.",
                    Specialization = "Ophthalmology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. RENALES, MA. LINA",
                    Specialization = "Orthopedics",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. VILLANUEVA, MARLENE E.",
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
                    Doctor = "Dr. VERGARA, NONILON",
                    Specialization = "Psychiatry",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AMPARO, JOSE ROBERT",
                    Specialization = "Tropical medicine"
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. AGRAVA, MA. AMPARO C.",
                    Specialization = "Sports medicine"
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
                    Doctor = "Dr. PINE, FLORENCIO J.",
                    Specialization = "Urology",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. RENALES, MA. LINA",
                    Specialization = "Dentistry",
                },
                new DoctorsAndSpecializationsModel
                {
                    Doctor = "Dr. VILLANUEVA, MARLENE E.",
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

        private ObservableCollection<DoctorsAndSpecializationsModel> consultAppDoctors;
        public ObservableCollection<DoctorsAndSpecializationsModel> ConsultAppDoctors
        {
            get { return consultAppDoctors; }
            set { SetProperty(ref consultAppDoctors, value); }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set 
            { 
                SetProperty(ref searchQuery, value);
                if (string.IsNullOrWhiteSpace(SearchQuery))
                {
                    ConsultAppDoctors = new ObservableCollection<DoctorsAndSpecializationsModel>(buffer);
                }
                else
                {
                    ConsultAppDoctors = SelectedIndex switch
                    {
                        0 => new ObservableCollection<DoctorsAndSpecializationsModel>
                        (ConsultAppDoctors.Where(x => x.Doctor.ToLower().Contains(SearchQuery.ToLower()))),

                        1 => new ObservableCollection<DoctorsAndSpecializationsModel>
                        (ConsultAppDoctors.Where(x => x.Specialization.ToLower().Contains(SearchQuery.ToLower()))),

                        2 => new ObservableCollection<DoctorsAndSpecializationsModel>
                        (ConsultAppDoctors.Where(x => x.Hospital.ToLower().Contains(SearchQuery.ToLower()))),

                        _ => new ObservableCollection<DoctorsAndSpecializationsModel>
                            (ConsultAppDoctors.Where(x => x.Doctor.ToLower().Contains(SearchQuery.ToLower()))),
                    };
                }
            }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        private bool loading;
        public bool Loading
        {
            get { return loading; }
            set { SetProperty(ref loading, value); }
        }

        private bool viewsLoaded;
        public bool ViewsLoaded
        {
            get { return viewsLoaded; }
            set { SetProperty(ref viewsLoaded, value); }
        }

        #endregion
    }
}
