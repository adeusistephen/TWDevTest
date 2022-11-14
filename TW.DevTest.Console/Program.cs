// See https://aka.ms/new-console-template for more information


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TW.DevTest.Core.Interfaces;
using TW.DevTest.Infrastructure;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            BuildConfig(configBuilder);

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ISimpleLogger, SimpleLogger>();
                    services.AddTransient<ITestLogger, TestLogger>();

                }).Build();

            var svc = ActivatorUtilities.CreateInstance<TestLogger>(host.Services);

            svc.LoopLogger();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.Build();
        }
    }


    public class TestLogger : ITestLogger
    {
        private readonly ISimpleLogger logger;

        public TestLogger(ISimpleLogger logger)
        {
            this.logger = logger;
        }

        public void LoopLogger()
        {
            for (int i = 0; i < 10; i++)
            {
               logger.Info($"test simple logger: {i}");
            }

        }
    }

    public interface ITestLogger
    {
        void LoopLogger();
    }
}
