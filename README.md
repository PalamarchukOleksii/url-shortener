# url-shortener

This repository contains the **URL Shortener** application developed as a test task for Inforce.

## Table of Contents

- [About](#about)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Installation](#installation)

  - [Running with Docker](#running-with-docker)
  - [Running without Docker](#running-without-docker)

- [Usage](#usage)
- [Admin Access](#admin-access)
- [License](#license)

## About

The URL Shortener lets users convert long URLs into short, shareable links. It was built in alignment with all required features from the Inforce test task specification, including multiple user roles, views, and functionality such as instant updates, detailed info pages, and access control.

## Features

Fully implements all test task requirements from the official specification:

- **Authentication System**

  - Admin and regular user login
  - Role-based access control

- **Short URLs Table View**

  - Displays all shortened URLs
  - Inline URL creation (for authenticated users)
  - Soft error handling (e.g., duplicates)
  - Realtime updates without page reload
  - Deletion of:

    - Own records (regular users)
    - Any record (admin)

  - Anonymous users can only **view** the table

- **Short URL Info View**

  - Displays details: `CreatedBy`, `CreatedDate`, and more
  - Restricted to authorized users only

- **About View**

  - Publicly viewable
  - Admins can edit the description of the shortening algorithm

- **Shortening Service**

  - Custom logic for generating compact URLs
  - Ensures uniqueness and persistence

- **Unit Tests**

  - Covers key components (e.g., validation, handlers of usecases)

## Technologies Used

- **Frontend:** React + Vite
- **Backend:** ASP.NET Core (.NET 9)
- **Database:** PostgreSQL
- **Testing:** xUnit, Moq
- **Other:** MediatR, Entity Framework Core, FluentValidation

## Getting Started

1. Clone the repository and navigate into it:

   ```bash
   git clone https://github.com/PalamarchukOleksii/url-shortener.git
   cd url-shortener
   ```

## Installation

### Running with Docker

```bash
docker-compose up --build
```

- Frontend: [http://localhost:3000](http://localhost:3000)
- Backend API: [http://localhost:8080](http://localhost:8080)

---

### Running without Docker

#### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) + [npm](https://www.npmjs.com/)
- PostgreSQL running locally

#### Backend (ASP.NET Core)

```bash
cd url-shortener-server/src/UrlShortener.Api
```

Update `appsettings.json` with your DB connection:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=url_shortener_db;Username=your_username;Password=your_password"
}
```

Run the backend:

```bash
dotnet run
```

#### Frontend (React + Vite)

```bash
cd ../../../url-shortener-client
npm install
npm run dev
```

Visit: [http://localhost:3000](http://localhost:3000)

---

## Usage

1. Log in with admin or user credentials.
2. Shorten URLs from the main table view.
3. View info or delete entries depending on your permissions.
4. View and (if admin) edit the algorithm description in the About section.

## Admin Access

An admin user is automatically seeded:

```
Username: admin
Password: Passw0rd.
```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
