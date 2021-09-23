using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;

namespace University.API.Controllers
{
    [RoutePrefix("api/Courses")]
    public class CoursesController : ApiController
    {
        private readonly UniversityEntities context = new UniversityEntities();
        /// <summary>
        /// Crear un objeto del Curso
        /// </summary>
        /// <returns>Obejto Curso</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var courses = context.Course.ToList();
            var courseDTO = courses.Select(x => new CourseDTO
            {
                CourseID = x.CourseID,
                Title = x.Title,
                Credits = x.Credits.Value

            }).ToList();

            return Ok(courseDTO);

        }
        /// <summary>
        /// Crear un objeto del Curso
        /// </summary>
        /// <param name="id">Objeto Curso</param>
        /// <returns>Obejto del Curso</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        [HttpGet]

        public IHttpActionResult GetById(int id)
        {
            var courses = context.Course.Find(id);

            var courseDTO = new CourseDTO
            {
                CourseID = courses.CourseID,
                Title = courses.Title,
                Credits = courses.Credits.Value

            };


            return Ok(courseDTO);
        }

        /// <summary>
        /// Crear un objeto del curso
        /// </summary>
        /// <param name="courseDTO">objeto del curso</param>
        /// <returns>objeto del curso</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>
        [HttpPost]
        public IHttpActionResult Create(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var courses = context.Course.Add(new Course
                {
                    Title = courseDTO.Title,
                    Credits = courseDTO.Credits,

                });

                context.SaveChanges();

                courseDTO.CourseID = courses.CourseID;

                return Ok(courseDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Crear Objeto curso
        /// </summary>
        /// <param name="id">Objeto de curso</param>
        /// <param name="courseDTO">objeto de curso</param>
        /// <returns>objeto del curso</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>

        [HttpPut]
        public IHttpActionResult Edit(int id, CourseDTO courseDTO)
        {
            try
            {
                if (id != courseDTO.CourseID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var course = context.Course.Find(id);
                if (course == null) return NotFound();

                course.Title = courseDTO.Title;
                course.Credits = courseDTO.Credits;
               
                context.SaveChanges();

                return Ok(courseDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// crear objeto del curso
        /// </summary>
        /// <param name="id">objeto del curso</param>
        /// <returns>objeto del curso</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {

                var course = context.Course.Find(id);
                if (course == null)
                    return NotFound();

                if (context.Enrollment.Any(x => x.CourseID == id))
                    throw new Exception("Dependencia");

                context.Course.Remove(course);
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
