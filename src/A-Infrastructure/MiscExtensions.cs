using System;
namespace AInfrastructure
{
    public static class MiscExtensions
    {
        public static double Round(this double d, int numberOfPlaces)
        {
            return Math.Round(d, numberOfPlaces, MidpointRounding.AwayFromZero);
        }
    }
}
