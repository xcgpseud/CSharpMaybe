using System;

namespace Maybe.Tests.FakeStuff.Entities;

public class PersonEntity
{
    public Guid Guid { get; set; } = Guid.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; } = default;
}