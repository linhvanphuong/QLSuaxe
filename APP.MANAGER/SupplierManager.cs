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
    public interface ISupplierManager
    {
        Task Create(Supplier inputModel);
        Task Update(Supplier inputModel);
        Task Delete(long id);
        Task<List<Supplier>> Get_List(string name);
        Task<Supplier> Find_By_Id(long id);
    }
    public class SupplierManager : ISupplierManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(Supplier inputModel)
        {
            try
            {
                await _unitOfWork.SupplierRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task Update(Supplier inputModel)
        {
            try
            {
                await _unitOfWork.SupplierRepository.Update(inputModel);
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
                await _unitOfWork.SupplierRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Supplier>> Get_List(string name)
        {
            try
            {
                var data = (await _unitOfWork.SupplierRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Supplier> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.SupplierRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
