using Data.Infrastructure;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{

    public interface IAccessoryService
    {

        IEnumerable<Accessory> GetAccessorys();
        Accessory GetAccessoryById(int AccessoryId);
        void CreateAccessory(Accessory Accessory);
        void EditAccessory(Accessory AccessoryToEdit);
        void DeleteAccessory(int AccessoryId);
        void SaveAccessory();

    }
    public class AccessoryService : IAccessoryService
    {
        #region Field
        private readonly IAccessoryRepository AccessoryRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public AccessoryService(IAccessoryRepository AccessoryRepository, IUnitOfWork unitOfWork)
        {
            this.AccessoryRepository = AccessoryRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Accessory> GetAccessorys()
        {
            var Accessorys = AccessoryRepository.GetAll();
            return Accessorys;
        }

        public Accessory GetAccessoryById(int AccessoryId)
        {
            var Accessory = AccessoryRepository.GetById(AccessoryId);
            return Accessory;
        }

        public void CreateAccessory(Accessory Accessory)
        {
            AccessoryRepository.Add(Accessory);
            SaveAccessory();
        }

        public void EditAccessory(Accessory AccessoryToEdit)
        {
            AccessoryRepository.Update(AccessoryToEdit);
            SaveAccessory();
        }

        public void DeleteAccessory(int AccessoryId)
        {
            //Get Accessory by id.
            var Accessory = AccessoryRepository.GetById(AccessoryId);
            if (Accessory != null)
            {
                AccessoryRepository.Delete(Accessory);
                SaveAccessory();
            }
        }

        public void SaveAccessory()
        {
            unitOfWork.Commit();
        }

        

        #endregion
    }
}
