using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO.Packaging;

namespace Geburtstags_Informant_WPF
{
    [DataContract]
    class Profile
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime Birthdate { get; set; }
        [DataMember]
        public ObservableCollection<Plant> PlantList { get; set; }
        [DataMember]
        public ObservableCollection<Gift> GiftList { get; set; }
    
        public Profile(string firstName, string lastName, DateTime birthdate)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            PlantList = new ObservableCollection<Plant>();
            GiftList = new ObservableCollection<Gift>();
        }
    }
}
