using System;
using System.IO;
using System.Linq;
using IQueryableTask1.E3SClient;
using IQueryableTask1.E3SClient.Entities;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace IQueryableTask1
{
    public class ConfigurationFixture : IDisposable
    {
        public static IConfiguration Configuration;
        public ConfigurationFixture()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

        }

        public void Dispose()
        {
        }
    }
    
    public class E3SProviderTests : IClassFixture<ConfigurationFixture>
    {
        protected ConfigurationFixture ConfigurationFixture;

        public E3SProviderTests(ConfigurationFixture configurationFixture)
        {
            ConfigurationFixture = configurationFixture;
        }

        [Fact]
        public void WithoutProvider()
        {
            var client = new E3SQueryClient(ConfigurationFixture.Configuration["user"], ConfigurationFixture.Configuration["password"]);
            var res = client.SearchFTS<EmployeeEntity>("workstation:(EPRUIZHW0249)", 0, 1);

            foreach (var emp in res)
            {
                Console.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            }
        }

        [Fact]
        public void WithoutProviderNonGeneric()
        {
            var client = new E3SQueryClient(ConfigurationFixture.Configuration["user"], ConfigurationFixture.Configuration["password"]);            
            var res = client.SearchFTS(typeof(EmployeeEntity), "workstation:(EPRUIZHW0249)", 0, 10);

            foreach (var emp in res.OfType<EmployeeEntity>())
            {
                Console.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            }
        }

        [Fact]
        public void WithProvider()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationFixture.Configuration["user"], ConfigurationFixture.Configuration["password"]);

            foreach (var emp in employees.Where(e => e.workStation == "EPRUIZHW0249"))
            {
                Console.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            }
        }
    }
}
