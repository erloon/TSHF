using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TSHF.Hangfire
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.SetServiceName("HangfireServer");
                x.SetDescription("Usługa windows service z serwerem hangfire");
                x.SetDisplayName("HangfireServer");
                x.RunAsLocalService();
                
                //x.OnException((exception) =>
                //{
                //    Console.WriteLine("Exception thrown - " + exception.Message);
                //});
                x.Service<HangfireServer>();
               // x.EnableServiceRecovery(r => { r.RestartService(1); });

            });

            var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());  //11
            Environment.ExitCode = exitCode;
        }
    }
}
