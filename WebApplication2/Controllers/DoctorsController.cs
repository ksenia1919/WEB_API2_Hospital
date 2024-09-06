using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Text;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/Doctor")]
    public class DoctorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("{id:int}")]
        [HttpGet]
        [ResponseType(typeof(DoctorEditModel))]
        public IHttpActionResult GetDoctor(int id)
        {
            var doctor = db.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var doctorEditModel = new DoctorEditModel
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                CabinetId = doctor.CabinetId,
                SpecializationId = doctor.SpecializationId,
                DistrictId = doctor.DistrictId
            };

            return Ok(doctorEditModel);
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetDoctors(
             [FromUri] string sortField = "FullName",
             [FromUri] string sortDirection = "asc",
             [FromUri] int page = 1,
             [FromUri] int pageSize = 10
         )
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var doctors = db.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortDirection))
            {
                switch (sortField)
                {
                    case "FullName":
                        doctors = sortDirection == "asc"
                            ? doctors.OrderBy(d => d.FullName)
                            : doctors.OrderByDescending(d => d.FullName);
                        break;
                    case "CabinetNumber":
                        doctors = sortDirection == "asc"
                            ? doctors.OrderBy(d => d.Cabinet.Number)
                            : doctors.OrderByDescending(d => d.Cabinet.Number);
                        break;
                    case "SpecializationName":
                        doctors = sortDirection == "asc"
                            ? doctors.OrderBy(d => d.Specialization.Name_s)
                            : doctors.OrderByDescending(d => d.Specialization.Name_s);
                        break;
                    case "DistrictNumber":
                        doctors = sortDirection == "asc"
                            ? doctors.OrderBy(d => d.District.Number)
                            : doctors.OrderByDescending(d => d.District.Number);
                        break;
                    default:
                        doctors = doctors.OrderBy(d => d.FullName);
                        break;
                }
            }

            int totalItems = doctors.Count();
            int skip = (page - 1) * pageSize;
            var doctorsPaged = doctors.Include(d => d.Cabinet).Include(d => d.Specialization).Include(d => d.District).Skip(skip).Take(pageSize).ToList();

            var doctorList = doctorsPaged.Select(d => new DoctorListModel
            {
                Id = d.Id,
                FullName = d.FullName,
                CabinetNumber = d.Cabinet?.Number ?? 0,
                SpecializationName = d.Specialization?.Name_s ?? "Не указано",
                DistrictNumber = d.District?.Number ?? 0
            }).ToList();

            return Ok(doctorList);
        }


        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateDoctor(DoctorEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newDoctor = new Doctor
            {
                FullName = model.FullName,
                CabinetId = model.CabinetId,
                SpecializationId = model.SpecializationId,
                DistrictId = model.DistrictId
            };

            db.Doctors.Add(newDoctor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newDoctor.Id }, newDoctor);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateDoctor(int id, DoctorEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctorToUpdate = db.Doctors.Find(id);

            if (doctorToUpdate == null)
            {
                return NotFound();
            }

            doctorToUpdate.FullName = model.FullName;
            doctorToUpdate.CabinetId = model.CabinetId;
            doctorToUpdate.SpecializationId = model.SpecializationId;
            doctorToUpdate.DistrictId = model.DistrictId;

            db.SaveChanges();

            return Ok(doctorToUpdate);
        }

        [Route("{id}")]
        [HttpDelete] 
        public IHttpActionResult DeleteDoctor(int id)
        {
            var doctor = db.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctor);
            db.SaveChanges();

            return Ok(); 
        }
    }
}


