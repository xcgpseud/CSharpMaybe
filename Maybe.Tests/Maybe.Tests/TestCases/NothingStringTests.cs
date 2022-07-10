using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Maybe.Exceptions;
using Maybe.Interfaces;
using NUnit.Framework;

namespace Maybe.Tests.TestCases;

public class NothingStringTests : MaybeTestBase
{
    private const string DefaultValue = "Mercedes";
    private const string InvalidValue = "Volvo";
    private readonly string[] _invalidValues = { "Volkswagen", "BMW" };

    private const string ExceptionMessage = "This is an exception";

    private readonly Func<string, bool> _invalidityCheck = i => i == InvalidValue;


    [TestCase(null)]
    public async Task MaybeWithJustAnInvalidValue_BehavesCorrectly(string invalidValue)
    {
        var sut = Maybe<string>.From(invalidValue);

        await TestNothingValidity(sut);
    }

    [TestCase(null)]
    [TestCase("Volvo")]
    public async Task MaybeWithInvalidityCheck_BehavesCorrectly(string invalidValue)
    {
        var sut = Maybe<string>.From(invalidValue, _invalidityCheck);

        await TestNothingValidity(sut);
    }

    [TestCase(null)]
    [TestCase("Volvo")]
    [TestCase("BMW")]
    public async Task MaybeWithInvalidityCheckAndInvalidValues_BehavesCorrectly(string invalidValue)
    {
        var sut = Maybe<string>.From(invalidValue, _invalidityCheck, _invalidValues);

        await TestNothingValidity(sut);
    }

    [TestCase(null)]
    [TestCase("BMW")]
    public async Task MaybeWithOnlyInvalidValues_BehavesCorrectly(string invalidValue)
    {
        var sut = Maybe<string>.From(invalidValue, _invalidValues);

        await TestNothingValidity(sut);
    }

    private async Task TestNothingValidity(IMaybe<string> maybe)
    {
        maybe.Should().BeOfType<Nothing<string>>();

        // Assert the basic checks
        maybe.GetOrElse(DefaultValue).Should().Be(DefaultValue);
        maybe.GetOrCall(() => DefaultValue).Should().Be(DefaultValue);

        // Assert the exception throwing methods
        maybe.Invoking(x => x.Get()).Should().Throw<MaybeValueNotFoundException>();
        maybe.Invoking(x => x.GetOrThrow(new Exception(ExceptionMessage)))
            .Should().Throw<Exception>().WithMessage(ExceptionMessage);

        // Assert the async stuff
        var asyncResult = await maybe.GetOrCallAsync(async () => await Task.FromResult(DefaultValue));
        asyncResult.Should().Be(DefaultValue);
    }
}