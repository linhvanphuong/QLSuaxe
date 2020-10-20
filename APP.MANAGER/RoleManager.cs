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
    public interface IRoleManager
    {
        Task<Roles> Create(Roles inputModel);
        Task Update(Roles inputModel);
        Task Delete(long id);
        Task<List<Roles>> Get_List(string name, byte status);
        Task<Roles> Find_By_Id(long id);
        Task<List<Permissions>> Get_List_Permission_By_Role(long roleId);
    }
    public class RoleManager: IRoleManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Roles> Create(Roles inputModel)
        {
            try
            {
                var data =  await _unitOfWork.RolesRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
                if (inputModel.Permissions != null)
                {
                    await DeletePermission(data.Id);
                    await CreatePermission(data);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task DeletePermission(long roleId)
        {
            try
            {
                var data = (await _unitOfWork.PermissionsRepository.FindBy(c => c.RoleId == roleId)).ToList();
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        await _unitOfWork.PermissionsRepository.Delete(item);
                        await _unitOfWork.SaveChange();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task CreatePermission(Roles inputModel)
        {
            try
            {
                foreach (var item in inputModel.Permissions)
                {
                    item.RoleId = inputModel.Id;
                    item.ActionCode = string.IsNullOrEmpty(item.ActionCode) ? "" : item.ActionCode;
                    await _unitOfWork.PermissionsRepository.Add(item);
                    await _unitOfWork.SaveChange();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Permissions>> Get_List_Permission_By_Role(long roleId)
        {
            try
            {
                var data = (await _unitOfWork.PermissionsRepository.FindBy(c => c.RoleId == roleId)).ToList();
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Roles> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.RolesRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Roles>> Get_List(string name, byte status)
        {
            try
            {
                var data = (await _unitOfWork.RolesRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name.ToLower().Trim()))
                                                                          && (status == (int)StatusEnum.All || x.Status == (byte)status)
                                                                          ))).ToList();
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Roles inputModel)
        {
            try
            {
                await _unitOfWork.RolesRepository.Update(inputModel);
                await _unitOfWork.SaveChange();
                if (inputModel.Permissions != null)
                {
                    await DeletePermission(inputModel.Id);
                    await CreatePermission(inputModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
