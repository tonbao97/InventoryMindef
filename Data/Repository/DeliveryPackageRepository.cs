using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DeliveryPackageRepository : RepositoryBase<DeliveryPackage>, IDeliveryPackageRepository
    {
        public DeliveryPackageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IDeliveryPackageRepository : IRepository<DeliveryPackage>
    {

    }
}
