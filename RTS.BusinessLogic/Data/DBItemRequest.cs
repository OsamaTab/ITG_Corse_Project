using RTS.BusinessLogic.IServices;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.Data
{
    public class DBItemRequest : IItemRequestService
    {
        public Boolean RequestItem(Employee user)
        {
            try
            {
                string to = user.Email;
                string subject = "test";

                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.Subject = subject;

                mm.From = new MailAddress("projectmail9990@gmail.com");
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
    }
}
