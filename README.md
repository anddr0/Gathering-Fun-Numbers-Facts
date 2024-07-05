# Funny Facts About Numbers

## Project Overview

**TestExerciseControlant** is a .NET application designed to provide interesting facts about numbers. 
The project includes the main application and a comprehensive testing suite to ensure the functionality and reliability of the application.

## Project Structure

The project consists of the following directories and files:

- **TestExerciseControlant.sln**: The solution file that organizes the projects within Visual Studio.
- **TestExerciseControlant**: The main application directory.
  - **TestExerciseControlant.csproj**: The project file for the main application.
  - **ProgramManager.cs**: Manages the overall application logic and user interactions.
  - **UserInterface.cs**: Handles the display and navigation of the menu.
  - **NumbersApiServise.cs**: Interacts with the Numbers API to fetch facts.
  - **FactStore.cs**: Stores and manages fetched facts.
  - **Option.cs**: Represents menu options in the user interface.
  - **INumbersApiService.cs**: Interface for the Numbers API service.
  - **bin**: Directory for compiled binaries.
  - **obj**: Directory for object files and other temporary files.
- **TestExerciseControlant.Tests**: The testing suite directory.
  - **TestExerciseControlant.Tests.csproj**: The project file for the testing suite.
  - **ProgramTests.cs**: Contains unit tests for the application.
  - **bin**: Directory for compiled test binaries.
  - **obj**: Directory for test object files and other temporary files.

## How It Works

The main application is developed across several files within the `TestExerciseControlant` directory. The core logic and functionality are spread across classes like `ProgramManager`, `UserInterface`, `NumbersApiServise`, and `FactStore`.

The testing suite in the `TestExerciseControlant.Tests` directory includes unit tests written using xUnit. These tests validate the functionality of the application, ensuring that any changes or additions do not introduce bugs or regressions.

## How to Run the Project

### Prerequisites

Ensure that you have the following installed on your system:

- .NET SDK
- A code editor or IDE such as Visual Studio or Visual Studio Code

### Running the Application

1. Open the solution file (`TestExerciseControlant.sln`) in your preferred code editor or IDE.
2. Build the solution to restore the dependencies and compile the application:
   ```sh
   dotnet build
   ```
3. Navigate to the `TestExerciseControlant` directory and run the application:
   ```sh
   cd TestExerciseControlant
   dotnet run
   ```

### Running the Tests

1. Open the solution file (`TestExerciseControlant.sln`) in your preferred code editor or IDE.
2. Build the solution to restore the dependencies and compile the tests:
   ```sh
   dotnet build
   ```
3. Navigate to the `TestExerciseControlant.Tests` directory and run the tests:
   ```sh
   cd TestExerciseControlant.Tests
   dotnet test
   ```

### Additional Information

- Ensure that you have the necessary permissions to run the commands in your terminal.
- If you encounter any issues with missing dependencies or build errors, try restoring the dependencies using:
  ```sh
  dotnet restore
  ```
