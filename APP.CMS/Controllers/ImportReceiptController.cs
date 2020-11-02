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
    [Route("don-hang-nhap")]
    public class ImportReceiptController : Controller
    {
        private readonly IImportReceiptManager _importReceiptManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly ISupplierManager _supplierManager;
        private readonly IAccessoriesManager _accessoriesManager;
        private readonly IAccountManager _accountManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public ImportReceiptController(IImportReceiptManager importReceiptManager, IHttpContextAccessor httpContextAccessor, IEmployeeManager employeeManager
                                      , ISupplierManager supplierManager, IAccessoriesManager accessoriesManager, IAccountManager accountManager)
        {
            this._importReceiptManager = importReceiptManager;
            this._httpContextAccessor = httpContextAccessor;
            this._employeeManager = employeeManager;
            this._supplierManager = supplierManager;
            this._accessoriesManager = accessoriesManager;
            this._accountManager = accountManager;
        }
        [CustomAuthen]
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string createdDate)
        {
            try
            {
                var data = await _importReceiptManager.Get_List(createdDate);
                if (data != null)
                {
                    data = data.OrderByDescending(c => c.CreatedDate).ToList();
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
                var session = _httpContextAccessor.HttpContext.Session;
                var account = Portal.Utils.SessionExtensions.Get<Accounts>(session, Portal.Utils.SessionExtensions.SessionAccount);
                var permission = Portal.Utils.SessionExtensions.Get<List<Permissions>>(_session, Portal.Utils.SessionExtensions.SesscionPermission);
                var path = _httpContextAccessor.HttpContext.Request.Path.Value;
                var currentPagePermission = permission.Where(c => c.MenuUrl.ToLower() == path.ToLower()).ToList();
                ViewData[nameof(PermissionEnum.Create)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Create))) > 0 ? 1 : 0;
                account.EmployeeName = (await _employeeManager.Find_By_Id(account.EmployeeId)).Name;
                ViewData["listSuplier"] = await _supplierManager.Get_List("");
                ViewData["listAccessories"] = await _accessoriesManager.Get_List("");
                ViewData["createdDate"] = DateTime.Now;
                ViewData["txtCreatedBy"] = account;
                return View();
            }
            catch (Exception ex)
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
        [HttpGet("tao-moi-ncc")]
        public async Task<IActionResult> Create_Suplier(long id)
        {
                return PartialView("_CreateSuplier");
        }
        [CustomAuthen(nameof(PermissionEnum.Update))]
        [HttpGet("xem")]
        public async Task<IActionResult> View(long id)
        {
            try
            {
                var data = await _importReceiptManager.Find_By_Id(id);
                ViewData["Suplier"] = await _supplierManager.Find_By_Id(data.SupplierId);
                ViewData["listImport_Accessories"] = await _importReceiptManager.Get_List_Import_Accesories(data.Id);
                var createdBy = await _accountManager.Find_By_Id(data.CreatedBy);
                createdBy.EmployeeName = (await _employeeManager.Find_By_Id(createdBy.EmployeeId)).Name;
                ViewData["txtCreatedBy"] = createdBy;
                return PartialView("View", data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen]
        [HttpPost("create-or-update")]
        public async Task<IActionResult> Create_Or_Update(ImportReceipt inputModel)
        {
            try
            {
                if (inputModel.Id == 0)
                {
                    var session = _httpContextAccessor.HttpContext.Session;
                    var account = Portal.Utils.SessionExtensions.Get<Accounts>(session, Portal.Utils.SessionExtensions.SessionAccount);
                    inputModel.CreatedBy = account.Id;
                    inputModel.CreatedDate = DateTime.Now;
                    await _importReceiptManager.Create(inputModel);
                    return Json(new { Result = true, Message = "Thêm mới dữ liệu thành công" });
                }
                else
                {
                    //await _importReceiptManager.Update(inputModel);
                    return Json(new { Result = true, Message = "Cập nhật dữ liệu thành công" });
                }
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
            var permission = Portal.Utils.SessionExtensions.Get<List<Permissions>>(_session, Portal.Utils.SessionExtensions.SesscionPermission);
            var path = _httpContextAccessor.HttpContext.Request.Path.Value;
            var currentPagePermission = permission.Where(c => c.MenuUrl.ToLower() == path.ToLower()).ToList();
            ViewData[nameof(PermissionEnum.Create)] = currentPagePermission.Count(c => c.ActionCode == (nameof(PermissionEnum.Create))) > 0 ? 1 : 0;
            return View();
        }
    }
}
