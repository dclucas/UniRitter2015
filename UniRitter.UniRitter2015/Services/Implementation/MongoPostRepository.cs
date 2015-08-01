using System;
using System.Collections.Generic;
using MongoDB.Driver;
using UniRitter.UniRitter2015.Models;

namespace UniRitter.UniRitter2015.Services.Implementation
{
    public class MongoPostRepository : MongoRepository<PostModel>
    {
        public MongoPostRepository()
            : base("posts")
        {
        }
    }
}