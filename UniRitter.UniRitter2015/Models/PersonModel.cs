using System;
using System.ComponentModel.DataAnnotations;

namespace UniRitter.UniRitter2015.Models
{
    public class PersonModel: IModel
    {
        public Guid? id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Tamanho maximo de 50 caracteres")]
        public string firstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Tamanho maximo de 50 caracteres")]
        public string lastName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Tamanho maximo de 50 caracteres")]
        [EmailAddress]
        public string email { get; set; }

        [MaxLength(200, ErrorMessage = "Tamanho maximo de 200 caracteres")]
        [RegularExpression("^http(s){0,1}://.+$")]
        public string url { get; set; }
    }
}