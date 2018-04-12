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
        public void Where_MemberEqualsConstant_MatchesOriginal()
        {
            var directReply = new E3SQueryClient(ConfigurationFixture.Configuration["user"], ConfigurationFixture.Configuration["password"])
                .SearchFTS<EmployeeEntity>("workstation:(EPRUIZHW0249)", 0, 1).ToList();

            var providerReply = new E3SEntitySet<EmployeeEntity>(
                    ConfigurationFixture.Configuration["user"],
                    ConfigurationFixture.Configuration["password"])
                .Where(e => e.workStation == "EPRUIZHW0249").ToList();

            Assert.Equal(directReply, providerReply);
        }

        [Fact]
        public void Where_ConstantEqualsMember_MatchesOriginal()
        {
            var directReply = new E3SQueryClient(ConfigurationFixture.Configuration["user"], ConfigurationFixture.Configuration["password"])
                .SearchFTS<EmployeeEntity>("workstation:(EPRUIZHW0249)", 0, 1).ToList();

            var providerReply = new E3SEntitySet<EmployeeEntity>(
                    ConfigurationFixture.Configuration["user"],
                    ConfigurationFixture.Configuration["password"])
                .Where(e => "EPRUIZHW0249" == e.workStation).ToList();

            Assert.Equal(directReply, providerReply);
        }
    }
}
