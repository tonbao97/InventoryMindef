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
    public interface IStaffService
    {

        IEnumerable<Staff> GetStaffs();
        Staff GetStaffById(int StaffId);
        void CreateStaff(Staff Staff);
        void EditStaff(Staff StaffToEdit);
        void DeleteStaff(int StaffId);
        void SaveStaff();

    }
    public class StaffService : IStaffService
    {
        #region Field
        private readonly IStaffRepository StaffRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public StaffService(IStaffRepository StaffRepository, IUnitOfWork unitOfWork)
        {
            this.StaffRepository = StaffRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Staff> GetStaffs()
        {
            var Staffs = StaffRepository.GetAll();
            return Staffs;
        }

        public Staff GetStaffById(int StaffId)
        {
            var Staff = StaffRepository.GetById(StaffId);
            return Staff;
        }

        public void CreateStaff(Staff Staff)
        {
            StaffRepository.Add(Staff);
            SaveStaff();
        }

        public void EditStaff(Staff StaffToEdit)
        {
            StaffRepository.Update(StaffToEdit);
            SaveStaff();
        }

        public void DeleteStaff(int StaffId)
        {
            //Get Staff by id.
            var Staff = StaffRepository.GetById(StaffId);
            if (Staff != null)
            {
                StaffRepository.Delete(Staff);
                SaveStaff();
            }
        }

        public void SaveStaff()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
