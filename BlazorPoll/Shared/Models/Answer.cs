using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        [Required(ErrorMessage = "Answer text is required")]
        [StringLength(100, ErrorMessage="Your answer is too long.")]
        public string Content { get; set; }
        public int Count { get; set; }

        public Answer()
        {
            Id = -1;
            PollId = -1;
            Content = "";
            Count = 0;
        }

        public Answer(int id)
        {
            Id = id;
            PollId = -1;
            Content = "";
            Count = 0;
        }
    }
}
