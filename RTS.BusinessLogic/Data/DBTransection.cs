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
    public class DBTransection : ITransectionService
    {
        private readonly RTSDBContext _context;

        public DBTransection(RTSDBContext context)
        {
            _context = context;
        }

        public async Task<Trnasaction> Create(int id,int device,DateTime date)
        {
            Trnasaction transaction = new Trnasaction
            {
                ItemRequestId = id,
                DeviceTypeId = device,
                TransectionDate = date
            };

            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public List<Trnasaction> GetTransections(string? search ,int? filter,DateTime? startDate,DateTime? endDate)
        {
            var trnasactions = from m in _context.Trnasactions.Include(t => t.Item.Item).Include(d => d.DeviceType).Include(t => t.Item.RequestedUser).Include(t => t.Item.Status)
                       select m;

            if (filter != null)
            {
                trnasactions = trnasactions.Where(s=>s.Item.StatusId==filter);
            }
            else if (!String.IsNullOrEmpty(search))
            {
                trnasactions = trnasactions.Where(s => s.Item.Item.Name.Contains(search));
            }
            else if(startDate!=null && endDate != null)
            {
                trnasactions = trnasactions.Where(x => x.TransectionDate.Date >= startDate && x.TransectionDate.Date <= endDate);
            }

            return trnasactions.OrderByDescending(x=>x.TransectionDate).ToList();
        }
    }
}
