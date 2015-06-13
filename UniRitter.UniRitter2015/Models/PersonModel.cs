﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniRitter.UniRitter2015.Models
{
    public class PersonModel
    {
        public Guid? id { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [RegularExpression("^http(s){0,1}://.+$")]
        public string url { get; set; }
    }
}