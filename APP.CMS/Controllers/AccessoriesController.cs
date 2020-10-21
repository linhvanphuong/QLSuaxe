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
    [Route("phu-tung")]
    public class AccessoriesController : Controller
    {
        private readonly IAccessoriesManager _accessoriesManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public AccessoriesController(IAccessoriesManager accessoriesManager, IHttpContextAccessor httpContextAccessor)
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
                return PartialView("_List", data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen(nameof(PermissionEnum.Create))]
        [HttpGet("tao-moi")]
        public async Task<IActionResult> Create()
        {
            return PartialView("_Create");
        }
        [CustomAuthen(nameof(PermissionEnum.Update))]
        [HttpGet("cap-nhat")]
        public async Task<IActionResult> Update(long id)
        {
            try
            {
                var data = await _accessoriesManager.Find_By_Id(id);
                return PartialView("_Update", data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen]
        [HttpPost("create-or-update")]
        public async Task<IActionResult> Create_Or_Update(Accessories inputModel)
        {
            try
            {
                if (string.IsNullOrEmpty(inputModel.Name))
                {
                    throw new Exception($"Tên {MessageConst.NOT_EMPTY_INPUT}");
                }
                if (inputModel.Id == 0)
                {
                    await _accessoriesManager.Create(inputModel);
                    return Json(new { Result = true, Message = "Thêm mới dữ liệu thành công" });
                }
                else
                {
                    await _accessoriesManager.Update(inputModel);
                    return Json(new { Result = true, Message = "Cập nhật dữ liệu thành công" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen(nameof(PermissionEnum.Delete))]
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _accessoriesManager.Delete(id);
                return Json(new { Result = true });
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
