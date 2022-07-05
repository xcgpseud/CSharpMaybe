using System;
using AutoFixture;
using FluentAssertions;
using Maybe.Exceptions;
using Maybe.Tests.FakeStuff.Entities;
using Maybe.Tests.FakeStuff.Repositories;
using NUnit.Framework;

namespace Maybe.Tests.Tests;

public class NothingTests : MaybeTestBase
{
    private PersonRepository _repository;

    [SetUp]
    public void LocalSetUp()
    {
        _repository = new PersonRepository();
    }

    [Test]
    public void Maybe_ReceivesNull_IsValidNothing()
    {
        var exceptionMessage = "Derpity derp, y'failed.";

        var defaultPersonEntity = Fixture.Create<PersonEntity>();
        
        var repositoryResult = _repository.Get(Guid.NewGuid());

        repositoryResult.Should().BeNull();

        var sut = Maybe<PersonEntity>.From(repositoryResult);

        sut.Should().BeOfType<Nothing<PersonEntity>>();

        sut.Invoking(x => x.Get()).Should().Throw<MaybeValueNotFoundException>();
        sut.GetOrCall(() => defaultPersonEntity).Should().Be(defaultPersonEntity);
        sut.GetOrElse(defaultPersonEntity).Should().Be(defaultPersonEntity);
        sut.Invoking(x => x.GetOrThrow(new Exception(exceptionMessage)))
            .Should().Throw<Exception>()
            .WithMessage(exceptionMessage);

        sut.IsEmpty().Should().BeTrue();
        sut.IsNotEmpty().Should().BeFalse();
    }
}