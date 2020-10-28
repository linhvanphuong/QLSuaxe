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
        private readonly IMotorManufactureManager _motorManufactureManager;
        private readonly IAccountManager _accountManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public TemporaryBillController(ITemporaryBillManager temporaryBillManager, IHttpContextAccessor httpContextAccessor,
                                       IMotorLiftsManager motorLiftsManager, ICustomersManager customersManager, IMotorTypesManager motorTypesManager,
                                       IEmployeeManager employeeManager, IServicesManager servicesManager, IAccessoriesManager accessoriesManager
                                       ,IMotorManufactureManager motorManufactureManager, IAccountManager accountManager)
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
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string time, byte status)
        {
            try
            {
                var data = await _temporaryBillManager.Get_List_Bill(time, status);
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
                var session = _httpContextAccessor.HttpContext.Session;
                var account = Portal.Utils.SessionExtensions.Get<Accounts>(session, Portal.Utils.SessionExtensions.SessionAccount);
                account.EmployeeName = (await _employeeManager.Find_By_Id(account.EmployeeId)).Name;
                var permission = Portal.Utils.SessionExtensions.Get<List<Permissions>>(_session, Portal.Utils.SessionExtensions.SesscionPermission);
                var path = _httpContextAccessor.HttpContext.Request.Path.Value;
                var currentPagePermission = permission.Where(c => c.MenuUrl.ToLower() == path.ToLower()).ToList();
                ViewData[nameof(PermissionEnum.Create)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Create))) > 0 ? 1 : 0;
                ViewData[nameof(PermissionEnum.Update)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Update))) > 0 ? 1 : 0;
                ViewData[nameof(PermissionEnum.Delete)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Delete))) > 0 ? 1 : 0;
                ViewData["listMotorLift"] = await _motorLiftsManager.Get_List("",(byte)MotorLiftEnum.Active);
                ViewData["listCustomer"] = await _customersManager.Get_List();
                ViewData["listMotorType"] = await _motorTypesManager.Get_List("", (byte)StatusEnum.All, 0);
                ViewData["listServices"] = await _servicesManager.Get_List("", (byte)StatusEnum.Active);
                ViewData["listAccessories"] = await _accessoriesManager.Get_List("");
                ViewData["listKTVien"] = await _accountManager.Get_List_KTV();
                ViewData["timeIn"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                account.EmployeeName  = (await _employeeManager.Find_By_Id(account.EmployeeId)).Name;
                ViewData["txtCreatedBy"] = account;
                return View();
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }

        }
        [HttpGet("service-info")]
        public async Task<IActionResult> Service_Info(long id)
        {
            try
            {
                var data = await _servicesManager.Find_By_Id(id);
                return Json(new { Result = true, Data = data });
            }
            catch(Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [HttpGet("accessory-info")]
        public async Task<IActionResult> Accessory_Info(long id)
        {
            try
            {
                var data = await _accessoriesManager.Find_By_Id(id);
                return Json(new { Result = true, Data = data });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [HttpGet("tao-moi-kh")]
        public async Task<IActionResult> Tao_Moi_KH()
        {
            return PartialView("_CreateCustomer");
        }
        [HttpGet("tao-moi-loai-xe")]
        public async Task<IActionResult> Tao_Moi_Loai_Xe()
        {
            try
            {
                ViewData["listManufac"] = await _motorManufactureManager.Get_List("", (byte)StatusEnum.Active);
                return PartialView("_CreateMotorType");
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            } 
        }
        [HttpGet("cap-nhat")]
        public async Task<IActionResult> Update(long id)
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
        [CustomAuthen]
        [HttpPost("create-or-update")]
        public async Task<IActionResult> Create_Or_Update(TemporaryBill inputModel)
        {
            try
            {
                if (inputModel.Id == 0)
                {
                    var session = _httpContextAccessor.HttpContext.Session;
                    var account = Portal.Utils.SessionExtensions.Get<Accounts>(session, Portal.Utils.SessionExtensions.SessionAccount);
                    inputModel.CreatedBy = account.Id;
                    await _temporaryBillManager.Create(inputModel);
                    return Json(new { Result = true, Message = "Thêm mới dữ liệu thành công" });
                }
                else
                {
                    inputModel.UpdatedTime = DateTime.Now;
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
        [HttpGet("danh-sach-phieu-tam-tinh")]
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
    }
}
