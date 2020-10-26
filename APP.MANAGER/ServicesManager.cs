﻿using APP.REPOSITORY;
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
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}