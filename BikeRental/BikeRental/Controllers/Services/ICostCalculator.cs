using System;

namespace BikeRental
{
    public interface ICostCalculator
    {
        public decimal Calculate(DateTime start, DateTime end, decimal rentalPriceFirstHour, decimal rentalPriceAdditionalHours);
    }
}