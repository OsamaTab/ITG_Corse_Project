using Microsoft.EntityFrameworkCore;
using RTS.BusinessLogic.IServices;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.Data
{
    public class DBItemRequest : IItemRequestService
    {
        private readonly RTSDBContext _context;

        public DBItemRequest(RTSDBContext context)
        {
            _context = context;
        }

        public List<ItemRequest> GetRequestByUser(string userId)
        {
            var request = _context.ItemRequests.Include(x => x.RequestedUser).Where(x => x.Status.Id == 2 && x.ItemOwner == userId);
            return request.ToList();
        }
        public List<ItemRequest> GetItemsByUser(string userId)
        {
            var request = _context.ItemRequests.Where(x => x.Status.Id == 1 && x.RequestedUserId == userId);
            return request.ToList();
        }

        public async Task<ItemRequest> Create(int id ,string owner,string requester,int status)
        {
            ItemRequest itemRequest = new ItemRequest();
            itemRequest.ItemId = id;
            itemRequest.ItemOwner = owner;
            itemRequest.RequestedUserId = requester;
            itemRequest.StatusId = status;
            
            _context.Add(itemRequest);
            await _context.SaveChangesAsync();
            return itemRequest;
        }


        public void SendRequest(string email, string body,int response)
        {
            string to = email;
            string subject = "Item Have Benn Requested";

            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.Subject = subject;

            mm.From = new MailAddress("projectmail9990@gmail.com");
            body = body.Replace("#linkApprove", response.ToString()).Replace("#linkDenied", response.ToString());
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;

            smtp.Credentials = new System.Net.NetworkCredential("projectmail9990@gmail.com", "Qwer1234/");
            smtp.Send(mm);

        }
        public void SendResponse(string email, string body,string response)
        {
                string to = email;
                string subject = "Response";

                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.Subject = subject;

                mm.From = new MailAddress("projectmail9990@gmail.com");
                body = body.Replace("#linkApprove",response);
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;

                smtp.Credentials = new System.Net.NetworkCredential("projectmail9990@gmail.com", "Qwer1234/");
                smtp.Send(mm);

        }

        public string EmailText(string path)
        {

            StringBuilder storeContent = new StringBuilder();

            try
            {
                using (StreamReader htmlReader = new StreamReader(path))
                {
                    string lineStr;
                    while ((lineStr = htmlReader.ReadLine()) != null)
                    {
                        storeContent.Append(lineStr);
                    }
                }
            }
            catch (Exception objError)
            {
                throw objError;
            }

            return storeContent.ToString();
        }

        public async Task<ItemRequest> Edit(int requestId, int status)
        {
            var request = await _context.ItemRequests.FindAsync(requestId);
            request.StatusId = status;

            _context.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<ItemRequest> Update(int requestId, string userId, int status)
        {
            var request = await _context.ItemRequests.FindAsync(requestId);
            request.StatusId = status;
            request.RequestedUserId = userId;
            _context.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }
    }
}
