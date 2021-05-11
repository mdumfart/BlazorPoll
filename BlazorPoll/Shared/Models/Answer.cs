using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class Answer
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Answer text is required")]
        [StringLength(100, ErrorMessage="Your answer is too long.")]
        public string Content { get; set; }
        
        public int Count { get; set; }

        [Column(Order = 2)]
        public virtual Poll Poll { get; set; }
    }
}
