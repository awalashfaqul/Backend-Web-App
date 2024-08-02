using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore;
using BsWebApp.Data; 
using BsWebApp.Models;

namespace BsWebApp.Repositories
{
    public class UserRepo 
    {
        private readonly AppDbContext _context; // Declaring a private readonly field to hold the database context

        public UserRepo(AppDbContext context) // Constructor to initialize the repository with the given database context
        {
            _context = context; // Assigning the provided context to the private field
        }

        public async Task<List<Address>> GetAddressesAsync(int userId)
        {
            /* Querying the Addresses table to get addresses where the UserId matches the provided userId*/
            return await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task CreateAddressAsync(int userId, Address address)
        {
            address.UserId = userId; // Setting the UserId of the address to the provided userId
            _context.Addresses.Add(address); // Adding the new address to the Addresses table
            await _context.SaveChangesAsync(); // Saving changes to the database asynchronously
        }

        public async Task UpdateAddressAsync(Address address) 
        {
            _context.Addresses.Update(address); // Updating the address in the Addresses table
            await _context.SaveChangesAsync(); // Saving changes to the database asynchronously
        }

        public async Task DeleteAddressAsync(int addressId) // Method to delete an address by its ID
        {
            var address = await _context.Addresses.FindAsync(addressId); // Finding the address with the given ID
            if (address != null) // Checking if the address exists
            {
                _context.Addresses.Remove(address); // Removing the address from the Addresses table
                await _context.SaveChangesAsync(); // Saving changes to the database asynchronously
            }
        }
    }
}
