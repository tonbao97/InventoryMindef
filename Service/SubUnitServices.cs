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
    public interface ISubUnitService
    {

        IEnumerable<SubUnit> GetSubUnits();
        SubUnit GetSubUnitById(int SubUnitId);
        void CreateSubUnit(SubUnit SubUnit);
        void EditSubUnit(SubUnit SubUnitToEdit);
        void DeleteSubUnit(int SubUnitId);
        void SaveSubUnit();

    }
    public class SubUnitService : ISubUnitService
    {
        #region Field
        private readonly ISubUnitRepository SubUnitRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public SubUnitService(ISubUnitRepository SubUnitRepository, IUnitOfWork unitOfWork)
        {
            this.SubUnitRepository = SubUnitRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<SubUnit> GetSubUnits()
        {
            var SubUnits = SubUnitRepository.GetAll();
            return SubUnits;
        }

        public SubUnit GetSubUnitById(int SubUnitId)
        {
            var SubUnit = SubUnitRepository.GetById(SubUnitId);
            return SubUnit;
        }

        public void CreateSubUnit(SubUnit SubUnit)
        {
            SubUnitRepository.Add(SubUnit);
            SaveSubUnit();
        }

        public void EditSubUnit(SubUnit SubUnitToEdit)
        {
            SubUnitRepository.Update(SubUnitToEdit);
            SaveSubUnit();
        }

        public void DeleteSubUnit(int SubUnitId)
        {
            //Get SubUnit by id.
            var SubUnit = SubUnitRepository.GetById(SubUnitId);
            if (SubUnit != null)
            {
                SubUnitRepository.Delete(SubUnit);
                SaveSubUnit();
            }
        }

        public void SaveSubUnit()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
