using System.Net;
using CarRentalSystem.Repositories;
using SendGrid.Helpers.Mail;
using SendGrid;
using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    public class CarService
    {
        private readonly CarRepository _carRepository;
        private readonly string _sendGridApiKey = "GK.9hujkTSFSV3QpdWE21pQ.hEKJfhyerkp_sd4eTtp-SscKoMhCdwDefrfbj";

        public CarService(CarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // Method to get all cars
        public List<CarModel> GetAllCars()
        {
            return _carRepository.GetAllCars();
        }

        // Method to add a new car
        public void AddCar(CarModel car)
        {
            _carRepository.AddCar(car);
        }

        // Method to get a car by its ID
        public CarModel GetCarById(int id)
        {
            return _carRepository.GetCarById(id);
        }

        // Method to update a car's details
        public void UpdateCar(int id, CarModel updatedCar)
        {
            var car = _carRepository.GetCarById(id);
            if (car != null)
            {
                // Update car properties
                car.Makes = updatedCar.Makes;
                car.Model = updatedCar.Model;
                car.Year = updatedCar.Year;
                car.PricePerDay = updatedCar.PricePerDay;
                car.IsAvailable = updatedCar.IsAvailable;

                _carRepository.UpdateCar(car);
            }
        }

        // Method to delete a car
        public void DeleteCar(int id)
        {
            _carRepository.DeleteCar(id);
        }

        public bool IsCarAvailable(int carId)
        {
            var car = _carRepository.GetCarById(carId);
            return car != null && car.IsAvailable;
        }

        public decimal RentCar(int carId, int rentalDays)
        {
            var car = _carRepository.GetCarById(carId);

            decimal rentalCost = car.PricePerDay * rentalDays;

            _carRepository.UpdateCarAvailability(carId, false);

            var emailSubject = "Rental Confirmation - Car Rental System";
            var emailBody = $"Your car rental is confirmed for {rentalDays} day(s). Total cost: {rentalCost:C}. Thank you for choosing us!";

            Console.WriteLine("Sending rental confirmation email...");
            SendConfirmationEmailAsync("gurukoushik08@gmail.com", emailSubject, emailBody).Wait(); 

            return rentalCost;
        }

        private async Task SendConfirmationEmailAsync(string recipientEmail, string emailSubject, string emailBody)
        {
            var client = new SendGridClient(_sendGridApiKey); 
            var sender = new EmailAddress("gurukoushik08@gmail.com", "Car Rental System");
            var recipient = new EmailAddress("gurukoushik08@gmail.com");
            var emailMessage = MailHelper.CreateSingleEmail(sender, recipient, emailSubject, emailBody, emailBody);

            try
            {
                var response = await client.SendEmailAsync(emailMessage);

                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
                {
                    var errorDetails = await response.Body.ReadAsStringAsync();
                    Console.WriteLine($"Failed to send email. Error: {errorDetails}");
                }
                else
                {
                    Console.WriteLine("Confirmation email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while sending email: {ex.Message}");
            }
        }
    }
}
