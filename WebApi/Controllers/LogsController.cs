using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using LogDataAccess;

namespace WebApi.Controllers
{
    public class LogsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save([FromBody] Log log)
        {
            DateTime logCST = log.fecha.AddHours(-6);
            log.fecha = logCST;
            using (Team4RMEntities BD = new Team4RMEntities())
            {
                BD.Log.Add(log);
                BD.SaveChanges();
            }
            SendEmail(log);

            return Request.CreateResponse(HttpStatusCode.Created, log);
        }

        [HttpGet]
        public IEnumerable<LogDataAccess.Log> Get()
        {
            using (Team4RMEntities BD = new Team4RMEntities())
            {
                var listado = BD.Log.ToList();
                return listado;
            }
        }

        private void SendEmail(Log log)
        {
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();


            mmsg.To.Add("softtekt4@gmail.com");

            mmsg.Subject = log.mensaje;
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            mmsg.Body = log.mensaje;
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true;

            mmsg.From = new System.Net.Mail.MailAddress("softtekt4@gmail.com");

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            //cliente.UseDefaultCredentials = false;
            cliente.Credentials = new System.Net.NetworkCredential("softtekt4@gmail.com", "Soporte01");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com"; //en caso de estar usando otro dominio es mail.dominio.com
            try
            {
                cliente.Send(mmsg);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
