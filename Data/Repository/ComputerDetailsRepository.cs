using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{

    public class ComputerDetailsRepository : RepositoryBase<ComputerDetails>, IComputerDetailsRepository
    {
        public ComputerDetailsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IComputerDetailsRepository : IRepository<ComputerDetails>
    {

    }
}
