using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeRental.Model
{
    public enum BikeSort
    {
        None,
        PriceOfFirstHour,
        PriceOfAdditionalHours,
        PurchaseDate
    }

    public enum BikeCategory
    {
        StandardBike,
        MountainBike,
        TreckingBike,
        RacingBike
    }

    public class Bike
    {
        public int BikeId { get; set; }

        [Required, MaxLength(25)]
        public string Brand { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public DateTime? LastService { get; set; }

        [Required, Range(0, double.PositiveInfinity)]
        [RegularExpression("^\\d(\\.\\d{1,2})$")]
        public decimal RentalPriceFirstHour { get; set; }

        [Required, Range(0, double.PositiveInfinity)]
        [RegularExpression("^\\d(\\.\\d{1,2})$")]
        public decimal RentalPriceAdditionalHours { get; set; }

        [Required]
        public BikeCategory BikeCategory { get; set; }


        public List<Rental> Rentals { get; set; }
    }
}
