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
    private class TestableTripService(User loggedUser, TripDAO tripDAO) : TripService
    {
        private readonly User loggedUser = loggedUser;
        private readonly TripDAO tripDAO = tripDAO;

        protected override User GetLoggedUser()
        {
            return loggedUser;
        }

        protected override List<Trip> GetTrips(User user)
        {
            return tripDAO.TripsBy(user);
        }
    }

    [Test]
    public void GetTripsByUser_WhenUserIsLoggedInAndIsFriend_ReturnsTrips()
    {
        // Arrange
        User user = new();
        User loggedUser = new();
        user.AddFriend(loggedUser);
        Trip trip1 = new() { Destination = "Paris" };
        Trip trip2 = new() { Destination = "London" };
        user.AddTrip(trip1);
        user.AddTrip(trip2);
        var mockTripDAO = Substitute.For<TripDAO>();
        mockTripDAO.TripsBy(user).Returns([trip1, trip2]);

        TripService tripService = new TestableTripService(loggedUser, mockTripDAO);
        // Act
        List<Trip> trips = tripService.GetTripsByUser(user);
        // Assert
        Assert.That(trips, Is.Not.Null);
        Assert.That(trips, Has.Count.EqualTo(2));
        Assert.That(trips, Is.EquivalentTo([trip2, trip1]));
    }

    [Test]
    public void GetTripsByUser_WhenUserIsNotLoggedIn_ThrowsUserNotLoggedInException()
    {
        // Arrange
        User user = new();
        User loggedUser = null;
        TripService tripService = new TestableTripService(loggedUser, new TripDAO());
        // Act & Assert
        Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(user));
    }

    [Test]
    public void GetTripsByUser_WhenUserIsLoggedInButNotAFriend_ReturnsEmptyList()
    {
        // Arrange
        User user = new();
        User loggedUser = new();
        Trip trip1 = new() { Destination = "Rome" };
        Trip trip2 = new() { Destination = "Bruxelles" };
        user.AddTrip(trip1);
        user.AddTrip(trip2);
        TripService tripService = new TestableTripService(loggedUser, new TripDAO());
        // Act
        List<Trip> trips = tripService.GetTripsByUser(user);
        // Assert
        Assert.That(trips, Is.Not.Null);
        Assert.That(trips, Is.Empty);
    }
}
