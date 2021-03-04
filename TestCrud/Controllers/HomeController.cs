using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TestCrud.Model;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using TestCrud.Helpers;

namespace TestCrud.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpPost("Index")]
        public IActionResult Index([FromBody] EmailModel model)
        {
            try
            {
                SendMail sendMail = new SendMail();
                sendMail.Send(model);
            }
            catch(Exception ex)
            { 
                throw ex;
            }
            return Ok("message sent successfully");
        }
    }
}