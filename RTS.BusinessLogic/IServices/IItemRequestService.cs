using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.IServices
{
    public interface IItemRequestService
    {
        public Boolean SendRequest(Employee user,string body,int id);
        public string EmailText(string path);
        public Task<ItemRequest> Create(int id, string owner, string requester, int status);

    }
}
