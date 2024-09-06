using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

[RoutePrefix("api/District")]
public class DistrictsController : ApiController
{
    private ApplicationDbContext db = new ApplicationDbContext();

    [HttpGet]
    [Route("")]
    public IHttpActionResult GetDistricts()
    {
        var districts = db.Districts.ToList();
        return Ok(districts); 
    }
}
