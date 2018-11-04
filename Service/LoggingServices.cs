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
    public interface ILoggingService
    {

        IEnumerable<Logging> GetLoggings();
        Logging GetLoggingById(int LoggingId);
        void CreateLogging(Logging Logging);
        void EditLogging(Logging LoggingToEdit);
        void DeleteLogging(int LoggingId);
        void SaveLogging();

    }
    public class LoggingService : ILoggingService
    {
        #region Field
        private readonly ILoggingRepository LoggingRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public LoggingService(ILoggingRepository LoggingRepository, IUnitOfWork unitOfWork)
        {
            this.LoggingRepository = LoggingRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Logging> GetLoggings()
        {
            var Loggings = LoggingRepository.GetAll();
            return Loggings;
        }

        public Logging GetLoggingById(int LoggingId)
        {
            var Logging = LoggingRepository.GetById(LoggingId);
            return Logging;
        }

        public void CreateLogging(Logging Logging)
        {
            LoggingRepository.Add(Logging);
            SaveLogging();
        }

        public void EditLogging(Logging LoggingToEdit)
        {
            LoggingRepository.Update(LoggingToEdit);
            SaveLogging();
        }

        public void DeleteLogging(int LoggingId)
        {
            //Get Logging by id.
            var Logging = LoggingRepository.GetById(LoggingId);
            if (Logging != null)
            {
                LoggingRepository.Delete(Logging);
                SaveLogging();
            }
        }

        public void SaveLogging()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
