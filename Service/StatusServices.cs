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
    public interface IStatusService
    {

        IEnumerable<Status> GetStatuss();
        Status GetStatusById(int StatusId);
        void CreateStatus(Status Status);
        void EditStatus(Status StatusToEdit);
        void DeleteStatus(int StatusId);
        void SaveStatus();

    }
    public class StatusService : IStatusService
    {
        #region Field
        private readonly IStatusRepository StatusRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public StatusService(IStatusRepository StatusRepository, IUnitOfWork unitOfWork)
        {
            this.StatusRepository = StatusRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Status> GetStatuss()
        {
            var Statuss = StatusRepository.GetAll();
            return Statuss;
        }

        public Status GetStatusById(int StatusId)
        {
            var Status = StatusRepository.GetById(StatusId);
            return Status;
        }

        public void CreateStatus(Status Status)
        {
            StatusRepository.Add(Status);
            SaveStatus();
        }

        public void EditStatus(Status StatusToEdit)
        {
            StatusRepository.Update(StatusToEdit);
            SaveStatus();
        }

        public void DeleteStatus(int StatusId)
        {
            //Get Status by id.
            var Status = StatusRepository.GetById(StatusId);
            if (Status != null)
            {
                StatusRepository.Delete(Status);
                SaveStatus();
            }
        }

        public void SaveStatus()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
