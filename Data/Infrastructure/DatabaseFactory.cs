
namespace Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private InventoryEntities dataContext;
        public InventoryEntities Get()
        {
            return dataContext ?? (dataContext = new InventoryEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
