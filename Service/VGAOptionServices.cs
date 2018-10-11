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
    public interface IVGAOptionService
    {

        IEnumerable<VGAOption> GetVGAOptions();
        VGAOption GetVGAOptionById(int VGAOptionId);
        void CreateVGAOption(VGAOption VGAOption);
        void EditVGAOption(VGAOption VGAOptionToEdit);
        void DeleteVGAOption(int VGAOptionId);
        void SaveVGAOption();

    }
    public class VGAOptionService : IVGAOptionService
    {
        #region Field
        private readonly IVGAOptionRepository VGAOptionRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public VGAOptionService(IVGAOptionRepository VGAOptionRepository, IUnitOfWork unitOfWork)
        {
            this.VGAOptionRepository = VGAOptionRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<VGAOption> GetVGAOptions()
        {
            var VGAOptions = VGAOptionRepository.GetAll();
            return VGAOptions;
        }

        public VGAOption GetVGAOptionById(int VGAOptionId)
        {
            var VGAOption = VGAOptionRepository.GetById(VGAOptionId);
            return VGAOption;
        }

        public void CreateVGAOption(VGAOption VGAOption)
        {
            VGAOptionRepository.Add(VGAOption);
            SaveVGAOption();
        }

        public void EditVGAOption(VGAOption VGAOptionToEdit)
        {
            VGAOptionRepository.Update(VGAOptionToEdit);
            SaveVGAOption();
        }

        public void DeleteVGAOption(int VGAOptionId)
        {
            //Get VGAOption by id.
            var VGAOption = VGAOptionRepository.GetById(VGAOptionId);
            if (VGAOption != null)
            {
                VGAOptionRepository.Delete(VGAOption);
                SaveVGAOption();
            }
        }

        public void SaveVGAOption()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
