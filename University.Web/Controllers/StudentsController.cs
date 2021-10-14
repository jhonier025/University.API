using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        // GET: Students
        public async Task<ActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<StudentDTO>>("http://localhost/University.API/",
                "api/Students/",
                null, ApiService.Method.Get);

            var students = (List<StudentDTO>)responseDTO.Data;

            ViewData["students"] = new SelectList(students, "ID","FullName");

            return View(students);
        } 

        [HttpGet]
        public ActionResult Create()
        {
            return View(new StudentDTO());
        }
            
          
        [HttpPost]
        public async Task<ActionResult> Create(StudentDTO studentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<StudentDTO>("http://localhost/University.API/",
                    "api/Students/",
                    studentDTO,
                    ApiService.Method.Post);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(studentDTO);
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<StudentDTO>("http://localhost/University.API/",
                  "api/Students/" + id,
                  null,
                  ApiService.Method.Get);

            var student = (StudentDTO)responseDTO.Data;

            return View(student);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(StudentDTO studentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<StudentDTO>("http://localhost/University.API/",
                    "api/Students/" + studentDTO.ID,
                    studentDTO,
                    ApiService.Method.Put);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(studentDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<StudentDTO>("http://localhost/University.API/",
                  "api/Students/" + id,
                  null,
                  ApiService.Method.Delete);

                  return RedirectToAction(nameof(Index));

           
        }
    }
}