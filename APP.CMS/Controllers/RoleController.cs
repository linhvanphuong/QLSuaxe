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
    [Route("nhom-quyen")]
    public class RoleController : Controller
    {
        private readonly IRoleManager _roleManager;
        private readonly IMenuManager _menuManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public RoleController(IRoleManager roleManager, IMenuManager menuManager, IHttpContextAccessor httpContextAccessor)
        {
            this._roleManager = roleManager;
            this._menuManager = menuManager;
            this._httpContextAccessor = httpContextAccessor;
        }
        [CustomAuthen]
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string name, byte status)
        {
            try
            {
                var data = await _roleManager.Get_List(name, status);
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
            try
            {
                ViewData["listMenu"] = await _menuManager.Get_List_Child();
                ViewData["listAllMenu"] = await _menuManager.Get_List_Menu();
                var values = Enum.GetValues(typeof(PermissionEnum));
                List<LookupModel> data = new List<LookupModel>();
                foreach (var item in values)
                {
                    data.Add(new LookupModel()
                    {
                        Title = Extensions.GetEnumDescription((PermissionEnum)item),
                        Value = item.ToString()
                    });
                }
                ViewData["listPermission"] = data;
                return View("Create");
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }

        }
        [CustomAuthen(nameof(PermissionEnum.Update))]
        [HttpGet("cap-nhat")]
        public async Task<IActionResult> Update(long id)
        {
            try
            {
                ViewData["listMenu"] = await _menuManager.Get_List_Child();
                ViewData["listAllMenu"] = await _menuManager.Get_List_Menu();
                var values = Enum.GetValues(typeof(PermissionEnum));
                List<LookupModel> data = new List<LookupModel>();
                foreach (var item in values)
                {
                    data.Add(new LookupModel()
                    {
                        Title = Extensions.GetEnumDescription((PermissionEnum)item),
                        Value = item.ToString()
                    });
                }
                ViewData["listPermission"] = data;
                var result = await _roleManager.Find_By_Id(id);
                return View("Update", result);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen]
        [HttpPost("create-or-update")]
        public async Task<IActionResult> Create_Or_Update(Roles inputModel)
        {
            try
            {
                if (string.IsNullOrEmpty(inputModel.Name))
                {
                    throw new Exception($"Tên {MessageConst.NOT_EMPTY_INPUT}");
                }
                if (inputModel.Id == 0)
                {
                    await _roleManager.Create(inputModel);
                    return Json(new { Result = true, Message = "Thêm mới dữ liệu thành công" });
                }
                else
                {
                    await _roleManager.Update(inputModel);
                    return Json(new { Result = true, Message = "Cập nhật dữ liệu thành công" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [HttpGet("get-list-permission-by-role")]
        public async Task<IActionResult> Get_List_Permission_By_Role(long roleId)
        {
            try
            {
                var data = await _roleManager.Get_List_Permission_By_Role(roleId);
                return Json(data);
            }
            catch(Exception ex)
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
                await _roleManager.Delete(id);
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
