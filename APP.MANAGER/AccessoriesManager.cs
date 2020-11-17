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

                var data = await _unitOfWork.ServicesRepository.Get(c => c.Id == inputModel.Id);
                if (inputModel.Price != data.Price)
                {
                    var hisPrice = new AccessoryPriceHistory();
                    hisPrice.AccessoriesId = inputModel.Id;
                    hisPrice.Price = inputModel.Price;
                    hisPrice.ToDate = DateTime.Now;
                    await _unitOfWork.AccessoryPriceHistoryRepository.Update(hisPrice);
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
                var price = (await _unitOfWork.AccessoryPriceHistoryRepository.FindBy(c => c.FromDate.Date <= DateTime.Now && (c.ToDate.Value.Date >= DateTime.Now
                                                                                  || c.ToDate.Value.Date == null))).FirstOrDefault();
                data.Price = price.Price;
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
