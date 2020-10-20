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
    public interface IJobPositionsManager
    {
        Task Create(JobPositions inputModel);
        Task Update(JobPositions inputModel);
        Task Delete(long id);
        Task<List<JobPositions>> Get_List(string name, byte status);
        Task<JobPositions> Find_By_Id(long id);
    }
    public class JobPositionsManager: IJobPositionsManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobPositionsManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(JobPositions inputModel)
        {
            try
            {
                await _unitOfWork.JobPositionsRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task Update(JobPositions inputModel)
        {
            try
            {
                await _unitOfWork.JobPositionsRepository.Update(inputModel);
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
                await _unitOfWork.JobPositionsRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<JobPositions>> Get_List(string name,byte status)
        {
            try
            {
                var data = (await _unitOfWork.JobPositionsRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                           && (status == (int)StatusEnum.All || x.Status == (byte)status)
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JobPositions> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.JobPositionsRepository.Get(c => c.Id == id);
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
