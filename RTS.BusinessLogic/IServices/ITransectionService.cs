using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.IServices
{
    public interface ITransectionService
    {
        public List<Trnasaction> GetTransections();
        public Task Create(Trnasaction trnasaction);

    }
}
