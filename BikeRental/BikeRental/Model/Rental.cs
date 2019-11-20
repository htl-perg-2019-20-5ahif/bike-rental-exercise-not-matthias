using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRental.Model
{
    [NotMapped]
    public class StartRentalRequest
    {
        public int RenterId { get; set; }
        public int BikeId { get; set; }
    }

    public class Rental
    {
        public int RentalId { get; set; }

        [Required]
        public DateTime RentalBegin { get; set; }

        public DateTime? RentalEnd { get; set; }

        [Range(0, double.PositiveInfinity)]
        [RegularExpression("^\\d(\\.\\d{1,2})$")]
        public decimal? TotalRentalCosts { get; set; }

        [Required]
        public bool Paid { get; set; }


        public int RenterId { get; set; }

        [Required]
        public Customer Renter { get; set; }

        public int BikeId { get; set; }

        [Required]
        public Bike Bike { get; set; }
    }
}