using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TestCrud.Model;

namespace TestCrud.Helpers
{
    public class SendMail
    {
        public void Send(EmailModel model)
        {
            try
            {
                using (MailMessage mm = new MailMessage(model.Email, model.To))
                {
                    mm.Subject = model.Subject;
                    mm.Body = model.Body;
                    mm.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.sendgrid.net";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("", "");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
