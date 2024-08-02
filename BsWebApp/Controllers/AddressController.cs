using Microsoft.AspNetCore.Mvc; // Importing namespace for ASP.NET Core MVC functionalities
using BsWebApp.Models; 
using BsWebApp.Repositories; 
using System.Threading.Tasks; // Importing namespace for asynchronous programming

namespace BsWebApp.Controllers // Defining the namespace for the controller classes
{
    [ApiController] 
    [Route("api/users/{userId}/addresses")] // Defining the route template for this controller
    public class UserAddressController : ControllerBase 
    {
        /* Declaring a private readonly field to hold the user repository */
        private readonly UserRepo _userRepo; 

        /* Constructor to initialize the controller with the given user repository */
        public UserAddressController(UserRepo userRepo) 
        {
            _userRepo = userRepo; // Assigning the provided repository to the private field
        }

        [HttpGet] 
        public async Task<IActionResult> GetAddresses(int userId) 
        {
            var addresses = await _userRepo.GetAddressesAsync(userId); // Calling the repository method to get addresses asynchronously
            return Ok(addresses); // Returning the addresses with an HTTP 200 OK response
        }

        [HttpPost] 
        public async Task<IActionResult> CreateAddress(int userId, [FromBody] Address address) 
        {
            await _userRepo.CreateAddressAsync(userId, address); // Calling the repository method to create the address asynchronously
            return CreatedAtAction(nameof(GetAddresses), new { userId }, address); // Returning the created address with an HTTP 201 Created response
        }

        [HttpPut("{addressId}")] 
        public async Task<IActionResult> UpdateAddress(int userId, int addressId, [FromBody] Address address) 
        {
            address.Id = addressId; // Setting the ID of the address to the provided addressId
            address.UserId = userId; // Setting the UserId of the address to the provided userId
            await _userRepo.UpdateAddressAsync(address); // Calling the repository method to update the address asynchronously
            return NoContent(); // Returning an HTTP 204 No Content response indicating successful update
        }

        [HttpDelete("{addressId}")] 
        public async Task<IActionResult> DeleteAddress(int userId, int addressId) 
        {
            await _userRepo.DeleteAddressAsync(addressId); // Calling the repository method to delete the address asynchronously
            return NoContent(); // Returning an HTTP 204 No Content response indicating successful deletion
        }
    }
}
