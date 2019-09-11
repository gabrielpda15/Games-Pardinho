using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GamesPardinho.Web.Site.Models;
using GamesPardinho.Web.Models.Entities.Security;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GamesPardinho.Web.Extensions;
using Microsoft.AspNetCore.WebUtilities;

namespace GamesPardinho.Web.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ActionName("Test")]
        public async Task<IActionResult> Test()
        {
            throw new Exception();

            var r = await ApiManager.CheckRole("Administrador");
            return r.ReturnCase<IActionResult>(Ok("Test Ok!"), Unauthorized("Not Ok!"), Unauthorized("Usuario errado!"));
        }

        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        public async Task<IActionResult> Teams()
        {
            return await Task.FromResult(View(nameof(Teams)));
        }

        public async Task<IActionResult> Tournaments()
        {
            return await Task.FromResult(View(nameof(Tournaments)));
        }

        [HttpGet]
        [ActionName("Login")]
        public IActionResult Login()
        {
            return PartialView("_Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ApiManager.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                await ApiManager.Login(new User() { Username = username, Password = password });
                HttpContext.Session.SetString("auth-token", JsonConvert.SerializeObject(ApiManager.token));
            }
            catch (ApiManager.AuthorizationException ex)
            {
                HttpContext.Session.SetString("login-fail", ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            return View(new ErrorViewModel { Code = code, Name = ReasonPhrases.GetReasonPhrase(code), Description = "Breve Descrição" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Exception()
        {
            return View(new ErrorViewModel { Code = 500, Name = ReasonPhrases.GetReasonPhrase(500), Description = "" });
        }
    }
}
