using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniRitter.UniRitter2015.Models
{
    public class CommentModel: IModel
    {
        public Guid? id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string body { get; set; }

        [Required]
        [MaxLength(50)]
        public string title { get; set; }

        [Required]
        public PersonModel author { get; set; }


        /*
        
CommentModel
         
Com base em PersonController, criar/editar PostController e CommentController para implementar POST, PUT, UPDATE, GET/ID e GET.

Deixar o resultado desses métodos "vazios" (retornar dados fictícios).
         
         */
    }
}