using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace UniRitter.UniRitter2015.Models
{
    public class CommentModel : IModel
    {
        public Guid? id { get; set; }

        [Required]
        [MaxLength(4000)]
        public string body { get; set; }

        [Required]
        [MaxLength(100)]
        public string title { get; set; }

        public Guid authorId { get; set; }
    }
}