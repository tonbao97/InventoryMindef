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
    class CategoryServices
    {
    }
    public interface ICategoryService
    {

        IEnumerable<Category> GetCategorys();
        Category GetCategoryById(int CategoryId);
        void CreateCategory(Category Category);
        void EditCategory(Category CategoryToEdit);
        void DeleteCategory(int CategoryId);
        void SaveCategory();

    }
    public class CategoryService : ICategoryService
    {
        #region Field
        private readonly ICategoryRepository CategoryRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CategoryService(ICategoryRepository CategoryRepository, IUnitOfWork unitOfWork)
        {
            this.CategoryRepository = CategoryRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Category> GetCategorys()
        {
            var Categorys = CategoryRepository.GetAll();
            return Categorys;
        }

        public Category GetCategoryById(int CategoryId)
        {
            var Category = CategoryRepository.GetById(CategoryId);
            return Category;
        }

        public void CreateCategory(Category Category)
        {
            CategoryRepository.Add(Category);
            SaveCategory();
        }

        public void EditCategory(Category CategoryToEdit)
        {
            CategoryRepository.Update(CategoryToEdit);
            SaveCategory();
        }

        public void DeleteCategory(int CategoryId)
        {
            //Get Category by id.
            var Category = CategoryRepository.GetById(CategoryId);
            if (Category != null)
            {
                CategoryRepository.Delete(Category);
                SaveCategory();
            }
        }

        public void SaveCategory()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
