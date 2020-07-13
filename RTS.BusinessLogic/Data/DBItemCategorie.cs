using RTS.BusinessLogic.IServices;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.Data
{
    public class DBItemCategorie : IItemCategorieService
    {
        private readonly RTSDBContext _context;

        public DBItemCategorie(RTSDBContext context)
        {
            _context = context;
        }


        public List<ItemCategorie> GetItemCategories()
        {
            var item = _context.itemCategories.Where(a=>a.IsDeleted==false);
            return item.ToList();
        }
        public List<ItemCategorie> GetDeletedItemCategories()
        {
            var item = _context.itemCategories.Where(a => a.IsDeleted == true);
            return item.ToList();
        }
        public async Task Create(ItemCategorie itemCategorie)
        {
            _context.Add(itemCategorie);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ItemCategorie itemCategorie)
        {
            _context.Update(itemCategorie);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var itemCategorie =await _context.itemCategories.FindAsync(id);
            itemCategorie.IsDeleted = true;
            await _context.SaveChangesAsync();

        }

    }
}
