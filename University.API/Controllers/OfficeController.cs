using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;

namespace University.API.Controllers
{

    [RoutePrefix("api/Office")]
    public class OfficeController : ApiController
    {
        private readonly UniversityEntities context = new UniversityEntities();
        /// <summary>
        /// Crear un objeto del office
        /// </summary>
        /// <returns>Obejto office</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>

        [HttpGet]

        public IHttpActionResult GetAll()
        {
            var office = context.OfficeAssignment.ToList();
            var officeDTO = office.Select(x => new OfficeDTO
            {
                InstructorID = x.InstructorID,
                Location = x.Location,

            }).ToList();


            return Ok(officeDTO);
        }

        /// <summary>
        /// Crear un objeto del office
        /// </summary>
        /// <param name="id">Objeto de office</param>
        /// <returns>Obejto office</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>


        [HttpGet]

        public IHttpActionResult GetById(int id)
        {
            var office = context.OfficeAssignment.Find(id);
            var officeDTO = new OfficeDTO
            {
                InstructorID = office.InstructorID,
                Location = office.Location

            };
            return Ok(officeDTO);
        }


        /// <summary>
        /// Crear un objeto del office
        /// </summary> 
        /// <param name="officeDTO">objeto del office</param>
        /// <returns>objeto del office</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>
        [HttpPost]
        public IHttpActionResult Create(OfficeDTO officeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var office = context.OfficeAssignment.Add(new OfficeAssignment
                {
                    Location = officeDTO.Location
                });

                context.SaveChanges();

                officeDTO.InstructorID = office.InstructorID;

                return Ok(officeDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Crear un objeto del office
        /// </summary>
        /// <param name="id">objeto del officce</param>
        /// <param name="officeDTO">objeto del office</param>
        /// <returns>objeto del estudiante</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>

        [HttpPut]
        public IHttpActionResult Edit(int id, OfficeDTO officeDTO)
        {
            try
            {
                if (id != officeDTO.InstructorID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var office = context.OfficeAssignment.Find(id);
                if (office == null) return NotFound();

                office.Location = officeDTO.Location;


                context.SaveChanges();
                return Ok(officeDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Crear un objeto del office
        /// </summary>
        /// <param name="id">C objeto del office</param>
        /// <returns></returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response> 

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {

                var office = context.OfficeAssignment.Find(id);
                if (office == null)
                    return NotFound();

                if (context.Enrollment.Any(x => x.OfficeAssignmentID == id))
                    throw new Exception("Dependencia");

                context.OfficeAssignment.Remove(office);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

}