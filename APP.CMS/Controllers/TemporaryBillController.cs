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
    [Route("hoa-don")]
    public class TemporaryBillController : Controller
    {
        private readonly ITemporaryBillManager _temporaryBillManager;
        private readonly IMotorLiftsManager _motorLiftsManager;
        private readonly ICustomersManager _customersManager;
        private readonly IMotorTypesManager _motorTypesManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IServicesManager _servicesManager;
        private readonly IAccessoriesManager _accessoriesManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public TemporaryBillController(ITemporaryBillManager temporaryBillManager, IHttpContextAccessor httpContextAccessor,
                                       IMotorLiftsManager motorLiftsManager, ICustomersManager customersManager, IMotorTypesManager motorTypesManager,
                                       IEmployeeManager employeeManager, IServicesManager servicesManager, IAccessoriesManager accessoriesManager)
        {
            this._temporaryBillManager = temporaryBillManager;
            this._motorLiftsManager = motorLiftsManager;
            this._customersManager = customersManager;
            this._motorTypesManager = motorTypesManager;
            this._employeeManager = employeeManager;
            this._servicesManager = servicesManager;
            this._accessoriesManager = accessoriesManager;
            this._httpContextAccessor = httpContextAccessor;
        }
        //[CustomAuthen]
        //[HttpGet("get-list")]
        //public async Task<IActionResult> Get_List(string name, string phone)
        //{
        //    try
        //    {
        //        var data = await _temporaryBillManager.Get_List(name, phone);
        //        return PartialView("_List", data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = false, Message = ex.Message });
        //    }
        //}
        [CustomAuthen(nameof(PermissionEnum.Create))]
        [HttpGet("tao-moi")]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewData["listMotorLift"] = await _motorLiftsManager.Get_List("",(byte)MotorLiftEnum.Active);
                ViewData["listCustomer"] = await _customersManager.Get_List();
                ViewData["listMotorType"] = await _motorTypesManager.Get_List("", (byte)StatusEnum.All, 0);
                ViewData["listServices"] = await _servicesManager.Get_List("", (byte)StatusEnum.Active);
                ViewData["listAccessories"] = await _accessoriesManager.Get_List("");
                ViewData["timeIn"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                var session = _httpContextAccessor.HttpContext.Session;
                var account = Portal.Utils.SessionExtensions.Get<Accounts>(session, Portal.Utils.SessionExtensions.SessionAccount);
                account.EmployeeName  = (await _employeeManager.Find_By_Id(account.EmployeeId)).Name;
                ViewData["txtCreatedBy"] = account;
                return View();
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
                var data = await _temporaryBillManager.Find_By_Id(id);
                return PartialView("_Update", data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen]
        [HttpPost("create-or-update")]
        public async Task<IActionResult> Create_Or_Update(TemporaryBill inputModel)
        {
            try
            {
                if (inputModel.Id == 0)
                {
                    await _temporaryBillManager.Create(inputModel);
                    return Json(new { Result = true, Message = "Thêm mới dữ liệu thành công" });
                }
                else
                {
                    await _temporaryBillManager.Update(inputModel);
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
                await _temporaryBillManager.Delete(id);
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
