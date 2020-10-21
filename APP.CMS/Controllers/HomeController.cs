using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using APP.MODELS;
using Portal.Utils;
using APP.CMS.Models;
using APP.MANAGER;

namespace APP.CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IMotorLiftsManager _motorLiftsManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public HomeController(IConfiguration config, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor,
                              IMotorLiftsManager motorLiftsManager)
        {
            this._config = config;
            this._motorLiftsManager = motorLiftsManager;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [CustomAuthen("NoCheck")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var account = Portal.Utils.SessionExtensions.Get<Accounts>(_session, Portal.Utils.SessionExtensions.SessionAccount);
                if (account != null)
                {
                    ViewData["listMotorLift"] = await _motorLiftsManager.Get_List();
                    return View();
                }
                return RedirectToAction("dang-nhap", "tai-khoan");
            }
            catch(Exception ex)
            {
                return RedirectToAction("dang-nhap", "tai-khoan");
            }      
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
