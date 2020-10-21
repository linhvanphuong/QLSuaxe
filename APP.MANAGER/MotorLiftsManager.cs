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
    public interface IMotorLiftsManager
    {
        Task Create(MotorLifts inputModel);
        Task Update(MotorLifts inputModel);
        Task Delete(long id);
        Task<List<MotorLifts>> Get_List(string name, byte status);
        Task<List<MotorLifts>> Get_List();
        Task<MotorLifts> Find_By_Id(long id);
    }
    public class MotorLiftsManager: IMotorLiftsManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public MotorLiftsManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(MotorLifts inputModel)
        {
            try
            {
                await _unitOfWork.MotorLiftsRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task Update(MotorLifts inputModel)
        {
            try
            {
                await _unitOfWork.MotorLiftsRepository.Update(inputModel);
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
                await _unitOfWork.MotorLiftsRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MotorLifts>> Get_List(string name, byte status)
        {
            try
            {
                var data = (await _unitOfWork.MotorLiftsRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                           && (status == (int)MotorLiftEnum.All || x.Status == (byte)status)
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MotorLifts>> Get_List()
        {
            try
            {
                var data = (await _unitOfWork.MotorLiftsRepository.GetAll()).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<MotorLifts> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.MotorLiftsRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
