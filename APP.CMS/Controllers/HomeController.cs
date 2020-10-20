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

namespace APP.CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public HomeController(IConfiguration config, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            this._config = config;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [CustomAuthen("NoCheck")]
        public IActionResult Index()
        {
            var account = Portal.Utils.SessionExtensions.Get<Accounts>(_session, Portal.Utils.SessionExtensions.SessionAccount);
            if (account != null)
            {
                return View();
            }
            return RedirectToAction("dang-nhap", "tai-khoan");
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
