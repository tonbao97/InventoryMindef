using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RamOptionRepository : RepositoryBase<RamOption>, IRamOptionRepository
    {
        public RamOptionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IRamOptionRepository : IRepository<RamOption>
    {

    }
}
