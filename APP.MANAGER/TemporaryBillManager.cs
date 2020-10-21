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
    public interface ITemporaryBillManager
    {
        Task Create(TemporaryBill inputModel);
        Task Update(TemporaryBill inputModel);
        Task Delete(long id);
        //Task<List<TemporaryBill>> Get_List(string name);
        Task<TemporaryBill> Find_By_Id(long id);
    }
    public class TemporaryBillManager : ITemporaryBillManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public TemporaryBillManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(TemporaryBill inputModel)
        {
            try
            {
                var data = await _unitOfWork.TemporaryBillRepository.Add(inputModel);
                if (inputModel.ListServices != null)
                {
                    await CreateBill_Service(data);
                }
                if (inputModel.ListAccessories != null)
                {
                    await CreateBill_Accessories(data);
                }
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async Task CreateBill_Service(TemporaryBill inputModel)
        {
            try
            {
                foreach (var item in inputModel.ListServices)
                {
                    var data = new TemporaryBill_Service();
                    data.TemporaryBillId = inputModel.Id;
                    data.ServiceId = item;
                    await _unitOfWork.TemporaryBill_ServiceRepository.Add(data);
                    await _unitOfWork.SaveChange();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private async Task CreateBill_Accessories(TemporaryBill inputModel)
        {
            try
            {
                foreach (var item in inputModel.ListServices)
                {
                    var data = new TemporaryBill_Accesary();
                    data.TemporaryBillId = inputModel.Id;
                    data.AccesaryId = item;
                    await _unitOfWork.TemporaryBill_AccesaryRepository.Add(data);
                    await _unitOfWork.SaveChange();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(TemporaryBill inputModel)
        {
            try
            {
                await _unitOfWork.TemporaryBillRepository.Update(inputModel);
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
                await _unitOfWork.TemporaryBillRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<List<TemporaryBill>> Get_List(string name)
        //{
        //    try
        //    {
        //        //var data = (await _unitOfWork.TemporaryBillRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
        //        //                                                           ))).ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<TemporaryBill> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.TemporaryBillRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
