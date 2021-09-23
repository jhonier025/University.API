using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using AutoMapper;

namespace University.API.Controllers
{    
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        private readonly IMapper mapper;
        private readonly StudentRepository studentRepository = new StudentRepository(new UniversityEntities());


        public StudentsController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Crear un objeto del estudiante
        /// </summary>
        /// <returns>Obejto estudiante</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        
        [HttpGet]
       
        public async Task<IHttpActionResult> GetAll()
        {
            var students = await studentRepository.GetAll();
            var studentsDTO = students.Select(x => mapper.Map<StudentDTO>(x));
           

        
            return Ok(studentsDTO);
        }
        /// <summary>
        /// Crear un objeto del estudiante
        /// </summary>
        /// <param name="id">Objeto de estudiante</param>
        /// <returns>Obejto estudiante</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        

        [HttpGet]
       
        public async Task<IHttpActionResult> GetById(int id)
        {
            var student = await studentRepository.GetById(id);

            var studentsDTO = mapper.Map<StudentDTO>(student);
          

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
        public async Task<IHttpActionResult> Create(StudentDTO studentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var student = mapper.Map<Student>(studentDTO);
                 student = await studentRepository.Insert(student);

                

                studentDTO.ID = student.ID;

                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Crear un objeto del estudiante
        /// </summary>
        /// <param name="id">objeto del estudiante</param>
        /// <param name="studentDTO">objeto del estudiante</param>
        /// <returns>objeto del estudiante</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>

        [HttpPut]
        public async Task<IHttpActionResult>Edit(int id, StudentDTO studentDTO)
        {
            try
            {
                if (id != studentDTO.ID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var student = await studentRepository.GetById(id);
                if (student == null) return NotFound();

                // Update Field
                student.LastName = studentDTO.LastName;
                student.FirstMidName = studentDTO.FirstMidName;
                student.EnrollmentDate = studentDTO.EnrollmentDate;

                //Update All
                //student = mapper.Map<Student>(studentDTO);



                await studentRepository.Update(student);
                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Crear un objeto del estudiante
        /// </summary>
        /// <param name="id">C objeto del estudiante</param>
        /// <returns></returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response> 

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {

                var student = await studentRepository.GetById(id);
                if (student == null)
                    return NotFound();

               // if (context.Enrollment.Any(x => x.StudentID == id))
                 //   throw new Exception("Dependencia");

                await studentRepository.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
