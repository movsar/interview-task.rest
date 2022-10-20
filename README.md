# A simple To Do List

### How to run: 
1. Clone the repository
2. Select docker-compose as a startup project
3. Run Docker Compose configuration

### Notes 
* Used Layered architecture separating Data access from Api service
* As a DBMS used SQL Server 2022
* Both Database and API run with Docker Compose in separate containers
* Tests are written for the controller with xUnit. Because of the simplicity of the app and framework taking care of incoming model formats, there is little to test to make them really valuable, although I thought about doing end-to-end testing, yet decided it might be an overkill.
