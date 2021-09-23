using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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
    }

}

