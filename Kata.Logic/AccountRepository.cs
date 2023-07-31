using System.Collections.Generic;
using MongoDB.Driver;

namespace Kata.Logic;

public class AccountRepository
{
    private readonly MongoClient _client;
    private readonly IMongoCollection<Account> _accounts;

    public AccountRepository(string connectionString)
    {
        _client = new MongoClient(connectionString);
        _accounts = _client.GetDatabase("kata_mongodb").GetCollection<Account>("accounts");
    }
    
    public void CreateAccount(string title)
    {
        var account = new Account(title);
        _accounts.InsertOne(account);
    }

    public List<Account> Find()
    {
        return _accounts.Find(_ => true).ToList();
    }
}
