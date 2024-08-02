using Microsoft.EntityFrameworkCore; 
using BsWebApp.Data; 
using BsWebApp.Models; 
using BsWebApp.Repositories; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using Xunit; // Importing xUnit testing framework

namespace BsWebApp.Tests 
{
    public class UserRepoTests 
    {
        /* Method to create new DbContextOptions for in-memory database */
        private DbContextOptions<AppDbContext> NewContextOptions(string dbName) 
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName) // Configuring to use in-memory database with given name
                .Options; // Returning the configured options
        }

        [Fact] /* Attribute indicating that this is a test method */
        public async Task GetAllAddresses() 
        {
            var options = NewContextOptions("TestDatabase_GetAllAddresses"); // Creating new DbContextOptions for the test database

            /* Creating a new instance of AppDbContext with the options */
            using (var context = new AppDbContext(options)) 
            {
                /* Creating a new user instance */
                var user = new User { Name = "Christofer Fransson" }; 
                user.Addresses = new List<Address> // Adding addresses to the user
                {
                    new Address { AddressStreet = "Blekingegatan", AddressStreetExtra = "12", AddressCity = "Huddinge", AddressZip = "114 32", AddressCountry = "Sweden", AddressState = "Stockholm", Label = "Home" },
                    new Address { AddressStreet = "Östra Esplanaden", AddressStreetExtra = "23", AddressCity = "Skaffelå", AddressZip = "951 82", AddressCountry = "Sweden", AddressState = "Luleå", Label = "Aurora Site"},
                    new Address { AddressStreet = "Södra Hamnplan", AddressStreetExtra = "7", AddressCity ="Knivsta", AddressZip = "351 62", AddressCountry = "Sweden", AddressState = "Uppsala Län", Label = "Work"}
                };
                context.Users.Add(user); // Adding the user to the context
                await context.SaveChangesAsync(); // Saving changes to the in-memory database
            }

            using (var context = new AppDbContext(options)) // Creating another instance of AppDbContext to test retrieval
            {
                var userRepository = new UserRepo(context); // Creating a new instance of UserRepo
                var addresses = await userRepository.GetAddressesAsync(1); // Retrieving addresses for the user with ID 1

                Assert.Equal(3, addresses.Count); // Asserting that the number of addresses retrieved is 3
            }
        }

        [Fact] 
        public async Task AddNewAddress() 
        {
            var options = NewContextOptions("TestDatabase_CreateAddress"); 

            using (var context = new AppDbContext(options)) 
            {
                var user = new User { Name = "Christofer Fransson", Addresses = new List<Address>() }; // Creating a new user instance with no addresses
                context.Users.Add(user); 
                await context.SaveChangesAsync(); 
            }

            using (var context = new AppDbContext(options)) // Creating an instance of AppDbContext to test address creation
            {
                var userRepository = new UserRepo(context); 
                var address = new Address { AddressStreet = "Lilla Torg", AddressStreetExtra = "5", AddressCity = "Visby", AddressZip = "203 51", AddressCountry = "Sweden", AddressState = "Gotland", Label = "Vacation" }; // Creating a new address instance

                // Retrieve the user before adding the address
                var user = await context.Users.Include(u => u.Addresses).FirstAsync(u => u.Id == 1); // Retrieving the user with addresses included
                await userRepository.CreateAddressAsync(user.Id, address); // Adding the new address to the user

                var addresses = await userRepository.GetAddressesAsync(1); // Retrieving addresses for the user with ID 1
                Assert.Single(addresses); // Asserting that there is only one address
                Assert.Equal("Vacation", addresses.First().Label); // Asserting that the address label is "Vacation"
            }
        }

        [Fact] 
        public async Task UpdateAddress() 
        {
            var options = NewContextOptions("TestDatabase_UpdateAddress"); 

            using (var context = new AppDbContext(options)) // Creating a new instance of AppDbContext with the options
            {
                var user = new User { Name = "Christofer Fransson", Addresses = new List<Address>() }; // Creating a new user instance with no addresses
                var address = new Address { AddressStreet = "Blekingegatan", AddressStreetExtra = "12", AddressCity = "Huddinge", AddressZip = "114 32", AddressCountry = "Sweden", AddressState = "Stockholm", Label = "Home" }; // Creating a new address instance
                user.Addresses.Add(address); // Adding the address to the user's address list
                context.Users.Add(user); // Adding the user to the context
                await context.SaveChangesAsync(); // Saving changes to the in-memory database
            }

            using (var context = new AppDbContext(options)) 
            {
                var userRepository = new UserRepo(context); // Creating a new instance of UserRepo
                var address = await context.Addresses.FirstAsync(); // Retrieving the first address
                address.AddressCity = "Flemingsberg"; // Updating the address city
                address.AddressZip = "141 32"; // Updating the address zip code
                await userRepository.UpdateAddressAsync(address); // Saving the updated address

                var updatedAddress = await context.Addresses.FirstAsync(); // Retrieving the updated address
                Assert.Equal("Flemingsberg", updatedAddress.AddressCity); // Asserting that the city has been updated
                Assert.Equal("141 32", updatedAddress.AddressZip); // Asserting that the zip code has been updated
            }
        }

        [Fact] 
        public async Task DeleteAddress() 
        {
            var options = NewContextOptions("TestDatabase_DeleteAddress"); 

            using (var context = new AppDbContext(options)) // Creating a new instance of AppDbContext with the options
            {
                var user = new User { Name = "Christofer Fransson", Addresses = new List<Address>() }; // Creating a new user instance with no addresses
                var address = new Address { AddressStreet = "Södra Hamnplan", AddressStreetExtra = "7", AddressCity ="Knivsta", AddressZip = "351 62", AddressCountry = "Sweden", AddressState = "Uppsala Län", Label = "Work"}; // Creating a new address instance
                user.Addresses.Add(address); // Adding the address to the user's address list
                context.Users.Add(user); // Adding the user to the context
                await context.SaveChangesAsync(); // Saving changes to the in-memory database
            }

            using (var context = new AppDbContext(options)) // Creating another instance of AppDbContext to test address deletion
            {
                var userRepository = new UserRepo(context); // Creating a new instance of UserRepo
                var address = await context.Addresses.FirstAsync(); // Retrieving the first address
                await userRepository.DeleteAddressAsync(address.Id); // Deleting the address by its ID

                var addresses = await userRepository.GetAddressesAsync(1); // Retrieving addresses for the user with ID 1
                Assert.DoesNotContain(addresses, a => a.Id == address.Id); // Asserting that the address is no longer in the list
            }
        }
    }
}
