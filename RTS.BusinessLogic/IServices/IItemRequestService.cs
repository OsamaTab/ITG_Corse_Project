using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.IServices
{
    public interface IItemRequestService
    {
        public List<ItemRequest> GetRequestByUser(string? userId);
        public List<ItemRequest> GetItemsByUser(string? userId);

        public void SendRequest(string email, string body, int response);
        public void SendResponse(string email, string body, string response);

        public string EmailText(string path);
        public Task<ItemRequest> Create(int id, string owner, string requester, int status);
        public Task<ItemRequest> Edit(int requestId, int status);


    }
}
