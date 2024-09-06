using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    [KnownType(typeof(DoctorListModel))]
    public class DoctorListModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int CabinetNumber { get; set; } 
        public string SpecializationName { get; set; } 
        public int? DistrictNumber { get; set; } 
    }
}