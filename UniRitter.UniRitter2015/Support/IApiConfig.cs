using System;
namespace UniRitter.UniRitter2015.Support
{
    public interface IApiConfig
    {
        string FullUrl { get; }
        string MongoDbName { get; }
        string MongoDbUrl { get; }
        int Port { get; }
    }
}
