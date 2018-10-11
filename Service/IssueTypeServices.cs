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
    public interface IIssueTypeService
    {

        IEnumerable<IssueType> GetIssueTypes();
        IssueType GetIssueTypeById(int IssueTypeId);
        void CreateIssueType(IssueType IssueType);
        void EditIssueType(IssueType IssueTypeToEdit);
        void DeleteIssueType(int IssueTypeId);
        void SaveIssueType();

    }
    public class IssueTypeService : IIssueTypeService
    {
        #region Field
        private readonly IIssueTypeRepository IssueTypeRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public IssueTypeService(IIssueTypeRepository IssueTypeRepository, IUnitOfWork unitOfWork)
        {
            this.IssueTypeRepository = IssueTypeRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<IssueType> GetIssueTypes()
        {
            var IssueTypes = IssueTypeRepository.GetAll();
            return IssueTypes;
        }

        public IssueType GetIssueTypeById(int IssueTypeId)
        {
            var IssueType = IssueTypeRepository.GetById(IssueTypeId);
            return IssueType;
        }

        public void CreateIssueType(IssueType IssueType)
        {
            IssueTypeRepository.Add(IssueType);
            SaveIssueType();
        }

        public void EditIssueType(IssueType IssueTypeToEdit)
        {
            IssueTypeRepository.Update(IssueTypeToEdit);
            SaveIssueType();
        }

        public void DeleteIssueType(int IssueTypeId)
        {
            //Get IssueType by id.
            var IssueType = IssueTypeRepository.GetById(IssueTypeId);
            if (IssueType != null)
            {
                IssueTypeRepository.Delete(IssueType);
                SaveIssueType();
            }
        }

        public void SaveIssueType()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
