using Microsoft.AspNetCore.Mvc; // Importing namespace for ASP.NET Core MVC functionalities
using BsWebApp.Models; // Importing the application's models namespace
using BsWebApp.Repositories; // Importing the application's repositories namespace
using System.Threading.Tasks; // Importing namespace for asynchronous programming

namespace BsWebApp.Controllers // Defining the namespace for the controller classes
{
    [ApiController] // Attribute indicating that this class is an API controller
    [Route("api/users/{userId}/addresses")] // Defining the route template for this controller
    public class UserAddressController : ControllerBase // Defining a controller class that inherits from ControllerBase
    {
        private readonly UserRepo _userRepo; // Declaring a private readonly field to hold the user repository

        public UserAddressController(UserRepo userRepo) // Constructor to initialize the controller with the given user repository
        {
            _userRepo = userRepo; // Assigning the provided repository to the private field
        }

        [HttpGet] // Attribute indicating that this method handles HTTP GET requests
        public async Task<IActionResult> GetAddresses(int userId) // Method to retrieve addresses for a specific user
        {
            var addresses = await _userRepo.GetAddressesAsync(userId); // Calling the repository method to get addresses asynchronously
            return Ok(addresses); // Returning the addresses with an HTTP 200 OK response
        }

        [HttpPost] // Attribute indicating that this method handles HTTP POST requests
        public async Task<IActionResult> CreateAddress(int userId, [FromBody] Address address) // Method to create a new address for a specific user
        {
            await _userRepo.CreateAddressAsync(userId, address); // Calling the repository method to create the address asynchronously
            return CreatedAtAction(nameof(GetAddresses), new { userId }, address); // Returning the created address with an HTTP 201 Created response
        }

        [HttpPut("{addressId}")] // Attribute indicating that this method handles HTTP PUT requests
        public async Task<IActionResult> UpdateAddress(int userId, int addressId, [FromBody] Address address) // Method to update an existing address
        {
            address.Id = addressId; // Setting the ID of the address to the provided addressId
            address.UserId = userId; // Setting the UserId of the address to the provided userId
            await _userRepo.UpdateAddressAsync(address); // Calling the repository method to update the address asynchronously
            return NoContent(); // Returning an HTTP 204 No Content response indicating successful update
        }

        [HttpDelete("{addressId}")] // Attribute indicating that this method handles HTTP DELETE requests
        public async Task<IActionResult> DeleteAddress(int userId, int addressId) // Method to delete an address by its ID
        {
            await _userRepo.DeleteAddressAsync(addressId); // Calling the repository method to delete the address asynchronously
            return NoContent(); // Returning an HTTP 204 No Content response indicating successful deletion
        }
    }
}
