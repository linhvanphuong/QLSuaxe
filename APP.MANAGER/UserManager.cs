using APP.MODELS;
using APP.REPOSITORY;
using Microsoft.Extensions.Logging;
using Portal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.MANAGER
{
    public interface IUserManager
    {
        Task<List<Users>> Get_List(string userName, string fullName, int status, int pageSize, int pageNumber);
    }
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Users> _logger;
        public UserManager(IUnitOfWork unitOfWork, ILogger<Users> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<List<Users>> Get_List(string userName = "", string fullName = "", int status = -1, int pageSize = 10, int pageNumber = 0)
        {
            try
            {
                var data = (await _unitOfWork.UserRepository.FindBy(x =>((string.IsNullOrEmpty(userName) || x.UserName.ToLower().Contains(userName)))
                                                                    )).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
