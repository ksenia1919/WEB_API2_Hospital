using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

[RoutePrefix("api/Specialization")]
public class SpecializationsController : ApiController
{
    private ApplicationDbContext db = new ApplicationDbContext();

    [HttpGet]
    [Route("")]
    public IHttpActionResult GetSpecializations()
    {
        var specializations = db.Specializations.ToList();
        return Ok(specializations); 
    }
}



