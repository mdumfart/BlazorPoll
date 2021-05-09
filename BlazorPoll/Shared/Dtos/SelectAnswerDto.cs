using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Shared.Dtos
{
    public class SelectAnswerDto
    {
        public Answer Answer { get; set; }
        public bool Add { get; set; }

        public SelectAnswerDto(Answer answer, bool add)
        {
            Answer = answer;
            Add = add;
        }
    }
}
