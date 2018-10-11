using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OSOptionRepository : RepositoryBase<OSOption>, IOSOptionRepository
    {
        public OSOptionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IOSOptionRepository : IRepository<OSOption>
    {

    }
}
