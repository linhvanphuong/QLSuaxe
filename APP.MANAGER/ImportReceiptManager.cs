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
    public interface IImportReceiptManager
    {
        Task Create(ImportReceipt inputModel);
        //Task Delete(long id);
        Task<List<ImportReceipt>> Get_List(string createdDate);
        Task<ImportReceipt> Find_By_Id(long id);
        Task<List<ImportReceipt_Accessory>> Get_List_Import_Accesories(long id);
    }
    public class ImportReceiptManager : IImportReceiptManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImportReceiptManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(ImportReceipt inputModel)
        {
            try
            {
                await _unitOfWork.ImportReceiptRepository.Add(inputModel);
                if (inputModel.listAccessory != null)
                {
                    await CreateImport_Accesory(inputModel.listAccessory, inputModel);
                }
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task CreateImport_Accesory(List<ImportReceipt_Accessory> list,ImportReceipt inputModel)
        {
            try
            {
                foreach (var item in list)
                {
                    item.ImportReceiptId = inputModel.Id;
                    await _unitOfWork.ImportReceipt_AccessoryRepository.Add(item);
                    var acc = await _unitOfWork.AccessoriesRepository.Get(c => c.Id == item.AccesoryId);
                    acc.Quantity = acc.Quantity + item.Quantity;
                    await _unitOfWork.AccessoriesRepository.Update(acc);
                    await _unitOfWork.SaveChange();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ImportReceipt>> Get_List(string createdDate)
        {
            try
            {
                var data = (await _unitOfWork.ImportReceiptRepository.FindBy(c=>c.CreatedDate.Date.ToString() == createdDate || string.IsNullOrEmpty(createdDate))).ToList();
                if(data != null)
                {
                    foreach (var i in data)
                    {
                        i.SupllierName = (await _unitOfWork.SupplierRepository.Get(c => c.Id == i.SupplierId)).Name;
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ImportReceipt_Accessory>> Get_List_Import_Accesories(long id)
        {
            try
            {
                var data = (await _unitOfWork.ImportReceipt_AccessoryRepository.FindBy(c => c.ImportReceiptId == id)).ToList();
                foreach(var i in data)
                {
                    var acc = await _unitOfWork.AccessoriesRepository.Get(c => c.Id == i.AccesoryId);
                    i.AccessoryName = acc.Name;
                    i.Unit = acc.Unit;
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ImportReceipt> Find_By_Id(long id)
        {
            try
            {
                var data = await _unitOfWork.ImportReceiptRepository.Get(c => c.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
