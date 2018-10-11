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
    public interface IMovementLogService
    {

        IEnumerable<MovementLog> GetMovementLogs();
        MovementLog GetMovementLogById(int MovementLogId);
        void CreateMovementLog(MovementLog MovementLog);
        void EditMovementLog(MovementLog MovementLogToEdit);
        void DeleteMovementLog(int MovementLogId);
        void SaveMovementLog();

    }
    public class MovementLogService : IMovementLogService
    {
        #region Field
        private readonly IMovementLogRepository MovementLogRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public MovementLogService(IMovementLogRepository MovementLogRepository, IUnitOfWork unitOfWork)
        {
            this.MovementLogRepository = MovementLogRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<MovementLog> GetMovementLogs()
        {
            var MovementLogs = MovementLogRepository.GetAll();
            return MovementLogs;
        }

        public MovementLog GetMovementLogById(int MovementLogId)
        {
            var MovementLog = MovementLogRepository.GetById(MovementLogId);
            return MovementLog;
        }

        public void CreateMovementLog(MovementLog MovementLog)
        {
            MovementLogRepository.Add(MovementLog);
            SaveMovementLog();
        }

        public void EditMovementLog(MovementLog MovementLogToEdit)
        {
            MovementLogRepository.Update(MovementLogToEdit);
            SaveMovementLog();
        }

        public void DeleteMovementLog(int MovementLogId)
        {
            //Get MovementLog by id.
            var MovementLog = MovementLogRepository.GetById(MovementLogId);
            if (MovementLog != null)
            {
                MovementLogRepository.Delete(MovementLog);
                SaveMovementLog();
            }
        }

        public void SaveMovementLog()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
