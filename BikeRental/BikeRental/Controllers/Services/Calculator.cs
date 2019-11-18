using System;

namespace BikeRental
{
    public class Calculator : ICalculator
    {
        public decimal Calculate(DateTime start, DateTime end, decimal priceFirstHour, decimal priceAdditionalHours)
        {
            //
            // Get the duration
            //
            if (end < start)
            {
                throw new InvalidDurationException();
            }

            var duration = end - start;
            if (duration < TimeSpan.Zero)
            {
                throw new InvalidDurationException();
            }

            //
            // Calculate first hour
            //
            decimal totalPrice = 0;
            if (duration.TotalMinutes >= 15)
            {
                totalPrice += priceFirstHour;
            }

            //
            // Calculate the additional hours
            // 
            var additionalHours = (int)Math.Ceiling((duration - TimeSpan.FromHours(1)).TotalHours);
            if (additionalHours > 0)
            {
                totalPrice += additionalHours * priceAdditionalHours;
            }

            return totalPrice;
        }
    }
}
