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
  
    public interface IBrandService
    {

        IEnumerable<Brand> GetBrands();
        Brand GetBrandById(int BrandId);
        void CreateBrand(Brand Brand);
        void EditBrand(Brand BrandToEdit);
        void DeleteBrand(int BrandId);
        void SaveBrand();

    }
    public class BrandService : IBrandService
    {
        #region Field
        private readonly IBrandRepository BrandRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public BrandService(IBrandRepository BrandRepository, IUnitOfWork unitOfWork)
        {
            this.BrandRepository = BrandRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Brand> GetBrands()
        {
            var Brands = BrandRepository.GetAll();
            return Brands;
        }

        public Brand GetBrandById(int BrandId)
        {
            var Brand = BrandRepository.GetById(BrandId);
            return Brand;
        }

        public void CreateBrand(Brand Brand)
        {
            BrandRepository.Add(Brand);
            SaveBrand();
        }

        public void EditBrand(Brand BrandToEdit)
        {
            BrandRepository.Update(BrandToEdit);
            SaveBrand();
        }

        public void DeleteBrand(int BrandId)
        {
            //Get Brand by id.
            var Brand = BrandRepository.GetById(BrandId);
            if (Brand != null)
            {
                BrandRepository.Delete(Brand);
                SaveBrand();
            }
        }

        public void SaveBrand()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
