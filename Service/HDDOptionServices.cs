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
    public interface IHDDOptionService
    {

        IEnumerable<HDDOption> GetHDDOptions();
        HDDOption GetHDDOptionById(int HDDOptionId);
        void CreateHDDOption(HDDOption HDDOption);
        void EditHDDOption(HDDOption HDDOptionToEdit);
        void DeleteHDDOption(int HDDOptionId);
        void SaveHDDOption();

    }
    public class HDDOptionService : IHDDOptionService
    {
        #region Field
        private readonly IHDDOptionRepository HDDOptionRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public HDDOptionService(IHDDOptionRepository HDDOptionRepository, IUnitOfWork unitOfWork)
        {
            this.HDDOptionRepository = HDDOptionRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<HDDOption> GetHDDOptions()
        {
            var HDDOptions = HDDOptionRepository.GetAll();
            return HDDOptions;
        }

        public HDDOption GetHDDOptionById(int HDDOptionId)
        {
            var HDDOption = HDDOptionRepository.GetById(HDDOptionId);
            return HDDOption;
        }

        public void CreateHDDOption(HDDOption HDDOption)
        {
            HDDOptionRepository.Add(HDDOption);
            SaveHDDOption();
        }

        public void EditHDDOption(HDDOption HDDOptionToEdit)
        {
            HDDOptionRepository.Update(HDDOptionToEdit);
            SaveHDDOption();
        }

        public void DeleteHDDOption(int HDDOptionId)
        {
            //Get HDDOption by id.
            var HDDOption = HDDOptionRepository.GetById(HDDOptionId);
            if (HDDOption != null)
            {
                HDDOptionRepository.Delete(HDDOption);
                SaveHDDOption();
            }
        }

        public void SaveHDDOption()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
