using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCrossover.Models
{
    public class TokenResult
    {
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
