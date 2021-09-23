using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;

namespace University.API.Controllers
{    
    
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
                     
    {     

        private readonly UniversityEntities context = new UniversityEntities();
        /// <summary>
        /// creaacion de objeto Departments
        /// </summary>
        /// <returns>objeto de Departments</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>

        [HttpGet]

        public IHttpActionResult GetAll()
        {
            var department = context.Department.ToList();
            var departmentDTO = department.Select(x => new DepartmentDTO
            {
                DepartmentID = x.DepartmentID,
                Name = x.Name,
                Budget = (double)x.Budget,
                StartDate = (DateTime)x.StartDate,
                InstructorID = (int)x.InstructorID,
               



            }).ToList();


            return Ok(departmentDTO);
        }
        /// <summary>
        /// Crear un objeto del Departments
        /// </summary>
        /// <param name="id">Objeto de Department</param>
        /// <returns>Obejto Departmens</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>


        [HttpGet]

        public IHttpActionResult GetById(int id)
        {
            var department = context.Department.Find(id);

            var departmentDTO = new DepartmentDTO
            {
                DepartmentID = department.DepartmentID,
                Name = department.Name,
                Budget = (double)department.Budget,
                StartDate = (DateTime)department.StartDate,
                InstructorID = (int)department.InstructorID.Value
                

            };


            return Ok(departmentDTO);
        }
        /// <summary>
        /// Crear un objeto del departamento
        /// </summary>
        /// <param name="departmentDTO">objeto del depatamento</param>
        /// <returns>objeto del departamento</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>
        [HttpPost]
        public IHttpActionResult Create(DepartmentDTO departmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var department = context.Department.Add(new Department
                {
                    Name = departmentDTO.Name,
                    Budget = (decimal?)(double)departmentDTO.Budget,
                    StartDate = (DateTime)departmentDTO.StartDate,
                    InstructorID = (int)departmentDTO.InstructorID
                    
                });

                context.SaveChanges();

                departmentDTO.DepartmentID = department.DepartmentID;

                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Crear un objeto del departamento
        /// </summary>
        /// <param name="id">objeto del departamento</param>
        /// <param name="departmentDTO">objeto del departamento</param>
        /// <returns>objeto del deparamento</returns>
        /// <response code="200">ok. Devuelve el objeto solicitado.</response>
        /// <response code="400">BadRequest. No se cumple con la validacion del modelo.
        /// </response>
        /// <response code="500">InternelServerError. Se ha prensentado un error.</response>

        [HttpPut]
        public IHttpActionResult Edit(int id, Department departmentDTO)
        {
            try
            {
                if (id != departmentDTO.DepartmentID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var department = context.Department.Find(id);
                if (department == null) return NotFound();

                department.Name = departmentDTO.Name;
                department.Budget = departmentDTO.Budget;
                department.StartDate = departmentDTO.StartDate;
                department.InstructorID = departmentDTO.InstructorID;

                context.SaveChanges();

                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Crear un objeto del departamento
        /// </summary>
        /// <param name="id">C objeto del departamento</param>
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
                var department = context.Department.Find(id);
                if (department == null)
                    return NotFound();

                if (context.Enrollment.Any(x => x.DepartmentID == id))
                    throw new Exception("Dependencia");

                context.Department.Remove(department);
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