# Simple Web-server-based Calculator

This project implements a simple web-based calculator using C#. It provides a user-friendly interface for basic arithmetic operations accessible through a web browser.

## Design Goals

- **Accessibility:** The program aims to be readily available through a web browser, eliminating the need for local installation.
- **Usability:** Inspired by traditional calculators, the interface focuses on simplicity and ease of use.
- **Functionality:** The core functionality centers on performing basic arithmetic operations (addition, subtraction, multiplication, and division) with user-provided inputs.

## Features

- **Client-Server Interaction:** Leverages HTTP requests and responses for communication between the web browser (client) and the C# server program.
- **Interactive Interface:** Presents a basic HTML form with buttons for numbers, operators, and a result display, mimicking a physical calculator.
- **Dynamic Results:** Processes user input in real-time, calculating and displaying the result within the web browser.

## Getting Started

1. **Prerequisites:** Ensure you have .NET installed on your system. You can download it from [here](https://dotnet.microsoft.com/en-us/download).
2. **Clone the Repository:** Use `git clone "LINK FROM THE CLONE BUTTON"` to clone the project code.
3. **Run the Server:** Navigate to the project directory (`cd csharp-webserver`) and execute `dotnet run` to start the server.
4. **Access the Calculator:** Open a web browser and navigate to `http://localhost:13000` (modify the port if changed in code).

## Usage
The web-based calculator provides a simple interface with buttons for numbers and operators.

- Enter numbers and operators sequentially.
- Click the "=" button to submit the expression and receive the calculated result.
- The result appears in the designated display area.

### Example:

- Click buttons "5", "+", "3", and "=".
- The result "8" is displayed in the result area.

**Note:** Currently, the program supports basic mathematic operations. Future enhancements might include functionalities like power calculations or square root extraction. **Section will Be Updated**

### Todays UseCase
- **Updating Everyday**
- Calculate with + - * /
- Add all numbers from 0-9
- **New** Refactor code (remove unnecessary code)
- **New** Add Power math/Square Root
