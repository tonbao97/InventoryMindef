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
    public interface ISupplierService
    {

        IEnumerable<Supplier> GetSuppliers();
        Supplier GetSupplierById(int SupplierId);
        void CreateSupplier(Supplier Supplier);
        void EditSupplier(Supplier SupplierToEdit);
        void DeleteSupplier(int SupplierId);
        void SaveSupplier();

    }
    public class SupplierService : ISupplierService
    {
        #region Field
        private readonly ISupplierRepository SupplierRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public SupplierService(ISupplierRepository SupplierRepository, IUnitOfWork unitOfWork)
        {
            this.SupplierRepository = SupplierRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Supplier> GetSuppliers()
        {
            var Suppliers = SupplierRepository.GetAll();
            return Suppliers;
        }

        public Supplier GetSupplierById(int SupplierId)
        {
            var Supplier = SupplierRepository.GetById(SupplierId);
            return Supplier;
        }

        public void CreateSupplier(Supplier Supplier)
        {
            SupplierRepository.Add(Supplier);
            SaveSupplier();
        }

        public void EditSupplier(Supplier SupplierToEdit)
        {
            SupplierRepository.Update(SupplierToEdit);
            SaveSupplier();
        }

        public void DeleteSupplier(int SupplierId)
        {
            //Get Supplier by id.
            var Supplier = SupplierRepository.GetById(SupplierId);
            if (Supplier != null)
            {
                SupplierRepository.Delete(Supplier);
                SaveSupplier();
            }
        }

        public void SaveSupplier()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
