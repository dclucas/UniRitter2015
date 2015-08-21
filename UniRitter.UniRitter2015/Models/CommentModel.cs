using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace UniRitter.UniRitter2015.Models
{
    public class CommentModel : IModel
    {
        public Guid? id { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Tamanho maximo de 4000 caracteres")]
        public string body { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tamanho maximo de 100 caracteres")]
        public string title { get; set; }

        public Guid authorId { get; set; }
    }
}