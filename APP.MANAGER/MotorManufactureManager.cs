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
    public interface IMotorManufactureManager
    {
        Task Create(MotorManufacture inputModel);
        Task Update(MotorManufacture inputModel);
        Task Delete(long id);
        Task<List<MotorManufacture>> Get_List(string name, byte status);
        Task<MotorManufacture> Find_By_Id(long id);
    }
    public class MotorManufactureManager : IMotorManufactureManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public MotorManufactureManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(MotorManufacture inputModel)
        {
            try
            {
                await _unitOfWork.MotorManufactureRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task Update(MotorManufacture inputModel)
        {
            try
            {
                await _unitOfWork.MotorManufactureRepository.Update(inputModel);
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
                await _unitOfWork.MotorManufactureRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MotorManufacture>> Get_List(string name, byte status)
        {
            try
            {
                var data = (await _unitOfWork.MotorManufactureRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                           && (status == (int)StatusEnum.All || x.Status == (byte)status)
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<MotorManufacture> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.MotorManufactureRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
