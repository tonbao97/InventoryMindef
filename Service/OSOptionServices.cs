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
    public interface IOSOptionService
    {

        IEnumerable<OSOption> GetOSOptions();
        OSOption GetOSOptionById(int OSOptionId);
        void CreateOSOption(OSOption OSOption);
        void EditOSOption(OSOption OSOptionToEdit);
        void DeleteOSOption(int OSOptionId);
        void SaveOSOption();

    }
    public class OSOptionService : IOSOptionService
    {
        #region Field
        private readonly IOSOptionRepository OSOptionRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public OSOptionService(IOSOptionRepository OSOptionRepository, IUnitOfWork unitOfWork)
        {
            this.OSOptionRepository = OSOptionRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<OSOption> GetOSOptions()
        {
            var OSOptions = OSOptionRepository.GetAll();
            return OSOptions;
        }

        public OSOption GetOSOptionById(int OSOptionId)
        {
            var OSOption = OSOptionRepository.GetById(OSOptionId);
            return OSOption;
        }

        public void CreateOSOption(OSOption OSOption)
        {
            OSOptionRepository.Add(OSOption);
            SaveOSOption();
        }

        public void EditOSOption(OSOption OSOptionToEdit)
        {
            OSOptionRepository.Update(OSOptionToEdit);
            SaveOSOption();
        }

        public void DeleteOSOption(int OSOptionId)
        {
            //Get OSOption by id.
            var OSOption = OSOptionRepository.GetById(OSOptionId);
            if (OSOption != null)
            {
                OSOptionRepository.Delete(OSOption);
                SaveOSOption();
            }
        }

        public void SaveOSOption()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
