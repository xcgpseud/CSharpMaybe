using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Maybe.Tests.FakeStuff.Entities;
using Maybe.Tests.FakeStuff.Repositories;
using NUnit.Framework;

namespace Maybe.Tests.Tests;

public class JustTests : MaybeTestBase
{
    private PersonRepository _repository;

    private readonly List<Guid> _createdGuids = new();

    [SetUp]
    public void LocalSetUp()
    {
        _repository = new PersonRepository();
        
        for (var i = 0; i < 5; i++)
        {
            var guid = Guid.NewGuid();

            _createdGuids.Add(guid);

            var person = Fixture
                .Build<PersonEntity>()
                .With(person => person.Guid, guid)
                .Create();

            _repository.Create(person);
        }
    }

    [Test]
    public void Maybe_ReceivesActualValue_IsValidJust()
    {
        var guid = _createdGuids.First();
        var exceptionMessage = "Derpity derp, y'failed.";

        var repositoryResult = _repository.Get(guid);

        repositoryResult.Should().NotBeNull();
        repositoryResult?.Guid.Should().Be(guid);

        var maybe = Maybe<PersonEntity>.From(repositoryResult);

        maybe.Should().BeOfType<Just<PersonEntity>>();

        maybe.Get().Should().Be(repositoryResult);
        maybe.GetOrCall(() => Fixture.Create<PersonEntity>()).Should().Be(repositoryResult);
        maybe.GetOrElse(Fixture.Create<PersonEntity>()).Should().Be(repositoryResult);
        maybe.GetOrThrow(new Exception(exceptionMessage)).Should().Be(repositoryResult);

        maybe.IsEmpty().Should().BeFalse();
        maybe.IsNotEmpty().Should().BeTrue();
    }
}