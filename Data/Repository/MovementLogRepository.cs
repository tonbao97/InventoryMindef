using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class MovementLogRepository : RepositoryBase<MovementLog>, IMovementLogRepository
    {
        public MovementLogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IMovementLogRepository : IRepository<MovementLog>
    {

    }
}
