<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniRitter.UniRitter2015.Models
=======
﻿namespace UniRitter.UniRitter2015.Models
>>>>>>> 092c5a607f9abc539260bb374deee0650a740538
{
    public class PostModel
    {
        public Guid? id { get; set; }
        public Guid? authorId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string body { get; set; }

        [Required]
        [MaxLength(100)]
        public string title { get; set; }

        public string[] tags { get; set; }

        // public PersonModel author { get; set; }

    }
}