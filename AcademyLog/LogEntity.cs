using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyLog
{
    public class LogEntity
    {
        public int id { get; set; }
        public string aplicacion { get; set; }
        public string mensaje { get; set; }
        public System.DateTime fecha { get; set; }
    }
}
