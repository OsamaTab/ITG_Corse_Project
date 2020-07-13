using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.IServices
{
    public interface IItemCategorieService
    {
        public List<ItemCategorie> GetItemCategories();

        public Task Create(ItemCategorie itemCategorie);
        public Task Edit(ItemCategorie itemCategorie);
        public Task Delete(int id);


    }
}
