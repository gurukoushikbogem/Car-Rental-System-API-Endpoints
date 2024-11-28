using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class CarRepository
    {
        private readonly DbContextcs _dbContext;

        public CarRepository(DbContextcs dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CarModel> GetAllCars()
        {
            return _dbContext.Cars.ToList();
        }

        public CarModel GetCarById(int carId)
        {
            return _dbContext.Cars.FirstOrDefault(car => car.Id == carId);
        }

        public List<CarModel> GetAvailableCars()
        {
            return _dbContext.Cars.Where(car => car.IsAvailable).ToList();
        }

        public string AddCar(CarModel newCar)
        {
            if (newCar == null)
            {
                return "Car object is null.";
            }
            _dbContext.Cars.Add(newCar);
            _dbContext.SaveChanges();
            return "Car added successfully.";
        }

        public string DeleteCar(int carId)
        {
            var carToDelete = GetCarById(carId);
            if (carToDelete == null)
            {
                return $"Car with ID {carId} not found.";
            }

            _dbContext.Cars.Remove(carToDelete);
            _dbContext.SaveChanges();
            return "Car deleted successfully.";
        }

        public string UpdateCar(CarModel updatedCar)
        {
            if (updatedCar == null)
            {
                return "Car object is null.";
            }

            var existingCar = GetCarById(updatedCar.Id);
            if (existingCar == null)
            {
                return $"Car with ID {updatedCar.Id} not found.";
            }

            existingCar.Makes = updatedCar.Makes;
            existingCar.Model = updatedCar.Model;
            existingCar.Year = updatedCar.Year;
            existingCar.PricePerDay = updatedCar.PricePerDay;
            existingCar.IsAvailable = updatedCar.IsAvailable;

            _dbContext.SaveChanges();
            return "Car updated successfully.";
        }

        public string UpdateCarAvailability(int carId, bool isAvailable)
        {
            var carToUpdate = GetCarById(carId);
            if (carToUpdate == null)
            {
                return $"Car with ID {carId} not found.";
            }

            carToUpdate.IsAvailable = isAvailable;
            _dbContext.SaveChanges();
            return "Car availability updated successfully.";
        }
    }
}
