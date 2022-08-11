using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCrossover.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string profilepicture { get; set; }
        public string location { get; set; }
        public DateTime createdat { get; set; }
    }
}
