using System;

namespace UniRitter.UniRitter2015.Support
{
    public class ApiConfig : UniRitter.UniRitter2015.Support.IApiConfig
    {
        public int Port
        {
            get { return GetEnvVar("PORT", 9000); }
        }

        public string MongoDbUrl
        {
            get { return GetEnvVar("MONGOLAB_URI", "mongodb://localhost"); }
        }

        public string MongoDbName
        {
            get { return GetEnvVar("MONGODB_NAME", "uniritter"); }
        }

        private T GetEnvVar<T>(string varName, T valueIfNull)
        {
            var val = Environment.GetEnvironmentVariable(varName);
            if (val != null)
            {
                return (T) Convert.ChangeType(val, typeof (T));
            }
            return valueIfNull;
        }
    }
}