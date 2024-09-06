using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

[RoutePrefix("api/Cabinet")]
public class CabinetsController : ApiController
{
    private ApplicationDbContext db = new ApplicationDbContext();

    [HttpGet]
    [Route("")]
    public IHttpActionResult GetCabinets()
    {
        var cabinets = db.Cabinets.ToList();
        return Ok(cabinets); 
    }
}

