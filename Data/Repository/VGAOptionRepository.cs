using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class VGAOptionRepository : RepositoryBase<VGAOption>, IVGAOptionRepository
    {
        public VGAOptionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IVGAOptionRepository : IRepository<VGAOption>
    {

    }
}
