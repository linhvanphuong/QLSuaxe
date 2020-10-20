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
    public interface IEmployeeManager
    {
        Task Create(Employees inputModel);
        Task Update(Employees inputModel);
        Task Delete(long id);
        Task<List<Employees>> Get_List(string name, long jobPositionId);
        Task<List<Employees>> Get_List();
        Task<Employees> Find_By_Id(long id);
    }
    public class EmployeeManager: IEmployeeManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Employees inputModel)
        {
            try
            {
                await _unitOfWork.EmployeesRepository.Add(inputModel);
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
                await _unitOfWork.EmployeesRepository.Delete(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employees> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.EmployeesRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Employees>> Get_List(string name, long jobPositionId)
        {
            try
            {
                var data = (await _unitOfWork.EmployeesRepository.FindBy(x => ((string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name))
                                                                            && (jobPositionId == 0 || x.JobPositionId == (long)jobPositionId)
                                                                           ))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Employees>> Get_List()
        {
            try
            {
                var data = (await _unitOfWork.EmployeesRepository.GetAll()).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Employees inputModel)
        {
            try
            {
                await _unitOfWork.EmployeesRepository.Update(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
