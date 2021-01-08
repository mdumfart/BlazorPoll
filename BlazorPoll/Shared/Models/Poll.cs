using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class Poll
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Question is required")]
        public string Question { get; set; }
        public bool IsMultipleChoice { get; set; } = false;
        public string Description { get; set; }
        public User Author { get; set; }
        [ValidateComplexType]
        public List<Answer> Answers { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
