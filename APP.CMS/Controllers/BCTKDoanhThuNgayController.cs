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
    [Route("thong-ke-doanh-thu-ngay")]
    public class BCTKDoanhThuNgayController : Controller
    {
        private readonly ITemporaryBillManager _temporaryBillManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public BCTKDoanhThuNgayController(ITemporaryBillManager temporaryBillManager, IHttpContextAccessor httpContextAccessor)
        {
            this._temporaryBillManager = temporaryBillManager;
            this._httpContextAccessor = httpContextAccessor;
        }
        [CustomAuthen]
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string time)
        {
            try
            {
                if (string.IsNullOrEmpty(time))
                {
                    time = DateTime.Now.Date.ToString();
                }
                var data = await _temporaryBillManager.Get_List_Bill(time,(byte)BillStatus.Bill);
                if (data != null)
                {
                    data = data.OrderBy(c => c.TimeOut).ToList();
                    decimal tongcong = 0;
                    foreach(var i in data)
                    {
                        i.tongTien = 0;
                        var listSv = await _temporaryBillManager.Get_List_TemporaryBill_Service(i.Id);
                        foreach(var j in listSv)
                        {
                            i.tongTien += (decimal)j.ServicePrice;
                        }
                        var listAcc = await _temporaryBillManager.Get_List_TemporaryBill_Accesary(i.Id);
                        foreach (var k in listAcc)
                        {
                            i.tongTien += (decimal)k.AccesaryPrice * k.Quantity;
                        }
                        tongcong += i.tongTien;
                    }
                    ViewData["tongcong"] = tongcong;
                    ViewData["ngay"] = time;
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
            return View();
        }
    }
}
