using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class IssueTypeRepository : RepositoryBase<IssueType>, IIssueTypeRepository
    {
        public IssueTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IIssueTypeRepository : IRepository<IssueType>
    {

    }
}
