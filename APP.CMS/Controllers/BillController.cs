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
    [Route("danh-sach-hoa-don")]
    public class BillController : Controller
    {
        private readonly ITemporaryBillManager _temporaryBillManager;
        private readonly IMotorLiftsManager _motorLiftsManager;
        private readonly ICustomersManager _customersManager;
        private readonly IMotorTypesManager _motorTypesManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IServicesManager _servicesManager;
        private readonly IAccessoriesManager _accessoriesManager;
        private readonly IMotorManufactureManager _motorManufactureManager;
        private readonly IAccountManager _accountManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public BillController(ITemporaryBillManager temporaryBillManager, IHttpContextAccessor httpContextAccessor,
                                       IMotorLiftsManager motorLiftsManager, ICustomersManager customersManager, IMotorTypesManager motorTypesManager,
                                       IEmployeeManager employeeManager, IServicesManager servicesManager, IAccessoriesManager accessoriesManager
                                       , IMotorManufactureManager motorManufactureManager, IAccountManager accountManager)
        {
            this._temporaryBillManager = temporaryBillManager;
            this._motorLiftsManager = motorLiftsManager;
            this._customersManager = customersManager;
            this._motorTypesManager = motorTypesManager;
            this._employeeManager = employeeManager;
            this._servicesManager = servicesManager;
            this._accessoriesManager = accessoriesManager;
            this._httpContextAccessor = httpContextAccessor;
            this._motorManufactureManager = motorManufactureManager;
            this._accountManager = accountManager;
        }
        [CustomAuthen]
        [HttpGet("danh-sach-hoa-don")]
        public async Task<IActionResult> Index()
        {
            var permission = Portal.Utils.SessionExtensions.Get<List<Permissions>>(_session, Portal.Utils.SessionExtensions.SesscionPermission);
            var path = _httpContextAccessor.HttpContext.Request.Path.Value;
            var currentPagePermission = permission.Where(c => c.MenuUrl.ToLower() == path.ToLower()).ToList();
            ViewData[nameof(PermissionEnum.Create)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Create))) > 0 ? 1 : 0;
            ViewData[nameof(PermissionEnum.Update)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Update))) > 0 ? 1 : 0;
            ViewData[nameof(PermissionEnum.Delete)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Delete))) > 0 ? 1 : 0;
            return View();
        }
        [CustomAuthen]
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string time)
        {
            try
            {
                var data = await _temporaryBillManager.Get_List_Bill(time, 3);
                return PartialView("_List", data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen]
        [HttpGet("xem")]
        public async Task<IActionResult> View(long id)
        {
            try
            {
                var permission = Portal.Utils.SessionExtensions.Get<List<Permissions>>(_session, Portal.Utils.SessionExtensions.SesscionPermission);
                var path = _httpContextAccessor.HttpContext.Request.Path.Value;
                var currentPagePermission = permission.Where(c => c.MenuUrl.ToLower() == path.ToLower()).ToList();
                ViewData[nameof(PermissionEnum.Create)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Create))) > 0 ? 1 : 0;
                ViewData[nameof(PermissionEnum.Update)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Update))) > 0 ? 1 : 0;
                ViewData[nameof(PermissionEnum.Delete)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Delete))) > 0 ? 1 : 0;
                var data = await _temporaryBillManager.Find_By_Id(id);
                ViewData["MotorLift"] = await _motorLiftsManager.Find_By_Id(data.MotorLiftId);
                ViewData["Customer"] = await _customersManager.Find_By_Id(data.CustomerId);
                ViewData["MotorType"] = await _motorTypesManager.Find_By_Id(data.MotorTypeId);
                ViewData["listServices"] = await _servicesManager.Get_List("", (byte)StatusEnum.Active);
                ViewData["listAccessories"] = await _accessoriesManager.Get_List("");
                ViewData["listKTVien"] = await _accountManager.Get_List_KTV();
                ViewData["timeIn"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                var session = _httpContextAccessor.HttpContext.Session;
                var account = Portal.Utils.SessionExtensions.Get<Accounts>(session, Portal.Utils.SessionExtensions.SessionAccount);
                account.EmployeeName = (await _employeeManager.Find_By_Id(account.EmployeeId)).Name;
                var createdBy = await _accountManager.Find_By_Id_Ok(data.CreatedBy);
                createdBy.EmployeeName = (await _employeeManager.Find_By_Id(createdBy.EmployeeId)).Name;
                ViewData["CreatedBy"] = createdBy;
                var updatedBy = await _accountManager.Find_By_Id_Ok(data.UpdatedBy);
                if (updatedBy != null)
                {
                    updatedBy.EmployeeName = (await _employeeManager.Find_By_Id(updatedBy.EmployeeId)).Name;
                    ViewData["UpdatedBy"] = updatedBy;
                }
                else
                {
                    ViewData["UpdatedBy"] = null;
                }
                var listServicesCreated = await _temporaryBillManager.Get_List_TemporaryBill_Service(data.Id);
                if (listServicesCreated != null)
                {
                    foreach (var i in listServicesCreated)
                    {
                        i.ServiceName = (await _servicesManager.Find_By_Id(i.ServiceId)).Name;
                    }
                }
                ViewData["listServicesCreated"] = listServicesCreated;
                var listAccessoriesCreated = await _temporaryBillManager.Get_List_TemporaryBill_Accesary(data.Id);
                if (listAccessoriesCreated != null)
                {
                    foreach (var i in listAccessoriesCreated)
                    {
                        var acc = (await _accessoriesManager.Find_By_Id(i.AccesaryId));
                        i.AccesaryName = acc.Name;
                        i.MaxQuantity = acc.Quantity + i.Quantity;
                        i.Unit = acc.Unit;
                        i.ThanhTien = i.Quantity * i.AccesaryPrice;
                    }

                }
                ViewData["listAccessoriesCreated"] = listAccessoriesCreated;
                return View(data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
    }
}
