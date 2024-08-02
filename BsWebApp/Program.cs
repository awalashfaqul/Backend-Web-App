using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
using Microsoft.EntityFrameworkCore; 
using BsWebApp.Data; 
using BsWebApp.Repositories; 

/* Initializing a new instance of WebApplicationBuilder with the provided arguments */
var builder = WebApplication.CreateBuilder(args); 

/* Add services to the container.
Adding the AppDbContext service for Entity Framework Core
Configuring the DbContext to use SQL Server with a connection string from the configuration */
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 

/* Registering the UserRepo class with scoped lifetime for dependency injection */
builder.Services.AddScoped<UserRepo>(); 

/* Adding support for controllers to handle HTTP requests */
builder.Services.AddControllers(); 

/* Building the WebApplication instance from the builder */
var app = builder.Build(); 

/* Configure the HTTP request pipeline.
Checking if the application is running in the development environment */
if (app.Environment.IsDevelopment()) 
{
    /* Enabling developer exception page to display detailed error information */
    app.UseDeveloperExceptionPage(); 
}

/* Enabling routing middleware to route requests to endpoints */
app.UseRouting(); 

/* Adding authorization middleware to the request pipeline */
app.UseAuthorization(); 

/* Mapping controller endpoints to handle incoming requests */
app.MapControllers(); 

/* Running the application */
app.Run(); 
