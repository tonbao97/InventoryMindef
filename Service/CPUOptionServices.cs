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
    public interface ICPUOptionService
    {

        IEnumerable<CPUOption> GetCPUOptions();
        CPUOption GetCPUOptionById(int CPUOptionId);
        void CreateCPUOption(CPUOption CPUOption);
        void EditCPUOption(CPUOption CPUOptionToEdit);
        void DeleteCPUOption(int CPUOptionId);
        void SaveCPUOption();

    }
    public class CPUOptionService : ICPUOptionService
    {
        #region Field
        private readonly ICPUOptionRepository CPUOptionRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CPUOptionService(ICPUOptionRepository CPUOptionRepository, IUnitOfWork unitOfWork)
        {
            this.CPUOptionRepository = CPUOptionRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<CPUOption> GetCPUOptions()
        {
            var CPUOptions = CPUOptionRepository.GetAll();
            return CPUOptions;
        }

        public CPUOption GetCPUOptionById(int CPUOptionId)
        {
            var CPUOption = CPUOptionRepository.GetById(CPUOptionId);
            return CPUOption;
        }

        public void CreateCPUOption(CPUOption CPUOption)
        {
            CPUOptionRepository.Add(CPUOption);
            SaveCPUOption();
        }

        public void EditCPUOption(CPUOption CPUOptionToEdit)
        {
            CPUOptionRepository.Update(CPUOptionToEdit);
            SaveCPUOption();
        }

        public void DeleteCPUOption(int CPUOptionId)
        {
            //Get CPUOption by id.
            var CPUOption = CPUOptionRepository.GetById(CPUOptionId);
            if (CPUOption != null)
            {
                CPUOptionRepository.Delete(CPUOption);
                SaveCPUOption();
            }
        }

        public void SaveCPUOption()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
