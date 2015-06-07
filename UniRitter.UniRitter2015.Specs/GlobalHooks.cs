using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public sealed class GlobalHooks
    {
        private static Process _iisProcess;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var thread = new Thread(StartIisExpress) { IsBackground = true };

            thread.Start();
        }

        private static void StartIisExpress()
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Minimized,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/site:\"{0}\"", "UniRitter.UniRitter2015")
            };

            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles(x86)"])
                                ? startInfo.EnvironmentVariables["programfiles"]
                                : startInfo.EnvironmentVariables["programfiles(x86)"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";


            try
            {
                _iisProcess = new Process { StartInfo = startInfo };

                _iisProcess.Start();
                _iisProcess.WaitForExit();
            }
            catch (Exception exc)
            {
                //_iisProcess.CloseMainWindow();
                //_iisProcess.Dispose();
                throw exc;
            }
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            if (_iisProcess.Handle != null && !_iisProcess.HasExited)
            {
                _iisProcess.CloseMainWindow();
                _iisProcess.Dispose();
            }
        }
    }
}
