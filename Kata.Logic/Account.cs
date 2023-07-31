using System;

namespace Kata.Logic;

public sealed partial class Account : IEquatable<Account>
{
    public string _id { get; set; }
    
    public string Title { get; }

    public Account(string title)
    {
        Title = title;
    }

    public bool Equals(Account other)
    {
        return Title.Equals(other?.Title);
    }
}
