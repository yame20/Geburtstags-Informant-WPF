using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Geburtstags_Informant_WPF
{
    [DataContract]
    class Gift
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Year { get; set; }
        public string DisplayedNameInWPF { get; set; }

        public Gift(string name, int year)
        {
            Name = name;
            Year = year;
            DisplayedNameInWPF = $"{Name} geschenkt im Jahr {Year}";
        }

        public override string ToString()
        {
            return $"{Name} geschenkt im Jahr {Year}";
        }
    }
}
