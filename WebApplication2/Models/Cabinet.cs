using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    [KnownType(typeof(Cabinet))]
    public class Cabinet
    {
        public int Id { get; set; }
        public int Number { get; set; }
    }
}