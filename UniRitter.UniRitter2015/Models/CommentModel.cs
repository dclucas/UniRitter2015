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
    public class CommentModel
    {
        public Guid? id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string body { get; set; }

        [Required]
        [MaxLength(50)]
        public string title { get; set; }

        public PersonModel author { get; set; }

    }
}