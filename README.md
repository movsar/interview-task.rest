# To Do List API

A fully documented **REST API for task management** built with **.NET 6**, following a layered architecture (**Data → API**).  
The project supports Docker-based deployment and includes unit tests for the controller.

---

## ✨ API Features

The `TaskController` provides the following endpoints:

| Method   | Route                          | Description                        |
| -------- | ------------------------------ | ---------------------------------- |
| `POST`   | `/Task/Create`                 | Create a new task                  |
| `GET`    | `/Task/Read?id=...`            | Get a task by ID                   |
| `PUT`    | `/Task/Update?id=...`          | Update a task                      |
| `DELETE` | `/Task/Delete?id=...`          | Delete a task                      |
| `PATCH`  | `/Task/Mark/Complete?id=...`   | Mark a task as completed           |
| `PATCH`  | `/Task/Mark/Incomplete?id=...` | Mark a task as incomplete          |
| `GET`    | `/Task/List`                   | Get all tasks                      |
| `GET`    | `/Task/List/overdue`           | Get overdue tasks                  |
| `GET`    | `/Task/List/pending`           | Get pending tasks                  |

📖 **Swagger documentation** is automatically generated when running in development mode.

---

## 📦 Project Architecture

```

To Do List.sln
├── API/               # REST API (.NET 6 Web API)
│   ├── Controllers/   # TaskController (CRUD + filtering)
│   ├── Program.cs     # Service configuration, Swagger
│   └── Dockerfile
├── Data/              # Data layer (EF Core, Repository pattern)
├── ApiTests/          # Controller tests (xUnit + Moq)
├── docker-compose.yml # API + SQL Server 2022 orchestration
└── README.md

````

---

## 🛠️ Technologies Used

- **Language:** C# (.NET 6)  
- **Backend:** ASP.NET Core Web API  
- **Data Layer:** Entity Framework Core, Repository pattern  
- **Database:** SQL Server 2022 (in Docker container)  
- **Documentation:** Swagger / OpenAPI  
- **Testing:** xUnit + Moq  
- **Containerization:** Docker, Docker Compose  

---

## 🚀 How to Run

### Option 1: Docker Compose (recommended)

```bash
# Clone the repository
git clone <repo-url>
cd tm-todo-task

# Select docker-compose as the startup project
# Build and start services
docker compose up --build
````

### Option 2: Run Locally

1. Install SQL Server 2022
2. Configure the connection string in `appsettings.json`
3. Start the API:

   ```bash
   dotnet run --project API/API.csproj
   ```

---

## 🧪 Testing

* The **ApiTests** project contains unit tests for `TaskController`
* Built with **xUnit** and **Moq** to mock the repository
* Covers all CRUD operations and status changes (complete/incomplete)

---

## 📝 Notes

* Implements a **layered architecture** separating data access and API service
* Uses **SQL Server 2022** as the database
* Database and API run in **separate Docker containers**
* Tests are focused on the controller; full end-to-end tests are unnecessary thanks to ASP.NET Core’s built-in model validation

---

## 👤 Author

Movsar Bekaev ([movsar.dev@gmail.com](mailto:movsar.dev@gmail.com))
