using APP.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using APP.MODELS;
using Portal.Utils;
using System.Linq;

namespace APP.MANAGER
{
    public interface IAccountManager
    {
        Task Create(Accounts inputModel);
        Task Update(Accounts inputModel);
        Task Update_Login_Logout(Accounts inputModel);
        Task Delete(long id);
        Task<List<Accounts>> Get_List(string name, byte status);
        Task<List<Accounts>> Get_List_KTV();
        Task<Accounts> Find_By_Id(long id);
        Task<Accounts> Find_By_UserName(string userName);
        Task<Accounts> Find_By_Id_Ok(long id);
        Task<Accounts> Login(string userName, string password);
        Task<Accounts> UpdateToken(Accounts inputModel);
        Task<Accounts> CheckToken(string token);
    }
    public class AccountManager : IAccountManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(Accounts inputModel)
        {
            try
            {
                inputModel.StatusActing =
                await _unitOfWork.AccountsRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
                if (inputModel.ListRole != null)
                {
                    foreach (var i in inputModel.ListRole)
                    {
                        var inputAccRole = new Account_Roles()
                        {
                            AccountId = inputModel.Id,
                            RoleId = i
                        };
                        await Create_Account_Role(inputAccRole);
                    }
                }            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Create_Account_Role(Account_Roles inputAccRole)
        {
            try
            {
                await _unitOfWork.Account_RolesRepository.Add(inputAccRole);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Delete_Account_Role_By_AccountId(long accountId)
        {
            try
            {
                var data = (await _unitOfWork.Account_RolesRepository.FindBy(c => c.AccountId == accountId)).ToList();
                if (data != null)
                {
                    foreach (var i in data)
                    {
                        await _unitOfWork.Account_RolesRepository.Delete(i);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task Delete(long id)
        {
            try
            {
                var inputModel = await Find_By_Id(id);
                await _unitOfWork.AccountsRepository.Delete(inputModel);
                var data = (await _unitOfWork.Account_RolesRepository.FindBy(c => c.AccountId == id)).ToList();
                if(data != null)
                {
                    foreach (var i in data)
                    {
                        await _unitOfWork.Account_RolesRepository.Delete(i);
                    }
                }
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Accounts> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.AccountsRepository.Get(c => c.Id == id);
                var listRole = (await _unitOfWork.Account_RolesRepository.FindBy(c => c.AccountId == id)).ToList();
                if (listRole != null)
                {
                    data.ListRole = new List<long>();
                    foreach (var i in listRole)
                    {

                        data.ListRole.Add(i.RoleId);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Accounts> Find_By_UserName(string userName)
        {
            try
            {
                var data = await _unitOfWork.AccountsRepository.Get(c => c.UserName.Trim().ToLower() == userName.Trim().ToLower());
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Accounts> Find_By_Id_Ok(long id)
        {
            try
            {
                var data = await _unitOfWork.AccountsRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Accounts>> Get_List(string name, byte status)
        {
            try
            {
                var data = (await _unitOfWork.AccountsRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.UserName.ToLower().Contains(name))
                                                                           && (status == (int)StatusEnum.All || x.Status == (byte)status)
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Accounts>> Get_List_KTV()
        {
            try
            {
                var data = (await _unitOfWork.AccountsRepository.FindBy(x=>x.StatusActing == (byte)AccountStatusEnum.Active)).ToList();
                //list account dang ranh
                List<Accounts> result = new List<Accounts>();
                foreach(var i in data)
                {
                    if (await _unitOfWork.EmployeesRepository.Get(c => c.Id == i.EmployeeId) != null)
                    {
                        i.JobPositionId = (await _unitOfWork.EmployeesRepository.Get(c => c.Id == i.EmployeeId)).JobPositionId;
                        i.EmployeeName = (await _unitOfWork.EmployeesRepository.Get(c => c.Id == i.EmployeeId)).Name;
                    }
                    // list employee theo account
                }
                var jobKTV = await _unitOfWork.JobPositionsRepository.Get(c => c.Name.Trim().ToLower().Contains("kỹ thuật"));
                foreach(var i in data)
                {
                     if(i.JobPositionId == jobKTV.Id)
                     {
                        var acc = i;
                        result.Add(i);
                     }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Accounts inputModel)
        {
            try
            {
                await _unitOfWork.AccountsRepository.Update(inputModel);
                await _unitOfWork.SaveChange();
                await Delete_Account_Role_By_AccountId(inputModel.Id);
                if (inputModel.ListRole != null)
                {
                    foreach (var i in inputModel.ListRole)
                    {
                        var inputAccRole = new Account_Roles()
                        {
                            AccountId = inputModel.Id,
                            RoleId = i
                        };
                        await Create_Account_Role(inputAccRole);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update_Login_Logout(Accounts inputModel)
        {
            try
            {
                await _unitOfWork.AccountsRepository.Update(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Accounts> Login(string userName, string password)
        {
            var data = await _unitOfWork.AccountsRepository.Get(x => x.UserName.Equals(userName)
                                                                && x.Password.Equals(password)
                                                                && x.Status == (byte)StatusEnum.Active);
            if (data == null)
            {

                throw new Exception("Tài khoản hoặc mật khẩu không đúng");
            }
            else
            {
                var listRolePermission = await GetPermissionByAccountId(data.Id);
                data.ListPermissions = listRolePermission;
                var listMenu = (await _unitOfWork.MenusRepository.FindBy(x => true)).ToList();
                var listMenuOfAccount = new List<Menus>();
                foreach (var item in listMenu)
                {
                    var menu = listRolePermission.Find(x => x.MenuId == item.Id);
                    if (menu != null)
                    {
                        listMenuOfAccount.Add(item);
                    }
                }
                data.ListMenu = listMenuOfAccount;
                return data;
            }
        }
        public async Task<List<Permissions>> GetPermissionByAccountId(long accountId)
        {
            try
            {
                var listRole = (await _unitOfWork.Account_RolesRepository.FindBy(x => x.AccountId == accountId)).ToList();
                var listPermissions = new List<Permissions>();
                foreach (var item in listRole)
                {
                    var data = (await _unitOfWork.PermissionsRepository.FindBy(x => x.RoleId == item.RoleId && !string.IsNullOrEmpty(x.ActionCode))).ToList();
                    if (data != null)
                    {
                        foreach (var role_Permission in data)
                        {
                            var linkMenu = await _unitOfWork.MenusRepository.Get(x => x.Id == role_Permission.MenuId);
                            role_Permission.MenuUrl = linkMenu.Url;
                        }
                        listPermissions = listPermissions.Union(data).ToList();
                    }
                }
                return listPermissions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Accounts> CheckToken(string token)
        {

            var now = DateTime.Now.Date;
            var data = await _unitOfWork.AccountsRepository.Get(x => x.Token == token && x.ExpiredToken.Value.Date >= now);
            return data;
        }

        public async Task<Accounts> UpdateToken(Accounts inputModel)
        {
            try
            {
                inputModel.UserName = inputModel.UserName.ToLower();
                await _unitOfWork.AccountsRepository.Update(inputModel);
                await _unitOfWork.SaveChange();
                var data = await _unitOfWork.AccountsRepository.Get(x => x.Id == inputModel.Id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
