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
    public interface IMenuManager
    {
        Task Create(Menus inputModel);
        Task Update(Menus inputModel);
        Task Delete(long id);
        Task<List<Menus>> Get_List(string name, byte status,long parentId);
        Task<List<Menus>> Get_List_Parent_Menu();
        Task<List<Menus>> Get_List_Menu();
        Task<List<Menus>> Get_Child(long parentId);
        Task<List<Menus>> Get_List_Child();
        Task<Menus> Find_By_Id(long id);
    }
    public class MenuManager: IMenuManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(Menus inputModel)
        {
            try
            {
                await _unitOfWork.MenusRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(long id)
        {
            try
            {
                var inputModel = await Find_By_Id(id);
                await _unitOfWork.MenusRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Menus> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.MenusRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Menus>> Get_List(string name, byte status, long parentId)
        {
            try
            {
                var data = (await _unitOfWork.MenusRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                           && (status == (int)StatusEnum.All || x.Status == (byte)status)
                                                                           && (parentId == -1 || x.ParentId == parentId)
                                                                           ))).ToList();
                foreach(var item in data)
                {
                    if (item.ParentId != 0)
                    {
                        item.ParentName = (await Find_By_Id(item.ParentId)).Name;
                    }  
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Menus>> Get_List_Child()
        {
            try
            {
                var data = (await _unitOfWork.MenusRepository.FindBy(x => x.ParentId == 0)).ToList();
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        item.ListChild = await Get_Child(item.Id);
                    }
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Menus>> Get_Child(long parentId)
        {
            try
            {
                var data = (await _unitOfWork.MenusRepository.FindBy(x => x.ParentId == parentId)).ToList();
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        item.ListChild = await Get_Child(item.Id);
                    }
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Menus>> Get_List_Parent_Menu()
        {
            try
            {
                var data = (await _unitOfWork.MenusRepository.FindBy(x => x.Status == (byte)StatusEnum.Active && x.ParentId == 0)).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Menus>> Get_List_Menu()
        {
            try
            {
                var data = (await _unitOfWork.MenusRepository.FindBy(x => x.Status == (byte)StatusEnum.Active)).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Menus inputModel)
        {
            try
            {
                await _unitOfWork.MenusRepository.Update(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
