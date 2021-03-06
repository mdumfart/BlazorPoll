﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPoll.Shared.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
