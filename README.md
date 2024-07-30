
# Forecastly

Forecastly is a robust weather forecasting application designed to provide accurate and real-time weather data. This application integrates various modules to ensure scalability, maintainability, and efficiency.

## Table of Contents

- [Installation](#installation)
- [Project Structure](#project-structure)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [Contributing](#contributing)
- [License](#license)

## Installation

To get started with Forecastly, follow these steps:

1. **Clone the repository:**

   ```bash
   git clone https://github.com/alimahboubi/Forecastly.git
   cd Forecastly
   ```

2. **Open the solution in your preferred IDE:**

   Open the `Forecastly.sln` file in Visual Studio or any compatible IDE.

3. **Restore NuGet packages:**

   Restore the necessary NuGet packages to ensure all dependencies are installed.

   ```bash
   dotnet restore
   ```

## Project Structure

The project is structured into several key components:

- **Forecastly.Api**: Contains the API layer for handling HTTP requests and responses.
- **Forecastly.Application**: Manages the application logic and use cases.
- **Forecastly.Domain**: Contains the core domain models and business logic.
- **Forecastly.Infrastructure.Cache.InMemory**: Provides in-memory caching functionalities.
- **Forecastly.Infrastructure.OpenWeatherMap**: Integrates with the OpenWeatherMap API for weather data.
- **Forecastly.Application.UnitTests**: Contains unit tests for the application logic.

### Directory Layout

```
Forecastly/
│
├── Forecastly.sln
├── Forecastly.sln.DotSettings
├── Forecastly.sln.DotSettings.user
├── .idea/
├── Src/
│   ├── Forecastly.Domain/
│   ├── Forecastly.Application/
│   ├── Forecastly.Infrastructure.OpenWeatherMap/
│   ├── Forecastly.Api/
│   └── Forecastly.Infrastructure.Cache.InMemory/
└── Forecastly.Application.UnitTests/
```

## Usage

To run the application, use the following command:

```bash
dotnet run --project Src/Forecastly.Api
```

This will start the API, which you can interact with using tools like Postman or Curl.

## Running Tests

To run the unit tests, navigate to the test project directory and use the `dotnet test` command:

```bash
cd Forecastly.Application.UnitTests
dotnet test
```

This will execute all the unit tests and provide a summary of the test results.

## Contributing

We welcome contributions to Forecastly. To contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes and commit them (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a Pull Request.

Please ensure your code follows the existing code style and includes relevant tests.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
