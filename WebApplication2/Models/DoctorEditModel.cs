using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    [KnownType(typeof(DoctorEditModel))]
    public class DoctorEditModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int CabinetId { get; set; }
        public int SpecializationId { get; set; } 
        public int? DistrictId { get; set; } 
    }
}