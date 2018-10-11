using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SubUnitRepository : RepositoryBase<SubUnit>, ISubUnitRepository
    {
        public SubUnitRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISubUnitRepository : IRepository<SubUnit>
    {

    }
}
