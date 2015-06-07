using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniRitter.UniRitter2015.Models
{
    public class PersonModel
    {
        public Guid id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string url { get; set; }
    }
}