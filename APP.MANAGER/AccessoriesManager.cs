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
                var hisPrice = new AccessoryPriceHistory();
                hisPrice.AccessoriesId = inputModel.Id;
                hisPrice.Price = inputModel.Price;
                hisPrice.FromDate = DateTime.Now;
                await _unitOfWork.AccessoryPriceHistoryRepository.Add(hisPrice);
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
                var data = await Find_By_Id(inputModel.Id);
                if (inputModel.Price != data.Price)
                {
                    var price = (await _unitOfWork.AccessoryPriceHistoryRepository.FindBy(c => c.AccessoriesId == inputModel.Id)).ToList();
                    if(price.Count() > 1) //neu ko phai la lan update dau tien
                    {
                        var lastTimeprice = price.OrderByDescending(c => c.ToDate).FirstOrDefault();
                        lastTimeprice.ToDate = DateTime.Now;
                        await _unitOfWork.AccessoryPriceHistoryRepository.Update(lastTimeprice);
                        var hisPrice = new AccessoryPriceHistory();
                        hisPrice.AccessoriesId = inputModel.Id;
                        hisPrice.Price = inputModel.Price;
                        hisPrice.FromDate = DateTime.Now;
                        await _unitOfWork.AccessoryPriceHistoryRepository.Add(hisPrice);
                    }
                    else
                    {
                        var lastTimeprice = price.FirstOrDefault();
                        lastTimeprice.ToDate = DateTime.Now;
                        await _unitOfWork.AccessoryPriceHistoryRepository.Update(lastTimeprice);
                        var hisPrice = new AccessoryPriceHistory();
                        hisPrice.AccessoriesId = inputModel.Id;
                        hisPrice.Price = inputModel.Price;
                        hisPrice.FromDate = DateTime.Now;
                        await _unitOfWork.AccessoryPriceHistoryRepository.Add(hisPrice);
                    }
                }
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
                foreach(var i in data)
                {
                    // ban ghi lich su gia gan nhat
                    var price = (await _unitOfWork.AccessoryPriceHistoryRepository.FindBy(c => c.FromDate <= DateTime.Now && (c.ToDate >= DateTime.Now
                                                                                  || c.ToDate == null) && c.AccessoriesId == i.Id)).FirstOrDefault();
                    //co the lay theo ToDate == null luon
                    i.Price = price == null ? 0 : price.Price;
                }
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
                var price = await _unitOfWork.AccessoryPriceHistoryRepository.Get(c => c.AccessoriesId == id && c.ToDate == null);
                data.Price = price == null ? 0: price.Price;
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
