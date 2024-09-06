using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    [KnownType(typeof(Specialization))]
    public class Specialization
    {
        public int Id { get; set; }
        public string Name_s { get; set; }
    }
}