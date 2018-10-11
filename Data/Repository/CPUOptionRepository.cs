using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CPUOptionRepository : RepositoryBase<CPUOption>, ICPUOptionRepository
    {
        public CPUOptionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ICPUOptionRepository : IRepository<CPUOption>
    {

    }
}
