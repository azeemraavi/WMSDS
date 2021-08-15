using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wmsds.Bll.Account;

namespace Wmsds.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> DoLogin(FormCollection formCollection)
        {
            string userName = formCollection["userName"];
            string password = formCollection["password"];

            IUserAccount userService = new UserAccountService();
            var wmsdsResponse = await userService.DoLogin(userName, password);
            if (wmsdsResponse.ResponseCode == Entities.EnumStatus.Success)
            {
                Session["userInfo"] = wmsdsResponse.DataObject;
                return Redirect("/Home/Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login");
        }
    }
}