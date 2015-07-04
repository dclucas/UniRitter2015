using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniRitter.UniRitter2015.Models
{
    public class PostModel
    {

        public Guid? id { get; set; }

        [Required]
        [MaxLength(4000)]
        public string body { get; set; }

        [Required]
        [MaxLength(100)]
        public string title { get; set; }

        //coloar um guid pro id
        public PersonModel author {get; set;}

        public string[] tags {get; set;}

    }
}