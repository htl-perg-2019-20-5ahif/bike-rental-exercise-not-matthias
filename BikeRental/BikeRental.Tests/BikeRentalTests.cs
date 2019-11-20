using System;
using Xunit;

namespace BikeRental.Tests
{
    public class BikeRentalTests
    {
        [Fact]
        public void TestCalculationOfRentalPrice()
        {
            ICostCalculator calculator = new CostCalculator();

            var price = calculator.Calculate(new DateTime(2019, 2, 14, 8, 15, 0), new DateTime(2019, 2, 14, 10, 30, 0), 3, 5);
            Assert.Equal(13, price);

            price = calculator.Calculate(new DateTime(2018, 2, 14, 8, 15, 0), new DateTime(2018, 2, 14, 8, 45, 0), 3, 100);
            Assert.Equal(3, price);

            price = calculator.Calculate(new DateTime(2018, 2, 14, 8, 15, 0), new DateTime(2018, 2, 14, 8, 25, 0), 20, 100);
            Assert.Equal(0, price);

            Assert.Throws<InvalidDurationException>(() => calculator.Calculate(new DateTime(2019, 2, 14, 10, 30, 0), new DateTime(2018, 2, 14, 10, 30, 0), 3, 5));
        }
    }
}
