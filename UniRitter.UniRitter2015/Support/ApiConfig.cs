using System;

namespace UniRitter.UniRitter2015.Support
{
    public class ApiConfig
    {
        public int Port
        {
            get { return GetEnvVar("PORT", 9000); }
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