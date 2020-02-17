using System;
using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts
{
    public class Post
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public EntryOwner Owner { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}