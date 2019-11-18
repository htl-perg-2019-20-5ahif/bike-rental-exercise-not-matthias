using System;
using System.ComponentModel.DataAnnotations;

namespace BikeRental.Model
{
    public class Rental
    {
        public int RentalId { get; set; }

        [Required]
        public DateTime RentalBegin { get; set; }

        public DateTime? RentalEnd { get; set; }

        [Range(0, double.PositiveInfinity)]
        public decimal? TotalRentalCosts { get; set; }

        [Required]
        public bool Paid { get; set; }


        public int RenterId { get; set; }

        [Required]
        public Customer Renter { get; set; }

        public int BikeId { get; set; }

        [Required]
        public Bike RentedBike { get; set; }
    }
}
