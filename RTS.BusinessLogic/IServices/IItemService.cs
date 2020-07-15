using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.IServices
{
    public interface IItemService
    {
        public List<Item> GetItems();
        public List<Item> GetDeletedItems();
        public List<Item> GetItemsByName(string search, int? filter);
        public List<Item> GetItemsByUser(Employee? user);

        public Task Create(Item item);
        public Task Edit(Item item);
        public Task Delete(int id);
    }
}
