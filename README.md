# Miacademy_Playwright-Csharp

## Description

This project is an automated testing suite built using **Playwright** with **C#**. It is designed to validate the application at [MiaAcademy](https://miacademy.co/#/) by simulating user interactions and ensuring that critical assertions are met throughout the process. The tests are containerized using **Docker** to provide a consistent environment for execution.

## Features

- Automates navigation and form filling in the MiaPrep Online High School application.
- Asserts that no input fields (including mandatory ones) are empty and all necessary elements are visible during the test flow.

## Test Cases
- *Objective*: Applying to Miacademy online highshool.
1. navigate to https://miacademy.co/#/
2. navigate to MiaPrep Online High School through the link on banner
3. apply to MOHS
4. fill in the Parent Information
5. proceed to Student Information page
- *Assertions*:
   - Verify that the content of the new page is visible accordingly.
   - Verify that the elements are found accordingly.
   - Verify that the mandatory fields are not empty.

## Project Structure
- `Miacademy_playwright/`: Contains all playwright tests (including e2e), fixtures, and configuration.
- `Miacademy_playwright/pages/`: Contains Page Object Models used in tests.
- `dockerfile`: Automates the creation of a Docker image for the application
- `gitignore`: Specifies files and directories that Git should ignore

## Setup
To set up the `miacademy_playwright` project on your local machine, follow these steps:

### Prerequisites
- **.NET SDK**: [Download .NET SDK](https://dotnet.microsoft.com/download)
- **Docker**: [Install Docker](https://docs.docker.com/get-docker/)
- **Visual Studio Code** (optional but recommended): [Download VS Code](https://code.visualstudio.com/)

### Cloning the Repository

1. Open a terminal or command prompt.
2. Clone the repository to your local machine:
   git clone <repository-url>
   cd miacademy_playwright
3. Build the Docker image using the provided Dockerfile:
   docker build -t miacademy_playwright .
4. Run the Docker container to execute the tests:
   docker run miacademy_playwright

If you prefer to run the tests locally without Docker, follow these steps:

1. dotnet restore
2. dotnet build
3. dotnet test

   
