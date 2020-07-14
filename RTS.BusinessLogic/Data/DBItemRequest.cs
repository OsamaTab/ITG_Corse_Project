using RTS.BusinessLogic.IServices;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.IO;
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


        public Boolean SendRequest(Employee user,string body)
        {
            try
            {
                string to = user.Email;
                string subject = "test";

                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.Subject = subject;

                mm.From = new MailAddress("projectmail9990@gmail.com");
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;

                smtp.Credentials = new System.Net.NetworkCredential("projectmail9990@gmail.com", "Qwer1234/");
                smtp.Send(mm);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
    }
}
