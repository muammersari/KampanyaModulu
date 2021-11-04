using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class VM<T>
    {
        public T data { get; set; }
        public string success { get; set; }
        public string message { get; set; }
    }
}
