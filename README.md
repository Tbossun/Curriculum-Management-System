upto<h1 align="center">Curriculum Management System</h1>

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

3. **Install Dependencies**

   Use the .NET CLI to install the required packages and dependencies:

   ```shell
   dotnet restore

4. **Initialize the SQLite Database:**

   Run the following commands to create and apply migrations for the SQLite database:

   ```shell
   dotnet ef migrations add InitialCreate
   dotnet ef database update

5. **Build and Run the Projec**

   To build and run the application, use the following command

   ```shell
   dotnet run

The application will be accessible in your web browser at 
http://localhost:7048 

Contact
If you have any questions or need further assistance, feel free to contact the project owner on their GitHub Profile.

Happy coding!







