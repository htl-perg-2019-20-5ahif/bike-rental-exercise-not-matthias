using System;
using System.ComponentModel.DataAnnotations;

namespace web
{
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }

    // TODO: CASCADE DELETE
    public class Customer
    {
        [Required]
        public Gender Gender { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(75)]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required, MaxLength(75)]
        public string Street { get; set; }

        [MaxLength(10)]
        public int HouseNumber { get; set; }

        [Required, MaxLength(10)]
        public int ZipCode { get; set; }

        [Required, MaxLength(75)]
        public string Town { get; set; }
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
        [Required, MaxLength(25)]
        public string Brand { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public DateTime LastService { get; set; }

        [Required]
        // TODO: Add precision in the context
        public decimal RentalPriceFirstHour { get; set; }

        [Required]
        // TODO: Add precision in the context
        public decimal RentalPriceAdditionalHours { get; set; }

        public BikeCategory BikeCategory { get; set; }
    }

    public class Rental
    {
        // TODO: Reference to the customer
        // TODO: Reference to the Bike

        [Required]
        public DateTime RentalBegin { get; set; }

        public DateTime RentalEnd { get; set; }

        public decimal TotalRentalCosts { get; set; }

        public bool Paid { get; set; }
    }
}
