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
    public interface IServicesManager
    {
        Task Create(Services inputModel);
        Task Update(Services inputModel);
        Task Delete(long id);
        Task<List<Services>> Get_List(string name, byte status);
        Task<Services> Find_By_Id(long id);
    }
    public class ServicesManager: IServicesManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServicesManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(Services inputModel)
        {
            try
            {
                await _unitOfWork.ServicesRepository.Add(inputModel);
                var hisPrice = new ServicePriceHistory();
                hisPrice.ServiceId = inputModel.Id;
                hisPrice.Price = inputModel.Price;
                hisPrice.FromDate = DateTime.Now;
                await _unitOfWork.ServicePriceHistoryRepository.Add(hisPrice);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task Update(Services inputModel)
        {
            try
            {
                var data = await Find_By_Id(inputModel.Id);
                if (inputModel.Price != data.Price)
                {
                    var price = (await _unitOfWork.ServicePriceHistoryRepository.FindBy(c => c.ServiceId == inputModel.Id)).ToList();
                    if (price.Count() > 1) //neu ko phai la lan update dau tien
                    {
                        var lastTimeprice = price.OrderByDescending(c => c.ToDate).FirstOrDefault();
                        lastTimeprice.ToDate = DateTime.Now;
                        await _unitOfWork.ServicePriceHistoryRepository.Update(lastTimeprice);
                        var hisPrice = new ServicePriceHistory();
                        hisPrice.ServiceId = inputModel.Id;
                        hisPrice.Price = inputModel.Price;
                        hisPrice.FromDate = DateTime.Now;
                        await _unitOfWork.ServicePriceHistoryRepository.Add(hisPrice);
                    }
                    else
                    {
                        var lastTimeprice = price.FirstOrDefault();
                        lastTimeprice.ToDate = DateTime.Now;
                        await _unitOfWork.ServicePriceHistoryRepository.Update(lastTimeprice);
                        var hisPrice = new ServicePriceHistory();
                        hisPrice.ServiceId = inputModel.Id;
                        hisPrice.Price = inputModel.Price;
                        hisPrice.FromDate = DateTime.Now;
                        await _unitOfWork.ServicePriceHistoryRepository.Add(hisPrice);
                    }
                }
                await _unitOfWork.ServicesRepository.Update(inputModel);
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
                await _unitOfWork.ServicesRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Services>> Get_List(string name, byte status)
        {
            try
            {
                var data = (await _unitOfWork.ServicesRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                           && (status == (int)StatusEnum.All || x.Status == (byte)status)
                                                                           ))).ToList();
                foreach (var i in data)
                {
                    var price = (await _unitOfWork.ServicePriceHistoryRepository.FindBy(c => c.FromDate <= DateTime.Now && (c.ToDate >= DateTime.Now
                                                                                  || c.ToDate == null) && c.ServiceId == i.Id)).FirstOrDefault();
                    i.Price = price == null ? 0 : price.Price;
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Services> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.ServicesRepository.Get(c => c.Id == id);
                if(data == null)
                {
                    return data;
                }
                var price = await _unitOfWork.ServicePriceHistoryRepository.Get(c => c.ServiceId == id && c.ToDate == null);
                data.Price = price == null ? 0 : price.Price;
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
