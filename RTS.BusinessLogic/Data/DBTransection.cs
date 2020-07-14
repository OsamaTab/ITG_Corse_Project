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

        public List<Trnasaction> GetTransections()
        {
            var trnasactions = _context.Trnasactions.Include(t=>t.Item.Item).Include(d=>d.DeviceType).Include(t=>t.Item.RequestedUser).Include(t=>t.Item.Status);
            return trnasactions.ToList();
        }
    }
}
