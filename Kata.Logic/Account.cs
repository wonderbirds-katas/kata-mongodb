using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kata.Logic;

public sealed partial class Account : IEquatable<Account>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string Title { get; set; }

    public Account(string title)
    {
        Title = title;
    }

    public bool Equals(Account other)
    {
        return Title.Equals(other?.Title);
    }
}
