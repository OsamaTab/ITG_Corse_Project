using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class DBItem : IItemService
    {
        private readonly RTSDBContext _context;

        public DBItem(RTSDBContext context)
        {
            _context = context;
        }


        public List<Item> GetItems()
        {
            var item = _context.Items.Include(i => i.CurentUser).Include(i => i.DeviceType).Where(d=>d.IsDeleted==false);
            return item.ToList();
        }
        public List<Item> GetDeletedItems()
        {
            var item = _context.Items.Include(i => i.CurentUser).Include(i => i.DeviceType).Where(d => d.IsDeleted == true);
            return item.ToList();
        }

        public List<Item> GetItemsByName(string search)
        {
            var item = from m in _context.Items.Include(i => i.CurentUser).Include(i => i.DeviceType).Where(i=>i.IsActive==true)
                       select m;

            if (!String.IsNullOrEmpty(search))
            {
                item = item.Include(i => i.CurentUser).Include(i => i.DeviceType).Where(s => s.Name.Contains(search)).Where(i => i.IsActive == true);
            }

            return item.ToList();
        }
        public List<Item> GetItemsByUser(Employee? user)
        {
            var item = _context.Items.Include(i => i.CurentUser).Include(i => i.DeviceType).Where(d => d.CurentUser.Id == user.Id).Where(i => i.IsActive == true);
            return item.ToList();
        }

        public async Task Create(Item item)
        {
            item.PurchaseDate = DateTime.Now;

            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Item item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            item.IsDeleted = true;
            await _context.SaveChangesAsync();

        }

    }
}
