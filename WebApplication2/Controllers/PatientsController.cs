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
    [RoutePrefix("api/Patient")]
    public class PatientsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("{id:int}")]
        [HttpGet]
        [ResponseType(typeof(PatientEditModel))]
        public IHttpActionResult GetPatient(int id)
        {
            var patient = db.Patients.Find(id);

            if (patient == null)
            {
                return NotFound();
            }

            var patientEditModel = new PatientEditModel
            {
                Id = patient.Id,
                LastName = patient.LastName,
                FirstName = patient.FirstName,
                MiddleName = patient.MiddleName,
                Adress = patient.Adress,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                DistrictId = patient.DistrictId
            };

            return Ok(patientEditModel);
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPatients(
            [FromUri] string sortField = "LastName", 
            [FromUri] string sortDirection = "asc",
            [FromUri] int page = 1,
            [FromUri] int pageSize = 10
)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var patients = db.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortDirection))
            {
                switch (sortField)
                {
                    case "LastName":
                        patients = sortDirection == "asc"
                            ? patients.OrderBy(p => p.LastName)
                            : patients.OrderByDescending(p => p.LastName);
                        break;
                    case "FirstName":
                        patients = sortDirection == "asc"
                            ? patients.OrderBy(p => p.FirstName)
                            : patients.OrderByDescending(p => p.FirstName);
                        break;
                    case "MiddleName":
                        patients = sortDirection == "asc"
                            ? patients.OrderBy(p => p.MiddleName)
                            : patients.OrderByDescending(p => p.MiddleName);
                        break;
                    case "DateOfBirth":
                        patients = sortDirection == "asc"
                            ? patients.OrderBy(p => p.DateOfBirth)
                            : patients.OrderByDescending(p => p.DateOfBirth);
                        break;
                    case "Gender":
                        patients = sortDirection == "asc"
                            ? patients.OrderBy(p => p.Gender)
                            : patients.OrderByDescending(p => p.Gender);
                        break;
                    case "DistrictNumber":
                        patients = sortDirection == "asc"
                            ? patients.OrderBy(p => p.District.Number)
                            : patients.OrderByDescending(p => p.District.Number);
                        break;
                    default:
                        patients = patients.OrderBy(p => p.LastName); 
                        break;
                }
            }

            int totalItems = patients.Count();
            int skip = (page - 1) * pageSize;
            var patientsPaged = patients.Include(p => p.District).Skip(skip).Take(pageSize).ToList();

            var patientList = patientsPaged.Select(p => new PatientListModel
            {
                Id = p.Id,
                LastName = p.LastName,
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                Adress = p.Adress, 
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender, 
                DistrictNumber = p.District?.Number ?? 0
            }).ToList();

            return Ok(patientList);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreatePatient(PatientEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPatient = new Patient
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                Adress = model.Adress,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                DistrictId = model.DistrictId
            };

            db.Patients.Add(newPatient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newPatient.Id }, newPatient);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdatePatient(int id, PatientEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patientToUpdate = db.Patients.Find(id);

            if (patientToUpdate == null)
            {
                return NotFound();
            }

            patientToUpdate.LastName = model.LastName;
            patientToUpdate.FirstName = model.FirstName;
            patientToUpdate.MiddleName = model.MiddleName;
            patientToUpdate.Adress = model.Adress;
            patientToUpdate.DateOfBirth = model.DateOfBirth;
            patientToUpdate.Gender = model.Gender;
            patientToUpdate.DistrictId = model.DistrictId;

            db.SaveChanges();

            return Ok(patientToUpdate);
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeletePatient(int id)
        {
            var patient = db.Patients.Find(id);

            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
            db.SaveChanges();

            return Ok();
        }
    }
}
