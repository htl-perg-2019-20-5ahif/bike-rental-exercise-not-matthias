using System;

namespace BikeRental
{
    public interface ICalculator
    {
        public decimal Calculate(DateTime start, DateTime end, decimal rentalPriceFirstHour, decimal rentalPriceAdditionalHours);
    }
}