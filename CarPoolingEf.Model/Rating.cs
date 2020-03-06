using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Models
{
    public class Feedback
    {
        public string Id { get; set; }

        public float Rating { get; set; }

        public string Comment { get; set; }
    }
}
