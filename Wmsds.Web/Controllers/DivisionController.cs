using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wmsds.Bll.Common;
using Wmsds.Entities.Common;

namespace Wmsds.Web.Controllers
{
    public class DivisionController : Controller
    {
        
        public async Task<ActionResult> Index() //Record Select  
        {
            ICommonService commonService = new CommonService();
            var res = await commonService.GetAllDivisions();
            return View(res);
        }

        [HttpGet]
        public PartialViewResult Create()   //Insert PartialView  
        {
            return PartialView(new Division());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Division division) // Record Insert  
        {
            ICommonService commonService = new CommonService();
            var res = await commonService.AddDivision(division);
            return Json(division, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<PartialViewResult> Edit(int divisionId)  // Update PartialView  
        {
            ICommonService commonService = new CommonService();
            var res = await commonService.GetDivisionById(divisionId);
            return PartialView(res);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(Division division)  // Record Update 
        {
            ICommonService commonService = new CommonService();
            var res = await commonService.UpdateDivision(division);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int divisionId)
        {
            //ICommonService commonService = new CommonService();
            //var res = await commonService.DeleteDivisionById(divisionId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}