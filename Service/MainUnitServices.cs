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
    public interface IMainUnitService
    {

        IEnumerable<MainUnit> GetMainUnits();
        MainUnit GetMainUnitById(int MainUnitId);
        void CreateMainUnit(MainUnit MainUnit);
        void EditMainUnit(MainUnit MainUnitToEdit);
        void DeleteMainUnit(int MainUnitId);
        void SaveMainUnit();

    }
    public class MainUnitService : IMainUnitService
    {
        #region Field
        private readonly IMainUnitRepository MainUnitRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public MainUnitService(IMainUnitRepository MainUnitRepository, IUnitOfWork unitOfWork)
        {
            this.MainUnitRepository = MainUnitRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<MainUnit> GetMainUnits()
        {
            var MainUnits = MainUnitRepository.GetAll();
            return MainUnits;
        }

        public MainUnit GetMainUnitById(int MainUnitId)
        {
            var MainUnit = MainUnitRepository.GetById(MainUnitId);
            return MainUnit;
        }

        public void CreateMainUnit(MainUnit MainUnit)
        {
            MainUnitRepository.Add(MainUnit);
            SaveMainUnit();
        }

        public void EditMainUnit(MainUnit MainUnitToEdit)
        {
            MainUnitRepository.Update(MainUnitToEdit);
            SaveMainUnit();
        }

        public void DeleteMainUnit(int MainUnitId)
        {
            //Get MainUnit by id.
            var MainUnit = MainUnitRepository.GetById(MainUnitId);
            if (MainUnit != null)
            {
                MainUnitRepository.Delete(MainUnit);
                SaveMainUnit();
            }
        }

        public void SaveMainUnit()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
