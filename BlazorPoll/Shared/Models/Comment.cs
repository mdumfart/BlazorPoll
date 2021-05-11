using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Provide a comment")]
        public string Content { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public virtual User Author { get; set; }

        public virtual Poll Poll { get; set; }
    }
}
