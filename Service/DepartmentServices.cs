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
    public interface IDepartmentService
    {

        IEnumerable<Department> GetDepartments();
        Department GetDepartmentById(int DepartmentId);
        void CreateDepartment(Department Department);
        void EditDepartment(Department DepartmentToEdit);
        void DeleteDepartment(int DepartmentId);
        void SaveDepartment();

    }
    public class DepartmentService : IDepartmentService
    {
        #region Field
        private readonly IDepartmentRepository DepartmentRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public DepartmentService(IDepartmentRepository DepartmentRepository, IUnitOfWork unitOfWork)
        {
            this.DepartmentRepository = DepartmentRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Department> GetDepartments()
        {
            var Departments = DepartmentRepository.GetAll();
            return Departments;
        }

        public Department GetDepartmentById(int DepartmentId)
        {
            var Department = DepartmentRepository.GetById(DepartmentId);
            return Department;
        }

        public void CreateDepartment(Department Department)
        {
            DepartmentRepository.Add(Department);
            SaveDepartment();
        }

        public void EditDepartment(Department DepartmentToEdit)
        {
            DepartmentRepository.Update(DepartmentToEdit);
            SaveDepartment();
        }

        public void DeleteDepartment(int DepartmentId)
        {
            //Get Department by id.
            var Department = DepartmentRepository.GetById(DepartmentId);
            if (Department != null)
            {
                DepartmentRepository.Delete(Department);
                SaveDepartment();
            }
        }

        public void SaveDepartment()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
