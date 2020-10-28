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
    public interface ICustomersManager
    {
        Task<Customers> Create(Customers inputModel);
        Task Update(Customers inputModel);
        Task Delete(long id);
        Task<List<Customers>> Get_List(string name, string phone);
        Task<List<Customers>> Get_List();
        Task<Customers> Find_By_Id(long id);
    }
    public class CustomersManager: ICustomersManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomersManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customers> Create(Customers inputModel)
        {
            try
            {
                var data = await _unitOfWork.CustomersRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
                return data;
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
                await _unitOfWork.CustomersRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customers> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.CustomersRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Customers>> Get_List(string name, string phone)
        {
            try
            {
                var data = (await _unitOfWork.CustomersRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                            && (string.IsNullOrEmpty(phone) || x.Phone.Trim().Contains(phone.Trim()))
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Customers>> Get_List()
        {
            try
            {
                var data = (await _unitOfWork.CustomersRepository.FindBy(x => x.Status == (int)StatusEnum.Active)).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Customers inputModel)
        {
            try
            {
                await _unitOfWork.CustomersRepository.Update(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
