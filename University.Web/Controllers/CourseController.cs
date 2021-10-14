using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        // GET: Course
        public async Task<ActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<CourseDTO>>("http://localhost/University.API/",
                "api/Course/",
                null, ApiService.Method.Get);

            var course = (List<CourseDTO>)responseDTO.Data;

            ViewData["course"] = new SelectList(course, "CourseID", "Title");

            return View(course);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CourseDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<CourseDTO>("http://localhost/University.API/",
                    "api/Course/",
                    courseDTO,
                    ApiService.Method.Post);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(courseDTO);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<CourseDTO>("http://localhost/University.API/",
                  "api/course/" + id,
                  null,
                  ApiService.Method.Get);

            var course = (CourseDTO)responseDTO.Data;

            return View(course);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<CourseDTO>("http://localhost/University.API/",
                    "api/Course/" + courseDTO.CourseID,
                    courseDTO,
                    ApiService.Method.Put);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(courseDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<CourseDTO>("http://localhost/University.API/",
                  "api/Course/" + id,
                  null,
                  ApiService.Method.Delete);

            return RedirectToAction(nameof(Index));


        }
    }
}