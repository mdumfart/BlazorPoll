using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Dtos
{
    public class RegisterResultDto
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
