using System;
using System.ComponentModel.DataAnnotations;

namespace UniRitter.UniRitter2015.Models
{
    public class PersonModel: IModel
    {
        public Guid? id { get; set; }

        [Required]
        [MaxLength(100)]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Url]
        public string url { get; set; }
    }
}