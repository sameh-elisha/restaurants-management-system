**.NET Clean Architecture solution**

* **Restaurants.API** → REST API endpoints
* **Restaurants.Application** → business logic & use cases
* **Restaurants.Domain** → core entities & interfaces
* **Restaurants.Infrastructure** → database & external services

---

# Restaurants Management System

A Clean Architecture backend system for managing restaurant operations, built with **.NET**.

---

## Features

* Modular design (Domain, Application, Infrastructure, API).
* Entity-based architecture (Menus, Orders, etc.).
* RESTful API endpoints.
* Extensible and testable codebase.

---

## Project Structure

* **Restaurants.API** → HTTP endpoints / controllers.
* **Restaurants.Application** → business logic, services, DTOs.
* **Restaurants.Domain** → entities, interfaces, core rules.
* **Restaurants.Infrastructure** → persistence, EF Core, external integrations.

---

## Technologies

* **.NET (C#)**
* **Entity Framework Core** (assumed)
* **Clean Architecture principles**

---

## Getting Started

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download) (7.0 or higher recommended)
* SQL Server / PostgreSQL (configure connection string)

### Installation

```bash
git clone https://github.com/ahmed18837/RestaurantsManagementSystem.git
cd RestaurantsManagementSystem
dotnet restore
```

### Database Setup

```bash
dotnet ef database update --project Restaurants.Infrastructure
```

### Run the API

```bash
dotnet run --project Restaurants.API
```

API will run at:
`https://localhost:5001` or `http://localhost:5000`

---

## Usage

Example endpoints (to be updated with real routes):

| Endpoint           | Method | Description      |
| ------------------ | ------ | ---------------- |
| `/api/menus`       | GET    | Fetch menu items |
| `/api/orders`      | POST   | Create an order  |
| `/api/tables/{id}` | PUT    | Update table     |

---

## Contributing

1. Fork the repo
2. Create a feature branch
3. Submit a PR

