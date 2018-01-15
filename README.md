I assume Vidual Studio Code and .NET Core 2.0 have been installed already. I f not then these will need to be installed first.
Please follow the following steps to set up the developer environment.

1. Download the archive file and extract it's contents into a folder. Say C:\GeoLayouts;
2. Use Visual Studio 2017 to open the GeoLayout.sln
3. Rebuild the solution.
4. Using PowerShell or command prompt navigate to c:\GeoLayouts\GeoLayout.WebAPI;
5. Type in dotnet restore ENTER
6. Type in dotnet build ENTER
7. Type in dotnet run.

8. In Visual Studio 2017 start the debug session (F5)
9. You should see a simple SPA application. Click on triangles on the left and right hand sides.
10. Observe that interaction with backend occurs using RESTful calls.
11. Optionally, you may want to run unit tests that are included with the solution.
