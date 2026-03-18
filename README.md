# mini-task-manager
A full-stack Task Management application built to demonstrate proficiency in **C# ASP.NET Core**, **MySQL**, **JavaScript**, and **Python**.

---

## Tech Stack

| Layer | Technology | Purpose |
|-------|-----------|---------|
| Backend | C# ASP.NET Core Web API | REST API with 5 CRUD endpoints |
| ORM | Entity Framework Core + Pomelo | C# to MySQL bridge |
| Database | MySQL | Stores Users and Tasks |
| Frontend | HTML + Vanilla JavaScript | Browser UI with fetch() calls |
| Reporting | Python 3 | Standalone DB report script |
| API Docs | Swagger (Swashbuckle) | Auto-generated API testing UI |

---

## Project Structure

```
Mini-Task-Manager/
├── backend/                    # C# ASP.NET Core Web API
│   ├── Controllers/
│   │   └── TasksController.cs  # GET, POST, PUT, DELETE endpoints
│   ├── Models/
│   │   ├── Task.cs             # Maps to Tasks table
│   │   └── User.cs             # Maps to Users table
│   ├── Data/
│   │   └── AppDbContext.cs     # EF Core DB connection hub
│   ├── Services/
│   │   └── TaskService.cs      # Business logic layer
│   ├── appsettings.json        # DB connection string config
│   └── Program.cs              # App entry point
├── frontend/
│   ├── index.html              # Task list UI
│   ├── app.js                  # fetch() API calls
│   └── style.css               # Styling
├── report.py                   # Standalone Python report script
└── README.md
```

---

## Prerequisites

Make sure you have these installed before starting:

- [.NET SDK 10+](https://dotnet.microsoft.com/download)
- [MySQL](https://dev.mysql.com/downloads/) + MySQL Workbench
- [Node.js](https://nodejs.org/) (for serving frontend locally)
- [Python 3](https://www.python.org/) + pip

---

## Getting Started

### Step 1 — Database Setup

Open MySQL Workbench and run:

```sql
CREATE DATABASE taskmanager_db;
USE taskmanager_db;

CREATE TABLE Users (
    id          INT AUTO_INCREMENT PRIMARY KEY,
    name        VARCHAR(100) NOT NULL,
    email       VARCHAR(150) NOT NULL UNIQUE,
    created_at  TIMESTAMP DEFAULT NOW()
);

CREATE TABLE Tasks (
    id           INT AUTO_INCREMENT PRIMARY KEY,
    user_id      INT NOT NULL,
    title        VARCHAR(200) NOT NULL,
    description  TEXT,
    is_done      BOOLEAN DEFAULT FALSE,
    created_at   TIMESTAMP DEFAULT NOW(),
    CONSTRAINT fk_user
        FOREIGN KEY (user_id)
        REFERENCES Users(id)
        ON DELETE CASCADE
);

-- Insert a test user
INSERT INTO Users (name, email) VALUES ('Ali', 'ali@email.com');
```

### Step 2 — Backend Setup

```bash
cd backend

# Install NuGet packages
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
dotnet add package Swashbuckle.AspNetCore

# Update appsettings.json with your MySQL credentials
# "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=taskmanager_db;User=root;Password=YOUR_PASSWORD;"

# Run the API
dotnet run
```

API will start at: `http://localhost:5275`
Swagger UI at: `http://localhost:5275/swagger`

### Step 3 — Frontend Setup

Open a new terminal tab:

```bash
cd frontend
npx serve .
```

Frontend will start at: `http://localhost:3000`

Open `http://localhost:3000` in your browser.

### Step 4 — Python Report Script

```bash
# Install MySQL connector
pip install mysql-connector-python

# Update password in report.py if needed
# Run the report
python report.py
```

Expected output:
```
========================================
  TASK MANAGER REPORT
  Generated: 2026-03-18 16:41
========================================

Total tasks:     2
Completed:       0
Pending:         2

Tasks per user:
  Ali: 2 tasks

========================================
```

---

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Tasks` | Get all tasks |
| GET | `/api/Tasks/{id}` | Get task by ID |
| POST | `/api/Tasks` | Create new task |
| PUT | `/api/Tasks/{id}` | Update task |
| DELETE | `/api/Tasks/{id}` | Delete task |

### Example POST request body

```json
{
  "userId": 1,
  "title": "Buy groceries",
  "description": "Get milk and eggs",
  "isDone": false
}
```

---

## Database Schema

```
Users                          Tasks
─────────────────              ──────────────────────
id          PK INT             id          PK INT
name        VARCHAR(100)       user_id     FK → Users.id
email       VARCHAR(150)       title       VARCHAR(200)
created_at  TIMESTAMP          description TEXT (nullable)
                               is_done     BOOLEAN
                               created_at  TIMESTAMP
```

Relationship: One User → Many Tasks (ON DELETE CASCADE)

---

## Running Both Servers

You need **two terminals** running simultaneously:

```bash
# Terminal 1 — Backend
cd backend && dotnet run

# Terminal 2 — Frontend
cd frontend && npx serve .
```

---

## Notes for Developers

- `appsettings.json` contains the DB connection string — update with your local MySQL password
- HTTPS redirection is disabled for local development — re-enable for production
- CORS is set to `AllowAll` for local dev — restrict origins for production
- `bin/` and `obj/` folders are excluded via `.gitignore` — run `dotnet restore` after cloning

---

## Author

John Yang — [github.com/devyang97](https://github.com/devyang97)
