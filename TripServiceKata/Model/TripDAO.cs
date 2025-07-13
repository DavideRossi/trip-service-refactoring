using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.Model;

namespace TripServiceKata.Services
{
    public class TripDAO
    {
        public static List<Trip> FindTripsByUser(User user)
        {
            throw new DependentClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }

        public virtual List<Trip> TripsBy(User user)
        {
            return FindTripsByUser(user);
        }
    }
}
