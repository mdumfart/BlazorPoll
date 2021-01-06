using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
