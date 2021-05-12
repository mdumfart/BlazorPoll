using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlazorPoll.Shared.Models
{
    public class Poll
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Question is required")]
        public string Question { get; set; }
        
        public bool IsMultipleChoice { get; set; } = false;
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public virtual List<Answer> Answers { get; set; }
        
        public virtual IdentityUser Author { get; set; }

        [ValidateComplexType]
        public virtual List<Comment> Comments { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Id: {Id} ");
            sb.Append($"Question: {Question} ");
            sb.Append($"Number of answers: {Answers.Count}");

            return sb.ToString();
        }
    }
}
