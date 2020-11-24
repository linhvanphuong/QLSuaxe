using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APP.MANAGER;
using Portal.Utils;
using APP.MODELS;
using Microsoft.AspNetCore.Http;

namespace APP.CMS.Controllers
{
    [Route("ton-kho")]
    public class BCTKTonKhoController : Controller
    {
        private readonly IAccessoriesManager _accessoriesManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public BCTKTonKhoController(IAccessoriesManager accessoriesManager, IHttpContextAccessor httpContextAccessor)
        {
            this._accessoriesManager = accessoriesManager;
            this._httpContextAccessor = httpContextAccessor;
        }
        [CustomAuthen]
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string name)
        {
            try
            {
                var data = await _accessoriesManager.Get_List(name);
                if (data != null)
                {
                    data = data.OrderByDescending(c => c.Id).ToList();
                }
                return PartialView("_List", data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen]
        [HttpGet("danh-sach")]
        public async Task<IActionResult> Index()
        {
            //var permission = Portal.Utils.SessionExtensions.Get<List<Permissions>>(_session, Portal.Utils.SessionExtensions.SesscionPermission);
            //var path = _httpContextAccessor.HttpContext.Request.Path.Value;
            //var currentPagePermission = permission.Where(c => c.MenuUrl.ToLower() == path.ToLower()).ToList();
            //ViewData[nameof(RolesEnum.Create)] = currentPagePermission.Count(c => c.ActionCode == (nameof(RolesEnum.Create))) > 0 ? 1 : 0;
            //ViewData[nameof(RolesEnum.Update)] = currentPagePermission.Count(c => c.ActionCode == (nameof(RolesEnum.Update))) > 0 ? 1 : 0;
            //ViewData[nameof(RolesEnum.Delete)] = currentPagePermission.Count(c => c.ActionCode == (nameof(RolesEnum.Delete))) > 0 ? 1 : 0;
            return View();
        }
    }
}
