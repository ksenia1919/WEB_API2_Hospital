using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    [KnownType(typeof(Doctor))]
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public int CabinetId { get; set; }
        public Cabinet Cabinet { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public int? DistrictId { get; set; } 
        public District District { get; set; }
    }
}