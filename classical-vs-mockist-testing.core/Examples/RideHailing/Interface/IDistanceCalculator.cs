public interface IDistanceCalculator
{
    public double Between(Location a, Location b);

    public double BetweenHaversine(Location a, Location b);
}