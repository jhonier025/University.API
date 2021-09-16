using System;
using System.Linq;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;

namespace University.API.Controllers
{    
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        private readonly UniversityEntities constext = new UniversityEntities();
        [HttpGet]
        //[Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var students = constext.Student.ToList();
            var studentsDTO = students.Select(x => new StudentDTO
            {
                ID = x.ID,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName,
                EnrollmentDate = x.EnrollmentDate.Value
                
            }).ToList();

        
            return Ok(studentsDTO);
        }

        [HttpGet]
       
        public IHttpActionResult GetById(int id)
        {
            var students = constext.Student.Find(id);

            var studentsDTO = new StudentDTO
            {
                ID = students.ID,
                FirstMidName = students.FirstMidName,
                LastName = students.LastName,
                EnrollmentDate = students.EnrollmentDate.Value

            };


            return Ok(studentsDTO);
        }
        /// <summary>
        /// Crear un objeto del estudiante
        /// </summary>
        /// <param name="studentDTO">objeto del estudiante</param>
        /// <returns>objeto del estudiante</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>
        [HttpPost]
        public IHttpActionResult Create(StudentDTO studentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var student = constext.Student.Add(new Student
                {
                    FirstMidName = studentDTO.FirstMidName,
                    LastName = studentDTO.LastName,
                    EnrollmentDate = studentDTO.EnrollmentDate
                });

                constext.SaveChanges();

                studentDTO.ID = student.ID;

                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Edit(int id,StudentDTO studentDTO)
        {
            try
            {
                if (id != studentDTO.ID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var student = constext.Student.Find(id);
                if (student == null)
                    return NotFound();

                student.LastName = studentDTO.LastName;
                student.FirstMidName = studentDTO.FirstMidName;
                student.EnrollmentDate = studentDTO.EnrollmentDate;

                constext.SaveChanges();
                                
                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                
                var student = constext.Student.Find(id);
                if (student == null)
                    return NotFound();

                if (constext.Enrollment.Any(x => x.StudentID == id))
                    throw new Exception("Dependencia");

                constext.Student.Remove(student);                             
                constext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
