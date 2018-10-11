using Data.Infrastructure;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IItemService
    {

        IEnumerable<Item> GetItems();
        Item GetItemById(int ItemId);
        void CreateItem(Item Item);
        void EditItem(Item ItemToEdit);
        void DeleteItem(int ItemId);
        void SaveItem();

    }
    public class ItemService : IItemService
    {
        #region Field
        private readonly IItemRepository ItemRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public ItemService(IItemRepository ItemRepository, IUnitOfWork unitOfWork)
        {
            this.ItemRepository = ItemRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Item> GetItems()
        {
            var Items = ItemRepository.GetAll();
            return Items;
        }

        public Item GetItemById(int ItemId)
        {
            var Item = ItemRepository.GetById(ItemId);
            return Item;
        }

        public void CreateItem(Item Item)
        {
            ItemRepository.Add(Item);
            SaveItem();
        }

        public void EditItem(Item ItemToEdit)
        {
            ItemRepository.Update(ItemToEdit);
            SaveItem();
        }

        public void DeleteItem(int ItemId)
        {
            //Get Item by id.
            var Item = ItemRepository.GetById(ItemId);
            if (Item != null)
            {
                ItemRepository.Delete(Item);
                SaveItem();
            }
        }

        public void SaveItem()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
