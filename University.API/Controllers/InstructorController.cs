using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using University.BL.DTOs;
using University.BL.Models;

namespace University.API.Controllers
{
    [RoutePrefix("api/Instructor")]
    public class InstructorController : ApiController
    {
        private readonly UniversityEntities context = new UniversityEntities();
        /// <summary>
        /// Crear un objeto del Instructor
        /// </summary>
        /// <returns>Obejto ibstructor</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>

        [HttpGet]

        public IHttpActionResult GetAll()
        {
            var instructor = context.Instructor.ToList();
            var instructorDTO = instructor.Select(x => new InstructorDTO
            {
                ID = x.ID,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName,
                HireDate = x.HireDate.Value

            }).ToList();


            return Ok(instructorDTO);
        }

        /// <summary>
        /// Crear un objeto del Instructor
        /// </summary>
        /// <param name="id">Objeto de instructor</param>
        /// <returns>Obejto instructor</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>


        [HttpGet]

        public IHttpActionResult GetById(int id)
        {
            var instructor = context.Instructor.Find(id);

            var instructorDTO = new InstructorDTO
            {
                ID = instructor.ID,
                FirstMidName = instructor.FirstMidName,
                LastName = instructor.LastName,
                HireDate = (DateTime)instructor.HireDate.Value

            };


            return Ok(instructorDTO);
        }

        /// <summary>
        /// Crear un objeto del intructor
        /// </summary> 
        /// <param name="instructorDTO">objeto del intructot</param>
        /// <returns>objeto del instructor</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>
        [HttpPost]
        public IHttpActionResult Create(Instructor instructorDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var instructor = context.Instructor.Add(new Instructor
                {
                    LastName = instructorDTO.LastName,
                    FirstMidName = instructorDTO.FirstMidName,
                    HireDate = instructorDTO.HireDate.Value
                });

                context.SaveChanges();

                instructorDTO.ID = instructor.ID;

                return Ok(instructorDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Crear un objeto del instructor
        /// </summary>
        /// <param name="id">objeto del intructor</param>
        /// <param name="instructorDTO">objeto del instructor</param>
        /// <returns>objeto del insructor</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>

        [HttpPut]
        public IHttpActionResult Edit(int id, InstructorDTO instructorDTO)
        {
            try
            {
                if (id != instructorDTO.ID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var instructor = context.Instructor.Find(id);
                if (instructor == null) return NotFound();

                instructor.ID = instructorDTO.ID;


                context.SaveChanges();
                return Ok(instructorDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Crear un objeto del instructor
        /// </summary>
        /// <param name="id">C objeto del instructor</param>
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

                var instructor = context.Instructor.Find(id);
                if (instructor == null)
                    return NotFound();

                if (context.Enrollment.Any(x => x.InstructorID == id))
                    throw new Exception("Dependencia");

                context.Instructor.Remove(instructor);
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
