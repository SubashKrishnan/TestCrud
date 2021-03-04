using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCrud.Model
{
    public class StudentMark
    {

        public Guid? ID { get; set; }
        public string NAME { get; set; }
        public int AGE { get; set; }
        public DateTime? DATE { get; set; }
       
        public int M1 { get; set; }
        public int M2 { get; set; }
        public int M3 { get; set; }
       
    }

    public class OneStudentMark:StudentMark
    { }
    
    public class GetValforOneStudentmark
    {
        public Guid ID { get; set; }
    }

    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

 

