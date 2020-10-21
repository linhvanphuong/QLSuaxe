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
    public interface IAccessoriesManager
    {
        Task Create(Accessories inputModel);
        Task Update(Accessories inputModel);
        Task Delete(long id);
        Task<List<Accessories>> Get_List(string name);
        Task<Accessories> Find_By_Id(long id);
    }
    public class AccessoriesManager :IAccessoriesManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccessoriesManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(Accessories inputModel)
        {
            try
            {
                await _unitOfWork.AccessoriesRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task Update(Accessories inputModel)
        {
            try
            {
                await _unitOfWork.AccessoriesRepository.Update(inputModel);
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
                await _unitOfWork.AccessoriesRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Accessories>> Get_List(string name)
        {
            try
            {
                var data = (await _unitOfWork.AccessoriesRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))  
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Accessories> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.AccessoriesRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
