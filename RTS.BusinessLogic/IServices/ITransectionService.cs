using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.IServices
{
    public interface ITransectionService
    {
        public List<Trnasaction> GetTransections(string? search, int? filter, DateTime? startDate, DateTime? endDate);
        public Task<Trnasaction> Create(int id, int device, DateTime date);

    }
}
