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
    public interface IMotorTypesManager
    {
        Task Create(MotorTypes inputModel);
        Task Update(MotorTypes inputModel);
        Task Delete(long id);
        Task<List<MotorTypes>> Get_List(string name, byte status,long manufactureId);
        Task<MotorTypes> Find_By_Id(long id);
    }
    public class MotorTypesManager:IMotorTypesManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public MotorTypesManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(MotorTypes inputModel)
        {
            try
            {
                await _unitOfWork.MotorTypesRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task Update(MotorTypes inputModel)
        {
            try
            {
                await _unitOfWork.MotorTypesRepository.Update(inputModel);
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
                await _unitOfWork.MotorTypesRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MotorTypes>> Get_List(string name, byte status, long manufactureId)
        {
            try
            {
                var data = (await _unitOfWork.MotorTypesRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                           && (status == (int)StatusEnum.All || x.Status == (byte)status)
                                                                           && (manufactureId == 0 || x.MotorManufactureID == manufactureId)
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<MotorTypes> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.MotorTypesRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
