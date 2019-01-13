# .NET Core Web API Entity Framework Sample

[![travis build result](https://travis-ci.com/Iukekini/.NET-Core-Web-API-Entity-Framework-Sample.svg?branch=master)](https://travis-ci.com/Iukekini/.NET-Core-Web-API-Entity-Framework-Sample.svg?branch=master)

A example project of a .NET core web api. This API is setup to test the controller using Dependency injection. Unity Containers were used to handle this. 

The UI is handle by Swashbuckler / swagger. I like this interface as it is an easy way to show other devs what's going on. 

### Project Highlights


* Entity Framework Core for all the db goodness
* Dependency Injection (Unity Containers) 
* Swagger UI / Swashbucker for ease of use and to help documentation. 
* Unit or Work / Repository Pattern to help test without having to mock the entity framework side. 


## How to Run the Solution

1. Install .Net Core. Instructions can be found [Here](https://dotnet.microsoft.com/download)

2. Download source to any folder on the machine. 

3. Run `make -B` command. This will get all the packages, run the tests and build all the projects.  

   If you want to run all of this manually. Here are the commands that the `make -B` will run.

    Restore any Packages not currently installed

        dotnet restore

    Units Tests

        dotnet test -v=m ./Web_API_Entity_Framework_Sample_Test/Web_API_Entity_Framework_Sample_Test.csproj 

    Build the solution

        dotnet build


4. Run the Project. 

    You can run the project with a `make run` command or with 
    
        dotnet run --project ./Web_API_Entity_Framework_Sample/Web_API_Entity_Framework_Sample.csproj 


    This will spin up the web server listening @ [http://localhost:5000](http://localhost:5000)

### Debug in visual Studio. 

If you have Visual Studio and .Net core Installed you can open the solution with the solution file (.sln) in the root folder and run the solution with F5.  