<h1 align="center">Curriculum Management System</h1>

<p align="center">
  Step-by-step guide to clone and run this ASP.NET MVC and Web API project with an SQLite database.
</p>

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet)
- [Git](https://git-scm.com/downloads)
- [Visual Studio](https://visualstudio.microsoft.com/)

## Getting Started

1. **Clone the Repository:**

   First, clone the project repository using Git:

   ```shell
   git clone https://github.com/Tbossun/Curriculum-Management-System.git

2. Navigate to the Project Directory:

Change your working directory to the project folder

3. Running the Application
Install Dependencies:

4. Use the .NET CLI to install the required packages and dependencies:
  ```shell
  dotnet restore

5. Initialize the SQLite Database:

Run the following commands to create and apply migrations for the SQLite database:
```shell
dotnet ef migrations add InitialCreate
dotnet ef database update

6. Build and Run the Project:

To build and run the application, use the following command:
dotnet run

The application will be accessible in your web browser at http://localhost:5000 or https://localhost:5001.

Contributing
If you would like to contribute to this project, please see the CONTRIBUTING.md file.

License
This project is licensed under the MIT License.

Contact
If you have any questions or need further assistance, feel free to contact the project owner:

GitHub Profile
Happy coding!

