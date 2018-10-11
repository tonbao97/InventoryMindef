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
    public interface IIssueService
    {

        IEnumerable<Issue> GetIssues();
        Issue GetIssueById(int IssueId);
        void CreateIssue(Issue Issue);
        void EditIssue(Issue IssueToEdit);
        void DeleteIssue(int IssueId);
        void SaveIssue();

    }
    public class IssueService : IIssueService
    {
        #region Field
        private readonly IIssueRepository IssueRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public IssueService(IIssueRepository IssueRepository, IUnitOfWork unitOfWork)
        {
            this.IssueRepository = IssueRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Issue> GetIssues()
        {
            var Issues = IssueRepository.GetAll();
            return Issues;
        }

        public Issue GetIssueById(int IssueId)
        {
            var Issue = IssueRepository.GetById(IssueId);
            return Issue;
        }

        public void CreateIssue(Issue Issue)
        {
            IssueRepository.Add(Issue);
            SaveIssue();
        }

        public void EditIssue(Issue IssueToEdit)
        {
            IssueRepository.Update(IssueToEdit);
            SaveIssue();
        }

        public void DeleteIssue(int IssueId)
        {
            //Get Issue by id.
            var Issue = IssueRepository.GetById(IssueId);
            if (Issue != null)
            {
                IssueRepository.Delete(Issue);
                SaveIssue();
            }
        }

        public void SaveIssue()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
