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
    [Route("loai-xe")]
    public class MotorTypesController : Controller
    {
        private readonly IMotorTypesManager _motorTypesManager;
        private readonly IMotorManufactureManager _motorManufactureManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public MotorTypesController(IMotorTypesManager motorTypesManager, IMotorManufactureManager motorManufactureManager, IHttpContextAccessor httpContextAccessor)
        {
            this._motorTypesManager = motorTypesManager;
            this._motorManufactureManager = motorManufactureManager;
            this._httpContextAccessor = httpContextAccessor;
        }
        [CustomAuthen]
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string name, byte status,long manufactureId)
        {
            try
            {
                var data = await _motorTypesManager.Get_List(name, status, manufactureId);
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
        [CustomAuthen(nameof(PermissionEnum.Create))]
        [HttpGet("tao-moi")]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewData["listManufac"] = await _motorManufactureManager.Get_List("", (byte)StatusEnum.Active);
                return PartialView("_Create");
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
                var data = await _motorTypesManager.Find_By_Id(id);
                ViewData["listManufac"] = await _motorManufactureManager.Get_List("", (byte)StatusEnum.Active);
                return PartialView("_Update", data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen]
        [HttpPost("create-or-update")]
        public async Task<IActionResult> Create_Or_Update(MotorTypes inputModel)
        {
            try
            {
                if (string.IsNullOrEmpty(inputModel.Name))
                {
                    throw new Exception($"Tên {MessageConst.NOT_EMPTY_INPUT}");
                }
                if (inputModel.Id == 0)
                {
                    var data = await _motorTypesManager.Create(inputModel);
                    return Json(new { Result = true, Message = "Thêm mới dữ liệu thành công",data=data });
                }
                else
                {
                    await _motorTypesManager.Update(inputModel);
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
                await _motorTypesManager.Delete(id);
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
            ViewData["listManufac"] = await _motorManufactureManager.Get_List("", (byte)StatusEnum.Active);
            return View();
        }
    }
}
