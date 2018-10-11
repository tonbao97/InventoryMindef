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
    public interface IDeliveryPackageService
    {

        IEnumerable<DeliveryPackage> GetDeliveryPackages();
        DeliveryPackage GetDeliveryPackageById(int DeliveryPackageId);
        void CreateDeliveryPackage(DeliveryPackage DeliveryPackage);
        void EditDeliveryPackage(DeliveryPackage DeliveryPackageToEdit);
        void DeleteDeliveryPackage(int DeliveryPackageId);
        void SaveDeliveryPackage();

    }
    public class DeliveryPackageService : IDeliveryPackageService
    {
        #region Field
        private readonly IDeliveryPackageRepository DeliveryPackageRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public DeliveryPackageService(IDeliveryPackageRepository DeliveryPackageRepository, IUnitOfWork unitOfWork)
        {
            this.DeliveryPackageRepository = DeliveryPackageRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<DeliveryPackage> GetDeliveryPackages()
        {
            var DeliveryPackages = DeliveryPackageRepository.GetAll();
            return DeliveryPackages;
        }

        public DeliveryPackage GetDeliveryPackageById(int DeliveryPackageId)
        {
            var DeliveryPackage = DeliveryPackageRepository.GetById(DeliveryPackageId);
            return DeliveryPackage;
        }

        public void CreateDeliveryPackage(DeliveryPackage DeliveryPackage)
        {
            DeliveryPackageRepository.Add(DeliveryPackage);
            SaveDeliveryPackage();
        }

        public void EditDeliveryPackage(DeliveryPackage DeliveryPackageToEdit)
        {
            DeliveryPackageRepository.Update(DeliveryPackageToEdit);
            SaveDeliveryPackage();
        }

        public void DeleteDeliveryPackage(int DeliveryPackageId)
        {
            //Get DeliveryPackage by id.
            var DeliveryPackage = DeliveryPackageRepository.GetById(DeliveryPackageId);
            if (DeliveryPackage != null)
            {
                DeliveryPackageRepository.Delete(DeliveryPackage);
                SaveDeliveryPackage();
            }
        }

        public void SaveDeliveryPackage()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
