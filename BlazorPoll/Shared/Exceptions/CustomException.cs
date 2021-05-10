using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Exceptions
{
    public abstract class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public new string Message { get; set; }
    }
}
