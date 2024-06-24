# Project Title

QA Exercise for Marloow Navigation

## Table of Contents

- [Requirements](#requirements)
- [Features](#features)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running Tests](#Tests)

## Requirements

Ensure you have the following installed:
- [Visual Studio](https://visualstudio.microsoft.com/downloads/)
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Google Chrome](https://www.google.com/chrome/)



## Features & Packages

#### Features

- Selenium Framework using POM design pattern
- Covers basic checks that the site loaded
- Covers basic authentication and logic checks
- Covers the two cart validation scenarios (add to cart / see similr items flow)

#### Nuget Packages

- Selenium.WebDriver v4.21
- Nunit related packages v4.1.0
## Installation

Step-by-step instructions to install the project.

#### Clone Repository
```bash
git clone https://github.com/NikolasAristidou/SeleniumFrameworkSolution.git
```

#### Restore NuGet Packages
   - Open solution file on Visual Studio (Or double click on SeleniumFrameworkSolution.sln file) 
   - In Visual Studio, open the Package Manager Console (`Tools` > `NuGet Package Manager` > `Package Manager Console`).
   - Run the following command to restore the NuGet packages:
     ```sh
     dotnet restore
     ```

## Configuration

#### Update Settings.json file credentials

```Bash
cd SeleniumFrameworkSolution Repository
```
- Update settings.json file with working amazon credentials
```Json
{
  "AppSettings": {
    "BaseUrl": "https://amazon.com",
    "Username": "",
    "Password": ""
  }
}
```

- On ConfigReader.cs (SeleniumFrameWork.Core.Utilities) change the coreProjectDirectory path to match the project Directory eg. for me i changed the value to @"C:\Users\nikol\source\repos\SeleniumFrameworkSolution". (I Couldn't get this fixed in time so this is the workaround)

```c#
var coreProjectDirectory = @"C:\Users\nikol\source\repos\SeleniumFrameworkSolution";
```

## Tests

# Important

Before running any tests make sure settings.json file has proper amazon credentials AND the cart of the user is EMPTY.

#### Run the test from CMD

- cd to the path of the repository eg.

```Bash
  cd C:\Users\nikol\source\repos\SeleniumFrameworkSolution
```

- Run the following command: 

```sh
     dotnet test --logger "trx;LogFileName=TestResults.xml"
```

- The results of the automation flow will be generated inside this folder 

SeleniumFrameworkSolution\SeleniumFramework.Tests\TestResults in XML form

#### Run the test from IDE + debugging

- Open solution (sln) file on Visual Studio.
- Add a breakpoint inside the code. (Tests.cs is the file with the tests)
- Click on Test tab -> Debug all tests

