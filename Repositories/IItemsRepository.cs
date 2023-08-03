using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Entities;

namespace Catalog_API.Repositories
{
    public interface IItemsRepository
    {
         Item? GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void CreateItem(Item item);
    }
}