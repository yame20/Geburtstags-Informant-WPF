using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Geburtstags_Informant_WPF
{
    [DataContract]
    class Plant
    {
        [DataMember]
        public string Name { get; set; }

        public Plant(string plantName)
        {
            Name = plantName;
        }
    }
}
