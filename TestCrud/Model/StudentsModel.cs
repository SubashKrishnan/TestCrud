using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCrud.Model
{
    public class StudentsModel
    {
        public Guid? ID { get; set; }
        public string NAME { get; set; }
        public int AGE { get; set; }
        public DateTime? DATE { get; set; }
    }
}
