using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCrud.Model
{
    public class MarksModel
    {
        public Guid STUDENT_ID { get; set; }
        public int M1 { get; set; }
        public int M2 { get; set; }
        public int M3 { get; set; }
        public DateTime? DATE { get; set; }

    }
}
