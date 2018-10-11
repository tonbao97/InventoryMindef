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
    public interface IUnitService
    {

        IEnumerable<Unit> GetUnits();
        Unit GetUnitById(int UnitId);
        void CreateUnit(Unit Unit);
        void EditUnit(Unit UnitToEdit);
        void DeleteUnit(int UnitId);
        void SaveUnit();

    }
    public class UnitService : IUnitService
    {
        #region Field
        private readonly IUnitRepository UnitRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public UnitService(IUnitRepository UnitRepository, IUnitOfWork unitOfWork)
        {
            this.UnitRepository = UnitRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Unit> GetUnits()
        {
            var Units = UnitRepository.GetAll();
            return Units;
        }

        public Unit GetUnitById(int UnitId)
        {
            var Unit = UnitRepository.GetById(UnitId);
            return Unit;
        }

        public void CreateUnit(Unit Unit)
        {
            UnitRepository.Add(Unit);
            SaveUnit();
        }

        public void EditUnit(Unit UnitToEdit)
        {
            UnitRepository.Update(UnitToEdit);
            SaveUnit();
        }

        public void DeleteUnit(int UnitId)
        {
            //Get Unit by id.
            var Unit = UnitRepository.GetById(UnitId);
            if (Unit != null)
            {
                UnitRepository.Delete(Unit);
                SaveUnit();
            }
        }

        public void SaveUnit()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
