using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Testcontainers.MongoDb;
using Xunit;

namespace Kata.Logic.Tests;

public class AccountRepositoryTests
{
    [Fact]
    public void CreateAccount_InsertsAccount()
    {
        var mongoContainer = new MongoDbBuilder()
            .WithPortBinding(27017, true)
            .WithUsername("root")
            .WithPassword("password")
            .WithCleanUp(true)
            .Build();
        
        Task.Run(async () => await mongoContainer.StartAsync()).Wait();

        try
        {
            const string accountTitle = "My First Account";
            var repository = new AccountRepository(mongoContainer.GetConnectionString());

            repository.CreateAccount(accountTitle);
            var accounts = repository.Find();

            // TODO: Why is the title of the returned object null?
            accounts.Should().HaveCount(1);
            accounts.First().Should().Be(new Account(accountTitle));
        }
        finally
        {
            Task.Run(async () => await mongoContainer.StopAsync()).Wait();
        }
    }
}