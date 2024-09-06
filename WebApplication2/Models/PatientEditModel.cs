using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    [KnownType(typeof(PatientEditModel))]
    public class PatientEditModel
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Adress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int DistrictId { get; set; } 
    }

}