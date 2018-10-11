using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    
    public class AccessoryRepository : RepositoryBase<Accessory>, IAccessoryRepository
    {
        public AccessoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IAccessoryRepository : IRepository<Accessory>
    {

    }
}
