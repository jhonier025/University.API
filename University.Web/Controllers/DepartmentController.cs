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
    public class DepartmentController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        // GET: Department
        public async Task<ActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<DepartmentDTO>>("http://localhost/University.API/",
                "api/Departmensts/",
                null, ApiService.Method.Get);

            var department = (List<DepartmentDTO>)responseDTO.Data;

            ViewData["department"] = new SelectList(department, "DepartmentID", "Name");

            return View(department);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new DepartmentDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(DepartmentDTO departmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<DepartmentDTO>("http://localhost/University.API/",
                    "api/Department/",
                    departmentDTO,
                    ApiService.Method.Post);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(departmentDTO);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<DepartmentDTO>("http://localhost/University.API/",
                  "api/Department/" + id,
                  null,
                  ApiService.Method.Get);

            var department = (DepartmentDTO)responseDTO.Data;

            return View(department);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(DepartmentDTO departmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<DepartmentDTO>("http://localhost/University.API/",
                    "api/Department/" + departmentDTO.DepartmentID,
                    departmentDTO,
                    ApiService.Method.Put);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(departmentDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<DepartmentDTO>("http://localhost/University.API/",
                  "api/Department/" + id,
                  null,
                  ApiService.Method.Delete);

            return RedirectToAction(nameof(Index));


        }
    }
}