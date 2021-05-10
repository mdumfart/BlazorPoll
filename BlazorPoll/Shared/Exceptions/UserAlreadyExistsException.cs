using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Exceptions
{
    public class UserAlreadyExistsException : CustomException
    {
        private const string PREFIX = "Username";
        private const string SUFFIX = "already exists";
        
        public UserAlreadyExistsException(string userName)
        {
            StatusCode = 400;
            StatusDescription = HttpStatusCode.BadRequest.ToString();
            Message = $"{PREFIX} {userName} {SUFFIX}";
        }
    }
}
