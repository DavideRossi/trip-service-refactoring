using NUnit.Framework;
using TripServiceKata.Services;
using TripServiceKata.Model;
using System.Collections.Generic;
using TripServiceKata.Exception;
using NSubstitute;

namespace TripServiceKata.Tests;

[TestFixture]
public class TripServiceTest
{
    [Test]
    public void DummyTest()
    {
        // Arrange
        List<int> ints = [1, 2, 3];
        // Act
        ints.Add(4);
        // Assert
        Assert.That(ints.Count, Is.EqualTo(4));
    }
}
