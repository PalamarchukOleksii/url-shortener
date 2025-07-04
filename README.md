# url-shortener

This repository contains the URL Shortener application developed as a test task for Inforce.

## Table of Contents

* [About](#about)
* [Features](#features)
* [Technologies Used](#technologies-used)
* [Getting Started](#getting-started)
* [Installation](#installation)

  * [Running with Docker](#running-with-docker)
  * [Running without Docker](#running-without-docker)
* [Usage](#usage)
* [License](#license)

## About

The URL Shortener application lets users convert long URLs into shorter, more manageable links. It was developed as a test task for Inforce.

## Features

* Shorten long URLs into compact links.
* Redirect users from short links to the original URLs.
* User-friendly interface.
* Responsive and performant web application.

## Technologies Used

* **Frontend:** React
* **Backend:** ASP.NET Core (.NET 9)
* **Database:** PostgreSQL

## Getting Started

1. Clone the repository and navigate into it:

   ```bash
   git clone https://github.com/PalamarchukOleksii/url-shortener.git
   cd url-shortener
   ```

## Installation

### Running with Docker

1. Build and start the application:

   ```bash
   docker-compose up --build
   ```

2. Open `http://localhost:3000` in your browser.

---

### Running without Docker

#### Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* [Node.js](https://nodejs.org/) + [npm](https://www.npmjs.com/)
* PostgreSQL running and accessible

#### Backend (ASP.NET Core)

1. Navigate to the API project directory:

   ```bash
   cd url-shortener-server/src/UrlShortener.Api
   ```

2. Open `appsettings.json` and configure the PostgreSQL connection string:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=url_shortener_db;Username=your_username;Password=your_password"
   }
   ```

3. Start the server:

   ```bash
   dotnet run
   ```

   The backend will be available at `http://localhost:5180`.

#### Frontend (React + Vite)

1. Navigate to the frontend app:

   ```bash
   cd ../../../url-shortener-client
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Start the development server:

   ```bash
   npm run dev
   ```

4. Open `http://localhost:3000` in your browser.

---

## Usage

1. Visit `http://localhost:3000`.
2. Enter a long URL and click "Shorten URL".
3. Use the generated short link to access the original URL.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
