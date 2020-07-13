using RTS.BusinessLogic.IServices;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.Services
{
    class ItemCategorieService
    {
        IItemCategorieService _data;
        public ItemCategorieService(IItemCategorieService data)
        {
            _data = data;
        }

        public List<ItemCategorie> GetItemCategories()
        {
            try
            {
                return _data.GetItemCategories();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task Create(ItemCategorie itemCategorie)
        {
            try
            {
                await _data.Create(itemCategorie);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task Edit(ItemCategorie itemCategorie)
        {
            try
            {
                await _data.Edit(itemCategorie);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                await _data.Delete(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
