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
    public class OfficeController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        // GET: Office
        public async Task<ActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<OfficeDTO>>("http://localhost/University.API/",
                "api/Office/",
                null, ApiService.Method.Get);

            var office = (List<OfficeDTO>)responseDTO.Data;

            ViewData["Office"] = new SelectList(office, "InstructorID", "Location");

            return View(office);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new OfficeDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(OfficeDTO officeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<OfficeDTO>("http://localhost/University.API/",
                    "api/Office/",
                    officeDTO,
                    ApiService.Method.Post);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(officeDTO);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<OfficeDTO>("http://localhost/University.API/",
                  "api/Office/" + id,
                  null,
                  ApiService.Method.Get);

            var office = (OfficeDTO)responseDTO.Data;

            return View(office);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(OfficeDTO officeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<OfficeDTO>("http://localhost/University.API/",
                    "api/Instructor/" + officeDTO.Location,
                    officeDTO,
                    ApiService.Method.Put);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(officeDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<OfficeDTO>("http://localhost/University.API/",
                  "api/Office/" + id,
                  null,
                  ApiService.Method.Delete);

            return RedirectToAction(nameof(Index));


        }
    }
}