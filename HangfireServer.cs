using System;
using System.Linq.Expressions;
using System.Timers;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace TSHF.Hangfire
{
    public class HangfireServer : ServiceControl
    {
        private BackgroundJobServer _server;
        private IDisposable _webApp;

        public bool Start(HostControl hostControl)
        {
            ConfigureHangfire();
            ConfigureDashboard();
            ConfigureJobs();

            var options = new BackgroundJobServerOptions()
            {
                ServerName = "Nowy Serwer",

            };
            _server = new BackgroundJobServer(options);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _webApp.Dispose();
            return true;
        }

        public void ConfigureHangfire()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["mssqlconnection"].ConnectionString;
            var options = new SqlServerStorageOptions()
            {
                SchemaName = "mod"
            };
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(connection, options);
        }

        public void ConfigureJobs()
        {
            RecurringJob.AddOrUpdate(recurringJobId: "recurringJob_test", methodCall: () => CallWcfLog(), cronExpression: Cron.MinuteInterval(1), timeZone: TimeZoneInfo.Local);
        }

        public void ConfigureDashboard()
        {
            StartOptions options = new StartOptions();

            options.Urls.Add("http://localhost:9095");
            options.Urls.Add("http://127.0.0.1:9095");
            options.Urls.Add($"http://{Environment.MachineName}:9095");

            _webApp = WebApp.Start<Startup>(options);
        }

        public void CallWcfLog()
        {
            Console.WriteLine("Dodano log");
        }
    }
}