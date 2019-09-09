using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesPardinho.Web.Site.Controllers
{
    public class MembersController : Controller
    {
        

        public IActionResult Index()
        {
            if (!HttpContext.Session.Validate())
            {
                HttpContext.Session.SetString("login-fail", "");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}