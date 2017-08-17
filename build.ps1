Push-Location $PSScriptRoot

& dotnet restore --source https://dotnet.myget.org/F/dotnet-core/api/v3/index.json --source https://api.nuget.org/v3/index.json
& dotnet build

Pop-Location