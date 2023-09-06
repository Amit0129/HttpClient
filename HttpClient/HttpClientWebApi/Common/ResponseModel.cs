using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResponseModel
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSucess { get; set; }
    }
}
