# SAPCardGame
This  game is built using .NetCore 3.1 Framework.

System requirement 
 .Net Core 3.1 SDK
 .Net Core 3.1 Runtime
 
#Instructions to run/test
Download the code from Git. 

You will find 2 folders in the root which contain 2 different projects - 

 1. CardGame - Game Console Project
 2. CardGame - xUnit Test Project

Stay in root folder and  open powershell Terminal/cmd and type below to

1. Restore Packages <br/>
    ```html
      dotnet restore
    ```
2. Compilate Solution <br/>
     ```html
     dotnet build
     ```
3. Run Tests (34 unit tests have been written) <br/>
    ```html
     dotnet test .\CardGameTest\
     ```
4. Start Game <br/>
    ```html
     dotnet run .\cardGame\CardGame.csproj
    ```
