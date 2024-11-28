using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carRentalService;

        public CarController(CarService carRentalService)
        {
            _carRentalService = carRentalService;
        }

    
        [HttpGet]
        public IActionResult GetAvailableCars()
        {
            var cars = _carRentalService.GetAllCars();
            return Ok(cars);
        }

        [HttpPost]
       // [Authorize(Roles= "Admin")]
        public IActionResult AddCar( CarModel car)
        {
            if (car == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            _carRentalService.AddCar(car);

            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            var car = _carRentalService.GetCarById(id);

            if (car == null)
                return NotFound(new { message = "Car not found." });

            return Ok(car);
        }

        
        [HttpPut("{id}")]

        public IActionResult UpdateCar(int id, CarModel updatedCar)
        {
            if (updatedCar == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var car = _carRentalService.GetCarById(id);

            if (car == null)
                return NotFound(new { message = "Car not found." });

            _carRentalService.UpdateCar(id, updatedCar);
            return Ok(new { message = "Car details updated successfully." });
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _carRentalService.GetCarById(id);

            if (car == null)
                return NotFound(new { message = "Car not found." });

            _carRentalService.DeleteCar(id);
            return NoContent();
        }

        [HttpPost("{id}/rent")]
        public IActionResult RentCar(int id, int rentalDays)
        {
            var car = _carRentalService.GetCarById(id);

            if (car == null)
                return NotFound(new { message = "Car not found." });

            if (!car.IsAvailable)
                return BadRequest(new { message = "Car is not available for rent." });

            var totalCost = _carRentalService.RentCar(id, rentalDays);
            return Ok(new { message = $"Car rented successfully. Total cost: {totalCost}" });
        }
    }
}
