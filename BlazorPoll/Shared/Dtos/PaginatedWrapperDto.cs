using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Dtos
{
    public class PaginatedWrapperDto<T>
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public T Data { get; set; }
    }
}
