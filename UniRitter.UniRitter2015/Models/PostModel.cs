﻿using System;
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
        [MaxLength(400)]
        public string body { get; set; }

        [Required]
        [MaxLength(100)]
        public string title { get; set; }

        public PersonModel author { get; set; }

        public string[] tags { get; set; }
    }
}