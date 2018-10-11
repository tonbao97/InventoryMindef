
using System;

namespace Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        InventoryEntities Get();
    }
}
