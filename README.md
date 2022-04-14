# Oscar's playground
My playground for trying out new stuff with .NET 6 and Blazor

## Features
### Mortgager
A tool for calculating your mortgage and the monthly cost for your apartment or house.

### Live displayer
A live display for current standings and upcoming, ongoing and past fixtures in Barclays Premier League.

### Travel Planner
Displays when the next bus leaves from one bus stop to another.

### Bus Line Viewer
View any bus line and its stops on a map.

## Technologies
- .NET 6
- Server-Side Blazor
- Heroku
- GitHub
- LocalStorage
- Google Firestore

## 3rd party packages
- AutoMapper
- HtmlAgilityPack
- MudBlazor
- Google.Cloud.Firestore
- Bing Maps

## Instructions
- Clone repo
- Place Google Firestore credentials in json-file and add absolute path to `appsettings.json`
    * When running in container, supply environment variable with contents of firestore credentials json (see `src/Features/Shared/GcpCredentialsConfigurator.cs`)
- `dotnet run`
- Enjoy