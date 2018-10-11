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
    public interface IPictureService
    {

        IEnumerable<Picture> GetPictures();
        Picture GetPictureById(int PictureId);
        void CreatePicture(Picture Picture);
        void EditPicture(Picture PictureToEdit);
        void DeletePicture(int PictureId);
        void SavePicture();

    }
    public class PictureService : IPictureService
    {
        #region Field
        private readonly IPictureRepository PictureRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public PictureService(IPictureRepository PictureRepository, IUnitOfWork unitOfWork)
        {
            this.PictureRepository = PictureRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Picture> GetPictures()
        {
            var Pictures = PictureRepository.GetAll();
            return Pictures;
        }

        public Picture GetPictureById(int PictureId)
        {
            var Picture = PictureRepository.GetById(PictureId);
            return Picture;
        }

        public void CreatePicture(Picture Picture)
        {
            PictureRepository.Add(Picture);
            SavePicture();
        }

        public void EditPicture(Picture PictureToEdit)
        {
            PictureRepository.Update(PictureToEdit);
            SavePicture();
        }

        public void DeletePicture(int PictureId)
        {
            //Get Picture by id.
            var Picture = PictureRepository.GetById(PictureId);
            if (Picture != null)
            {
                PictureRepository.Delete(Picture);
                SavePicture();
            }
        }

        public void SavePicture()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
