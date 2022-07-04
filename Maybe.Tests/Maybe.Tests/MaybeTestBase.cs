using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using NUnit.Framework;

namespace Maybe.Tests;

public class MaybeTestBase
{
    protected IFixture Fixture;
    
    [SetUp]
    public void SetUp()
    {
        Fixture = new Fixture();
        Fixture.Customize(new AutoFakeItEasyCustomization());
    }
}