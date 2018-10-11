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
    public interface IEquipmentService
    {

        IEnumerable<Equipment> GetEquipments();
        Equipment GetEquipmentById(int EquipmentId);
        void CreateEquipment(Equipment Equipment);
        void EditEquipment(Equipment EquipmentToEdit);
        void DeleteEquipment(int EquipmentId);
        void SaveEquipment();

    }
    public class EquipmentService : IEquipmentService
    {
        #region Field
        private readonly IEquipmentRepository EquipmentRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public EquipmentService(IEquipmentRepository EquipmentRepository, IUnitOfWork unitOfWork)
        {
            this.EquipmentRepository = EquipmentRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Equipment> GetEquipments()
        {
            var Equipments = EquipmentRepository.GetAll();
            return Equipments;
        }

        public Equipment GetEquipmentById(int EquipmentId)
        {
            var Equipment = EquipmentRepository.GetById(EquipmentId);
            return Equipment;
        }

        public void CreateEquipment(Equipment Equipment)
        {
            EquipmentRepository.Add(Equipment);
            SaveEquipment();
        }

        public void EditEquipment(Equipment EquipmentToEdit)
        {
            EquipmentRepository.Update(EquipmentToEdit);
            SaveEquipment();
        }

        public void DeleteEquipment(int EquipmentId)
        {
            //Get Equipment by id.
            var Equipment = EquipmentRepository.GetById(EquipmentId);
            if (Equipment != null)
            {
                EquipmentRepository.Delete(Equipment);
                SaveEquipment();
            }
        }

        public void SaveEquipment()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
