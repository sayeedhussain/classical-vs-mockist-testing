public class DistanceCalculator : IDistanceCalculator
{
    public double Between(Location a, Location b)
    {
        var dLat = a.Lat - b.Lat;
        var dLng = a.Lng - b.Lng;
        return Math.Sqrt(dLat * dLat + dLng * dLng);
    }

    public double BetweenHaversine(Location a, Location b)
    {
        const double R = 6371000; // Earth's radius in meters

        double lat1 = DegreesToRadians(a.Lat);
        double lat2 = DegreesToRadians(b.Lat);
        double dLat = lat2 - lat1;
        double dLng = DegreesToRadians(b.Lng - a.Lng);

        double h = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(h), Math.Sqrt(1 - h));
        return R * c;
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}
