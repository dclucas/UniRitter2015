using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniRitter.UniRitter2015.SelfHosted.Support
{
    public class ApiConfig
    {
        private T GetEnvVar<T>(string varName, T valueIfNull)
        {
            var val = Environment.GetEnvironmentVariable(varName);
            if (val != null)
            {
                return (T)Convert.ChangeType(val, typeof (T));
            }
            return valueIfNull;
        }

        public int Port
        {
            get { return GetEnvVar("PORT", 9000); }
        }
    }
}
