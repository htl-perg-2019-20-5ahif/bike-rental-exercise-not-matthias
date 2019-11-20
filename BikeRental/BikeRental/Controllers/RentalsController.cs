using BikeRental.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web;

namespace BikeRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly BikeRentalDbContext _context;
        private readonly ICostCalculator _calculator;

        public RentalsController(BikeRentalDbContext context, ICostCalculator calculator)
        {
            _context = context;
            _calculator = calculator;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            return await _context.Rentals
                .Include(rental => rental.Bike)
                .Include(rental => rental.Renter)
                .ToListAsync();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            return rental;
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rental>> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return rental;
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalId == id);
        }


        // POST: api/Rentals/start
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental([FromBody] StartRentalRequest request)
        {
            var renter = await _context.Customers.FindAsync(request.RenterId);
            if (renter is null)
            {
                return BadRequest("Invalid renter id");
            }

            var bike = await _context.Bikes.FindAsync(request.RenterId);
            if (renter is null)
            {
                return BadRequest("Invalid bike id");
            }

            var rental = new Rental
            {
                RentalBegin = DateTime.Now,
                RentalEnd = default,
                TotalRentalCosts = default,
                Paid = false,
                RenterId = request.RenterId,
                Renter = renter,
                BikeId = request.BikeId,
                Bike = bike
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental", new
            {
                id = rental.RentalId
            }, rental);
        }

        // POST: api/Rentals/5/end
        [HttpPost("{id}/end")]
        public async Task<ActionResult<Rental>> EndRental(int id)
        {
            //
            // Get the rental object
            //
            var rental = await _context.Rentals
                .Include(rental => rental.Bike)
                .Include(rental => rental.Renter)
                .FirstOrDefaultAsync(rental => rental.RentalId == id);

            // Invalid object
            if (rental == default)
            {
                return NotFound();
            }

            // Object already evaluated
            if (rental.RentalEnd != default)
            {
                return BadRequest();
            }

            //
            // Update the object
            //
            rental.RentalEnd = DateTime.Now;
            rental.TotalRentalCosts = _calculator.Calculate(
                    rental.RentalBegin,
                    rental.RentalEnd.GetValueOrDefault(),
                    rental.Bike.RentalPriceFirstHour,
                    rental.Bike.RentalPriceAdditionalHours
                );

            //
            // Save the object
            //
            await PutRental(rental.RentalId, rental);

            return rental;
        }

        // POST: api/Rentals/5/paid
        [HttpPost("{id}/paid")]
        public async Task<ActionResult<Rental>> PayRental(int id)
        {
            //
            // Get the rental object
            //
            var rental = await _context.Rentals
                 .Include(rental => rental.Bike)
                 .Include(rental => rental.Renter)
                 .FirstOrDefaultAsync(rental => rental.RentalId == id);

            // Invalid object
            if (rental == default)
            {
                return NotFound();
            }

            // Invalid price or not yet ended
            if (rental.TotalRentalCosts > 0m || rental.RentalEnd == default)
            {
                return BadRequest();
            }

            //
            // Update the object
            //
            rental.Paid = true;

            //
            // Save the object
            //
            await PutRental(rental.RentalId, rental);

            return rental;
        }
    }
}
