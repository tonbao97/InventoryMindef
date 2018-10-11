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
    public interface IRamOptionService
    {

        IEnumerable<RamOption> GetRamOptions();
        RamOption GetRamOptionById(int RamOptionId);
        void CreateRamOption(RamOption RamOption);
        void EditRamOption(RamOption RamOptionToEdit);
        void DeleteRamOption(int RamOptionId);
        void SaveRamOption();

    }
    public class RamOptionService : IRamOptionService
    {
        #region Field
        private readonly IRamOptionRepository RamOptionRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public RamOptionService(IRamOptionRepository RamOptionRepository, IUnitOfWork unitOfWork)
        {
            this.RamOptionRepository = RamOptionRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<RamOption> GetRamOptions()
        {
            var RamOptions = RamOptionRepository.GetAll();
            return RamOptions;
        }

        public RamOption GetRamOptionById(int RamOptionId)
        {
            var RamOption = RamOptionRepository.GetById(RamOptionId);
            return RamOption;
        }

        public void CreateRamOption(RamOption RamOption)
        {
            RamOptionRepository.Add(RamOption);
            SaveRamOption();
        }

        public void EditRamOption(RamOption RamOptionToEdit)
        {
            RamOptionRepository.Update(RamOptionToEdit);
            SaveRamOption();
        }

        public void DeleteRamOption(int RamOptionId)
        {
            //Get RamOption by id.
            var RamOption = RamOptionRepository.GetById(RamOptionId);
            if (RamOption != null)
            {
                RamOptionRepository.Delete(RamOption);
                SaveRamOption();
            }
        }

        public void SaveRamOption()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
