dotnet tool install --global dotnet-warp
dotnet publish -c Release -p:WarpOnBuild=true
pause