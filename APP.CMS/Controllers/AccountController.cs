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
    [Route("tai-khoan")]
    public class AccountController : Controller
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly IAccountManager _accountManager;
        private readonly IRoleManager _roleManager;
        private readonly IJobPositionsManager _jobPositionsManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public AccountController(IEmployeeManager employeeManager, IHttpContextAccessor httpContextAccessor,
                                 IAccountManager accountManager, IRoleManager roleManager, IJobPositionsManager jobPositionsManager)
        {
            this._employeeManager = employeeManager;
            this._httpContextAccessor = httpContextAccessor;
            this._accountManager = accountManager;
            this._roleManager = roleManager;
            this._jobPositionsManager = jobPositionsManager;
        }
        [HttpGet("dang-nhap")]
        public async Task<IActionResult> Login()
        {
            return View("Login");
        }
        [HttpGet("dang-xuat")]
        public async Task<IActionResult> Logout()
        {
            Portal.Utils.SessionExtensions.Set<Accounts>(_session, Portal.Utils.SessionExtensions.SessionAccount, null);
            Portal.Utils.SessionExtensions.Set<List<Permissions>>(_session, Portal.Utils.SessionExtensions.SesscionPermission, null);
            return View("Login");
        }
        [HttpPost("dang-nhap")]
        public async Task<IActionResult> Login(Accounts inputModel)
        {
            try
            {

                var account = await _accountManager.Login(inputModel.UserName, inputModel.Password);
                if(account == null)
                {
                    return Json(new { Result = false, Message = "Tài khoản hoặc mật khẩu không đúng" });
                }
                if (account != null)
                {
                    account.Token = CreateToken();
                    account.ExpiredToken = DateTime.Now.AddHours(12);
                    await _accountManager.UpdateToken(account);
                    var employ = await _employeeManager.Find_By_Id(account.EmployeeId);
                    var job = await _jobPositionsManager.Find_By_Id(employ.JobPositionId);
                    account.EmployeeName = employ.Name;
                    account.JobPositionName = job.Name;
                    Portal.Utils.SessionExtensions.Set<Accounts>(_session, Portal.Utils.SessionExtensions.SessionAccount, account);
                    Portal.Utils.SessionExtensions.Set<List<Permissions>>(_session, Portal.Utils.SessionExtensions.SesscionPermission, account.ListPermissions);
                    return Json(new { Result = true, Message = "Đăng nhập thành công" });
                }
                return Json(new { Result = false, Message = "Đăng nhập không thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        private string CreateToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            Console.WriteLine(token);
            return token;
        }
        [CustomAuthen]
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string name, byte status)
        {
            try
            {
                var data = await _accountManager.Get_List(name, status);
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
                ViewData["listEmployee"] = await _employeeManager.Get_List();
                ViewData["listRole"] = await _roleManager.Get_List("", (byte)StatusEnum.Active);
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
                ViewData["listEmployee"] = await _employeeManager.Get_List();
                ViewData["listRole"] = await _roleManager.Get_List("", (byte)StatusEnum.Active);
                var data = await _accountManager.Find_By_Id(id);
                return PartialView("_Update", data);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        [CustomAuthen]
        [HttpPost("create-or-update")]
        public async Task<IActionResult> Create_Or_Update(Accounts inputModel)
        {
            try
            {
                if (string.IsNullOrEmpty(inputModel.UserName))
                {
                    throw new Exception($"Tên {MessageConst.NOT_EMPTY_INPUT}");
                }
                if (string.IsNullOrEmpty(inputModel.Password))
                {
                    throw new Exception($"Mật khẩu {MessageConst.NOT_EMPTY_INPUT}");
                }
                if (inputModel.Id == 0)
                {
                    inputModel.CreatedDate = DateTime.Now;
                    await _accountManager.Create(inputModel);
                    return Json(new { Result = true, Message = "Thêm mới dữ liệu thành công" });
                }
                else
                {
                    inputModel.UpdatedDate = DateTime.Now;
                    await _accountManager.Update(inputModel);
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
                await _accountManager.Delete(id);
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
