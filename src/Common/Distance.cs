using System;
using System.Collections.Generic;

namespace StatementIQ
{
    public static class Distance
    {
        public enum Unit : byte
        {
            Kilometers = 1,
            Miles
        }

        private static readonly IDictionary<Unit, double> DistanceUnit = new Dictionary<Unit, double>
        {
            {Unit.Kilometers, 1.609344},
            {Unit.Miles, 0.8684}
        };

        public static double Calculate(double lat1, double lon1, double lat2, double lon2, Unit unit)
        {
            var theta = lon1 - lon2;

            var dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) +
                       Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
                       Math.Cos(Deg2Rad(theta));

            dist = Math.Acos(dist);
            dist = Rad2Deg(dist);

            return dist * 60 * 1.1515 * DistanceUnit[unit];
        }

        private static double Deg2Rad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        private static double Rad2Deg(double rad)
        {
            return rad / Math.PI * 180.0;
        }
    }
}