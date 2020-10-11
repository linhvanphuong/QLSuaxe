using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.MANAGER;
using Microsoft.AspNetCore.Mvc;
using Portal.Utils;

namespace APP.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager Accounts)
        {
            this.userManager = Accounts;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetList(string userName, string fullName, int status = -1, int pageSize = 10, int pageNumber = 0)
        {
            try
            {
                var data = await userManager.Get_List(userName, fullName, status, pageSize, pageNumber);
                if (data == null)
                {
                    throw new Exception(MessageConst.DATA_NOT_FOUND);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
