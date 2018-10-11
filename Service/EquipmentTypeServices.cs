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
    public interface IEquipmentTypeService
    {

        IEnumerable<EquipmentType> GetEquipmentTypes();
        EquipmentType GetEquipmentTypeById(int EquipmentTypeId);
        void CreateEquipmentType(EquipmentType EquipmentType);
        void EditEquipmentType(EquipmentType EquipmentTypeToEdit);
        void DeleteEquipmentType(int EquipmentTypeId);
        void SaveEquipmentType();

    }
    public class EquipmentTypeService : IEquipmentTypeService
    {
        #region Field
        private readonly IEquipmentTypeRepository EquipmentTypeRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public EquipmentTypeService(IEquipmentTypeRepository EquipmentTypeRepository, IUnitOfWork unitOfWork)
        {
            this.EquipmentTypeRepository = EquipmentTypeRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<EquipmentType> GetEquipmentTypes()
        {
            var EquipmentTypes = EquipmentTypeRepository.GetAll();
            return EquipmentTypes;
        }

        public EquipmentType GetEquipmentTypeById(int EquipmentTypeId)
        {
            var EquipmentType = EquipmentTypeRepository.GetById(EquipmentTypeId);
            return EquipmentType;
        }

        public void CreateEquipmentType(EquipmentType EquipmentType)
        {
            EquipmentTypeRepository.Add(EquipmentType);
            SaveEquipmentType();
        }

        public void EditEquipmentType(EquipmentType EquipmentTypeToEdit)
        {
            EquipmentTypeRepository.Update(EquipmentTypeToEdit);
            SaveEquipmentType();
        }

        public void DeleteEquipmentType(int EquipmentTypeId)
        {
            //Get EquipmentType by id.
            var EquipmentType = EquipmentTypeRepository.GetById(EquipmentTypeId);
            if (EquipmentType != null)
            {
                EquipmentTypeRepository.Delete(EquipmentType);
                SaveEquipmentType();
            }
        }

        public void SaveEquipmentType()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
