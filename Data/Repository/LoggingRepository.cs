using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class LoggingRepository : RepositoryBase<Logging>, ILoggingRepository
    {
        public LoggingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ILoggingRepository : IRepository<Logging>
    {

    }
}
