using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class MainUnitRepository : RepositoryBase<MainUnit>, IMainUnitRepository
    {
        public MainUnitRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IMainUnitRepository : IRepository<MainUnit>
    {

    }
}
