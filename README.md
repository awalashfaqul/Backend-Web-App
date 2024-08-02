# backend-web-app

# BsWebApp

## Overview

**BsWebApp** is a sample ASP.NET Core 6.0 application demonstrating a user and address management system. The application includes features for creating, reading, updating, and deleting addresses associated with users, and it uses Entity Framework Core for data access.

This project follows the principles of Test-Driven Development (TDD) and includes an in-memory database for testing purposes. It also showcases basic CRUD operations through a RESTful API.

## Features

- **User Management**: Manage user information.
- **Address Management**: Each user can have multiple addresses.
- **CRUD Operations**: Create, Read, Update, and Delete addresses associated with users.
- **Testing**: Includes unit tests for repository methods using xUnit.

## Project Structure

```
BsWebApp/
├── Controllers/
│   └── AddressController.cs
├── Data/
│   └── AppDbContext.cs
├── Models/
│   ├── User.cs
│   └── Address.cs
├── Repositories/
│   └── UserRepo.cs
├── Tests/
│   └── UserRepoTests.cs
├── Program.cs
├── BsWebApp.csproj
└── appsettings.json
```

## Setup and Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/awalashfaqul/BsWebApp.git
   cd BsWebApp
   ```

2. **Restore Dependencies**

   Restore the necessary packages using the following command:

   ```bash
   dotnet restore
   ```

3. **Run Migrations**

   Ensure the database is created and up-to-date:

   ```bash
   dotnet ef database update
   ```

4. **Run the Application**

   Start the application with:

   ```bash
   dotnet run
   ```

   The application will be available at `https://localhost:5001` (or the URL specified in your launch settings).

## API Endpoints

- **Get Addresses for User**
  - `GET /api/users/{userId}/addresses`

- **Create Address for User**
  - `POST /api/users/{userId}/addresses`
  - **Request Body**: Address object

- **Update Address**
  - `PUT /api/users/{userId}/addresses/{addressId}`
  - **Request Body**: Address object

- **Delete Address**
  - `DELETE /api/users/{userId}/addresses/{addressId}`

## Running Tests

To run the unit tests:

```bash
dotnet test
```

The tests will execute using the in-memory database and verify the CRUD operations on addresses.

## Configuration

The database connection string and other settings are configured in `appsettings.json`. The default configuration uses an in-memory database for development and testing. Update `appsettings.json` for different database providers in production.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes. Make sure to include tests for new features or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or feedback, please contact:

- **Name**: [Ashfaqul Awal]
- **Email**: [ashfaq.awal@gmail.com]

---
