public class DistanceCalculator
{
    public double Between(Location a, Location b)
    {
        var dLat = a.Lat - b.Lat;
        var dLng = a.Lng - b.Lng;
        return Math.Sqrt(dLat * dLat + dLng * dLng);
    }
}
