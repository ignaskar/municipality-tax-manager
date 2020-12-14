# municipality-tax-manager

1. clone ```master``` branch
2. open the solution properties and change to start multiple projects (API first, then Web Client - **both should be set to start using Kestrel!**)
3. I was using docker for my SQLDB since I was on a mac,so you need to change your connection string in ```appsettings.json```.
db will be migrated and seeded automatically on first startup.
4. launch the project
5. test using provided postman collection or navigate to ```https://localhost:5003``` to open Blazor WASM app.
you can test import functionality using ```upload_test.json``` file in web ui.
