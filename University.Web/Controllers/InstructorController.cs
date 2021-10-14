using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        // GET: Instructor
        public async Task<ActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<InstructorDTO>>("http://localhost/University.API/",
                "api/Instructor/",
                null, ApiService.Method.Get);

            var instructor = (List<InstructorDTO>)responseDTO.Data;

            ViewData["Instructor"] = new SelectList(instructor, "DepartmentID", "Name");

            return View(instructor);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new InstructorDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(InstructorDTO instructorDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<InstructorDTO>("http://localhost/University.API/",
                    "api/Instructor/",
                    instructorDTO,
                    ApiService.Method.Post);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(instructorDTO);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<InstructorDTO>("http://localhost/University.API/",
                  "api/Instructor/" + id,
                  null,
                  ApiService.Method.Get);

            var instructor = (InstructorDTO)responseDTO.Data;

            return View(instructor);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(InstructorDTO instructorDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<InstructorDTO>("http://localhost/University.API/",
                    "api/Instructor/" + instructorDTO.ID,
                    instructorDTO,
                    ApiService.Method.Put);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(instructorDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<InstructorDTO>("http://localhost/University.API/",
                  "api/Instructor/" + id,
                  null,
                  ApiService.Method.Delete);

            return RedirectToAction(nameof(Index));


        }
    }
}