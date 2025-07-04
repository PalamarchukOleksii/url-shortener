# url-shortener

This repository contains the URL Shortener application developed as a test task for Inforce.

## Table of Contents

* [About](#about)
* [Features](#features)
* [Technologies Used](#technologies-used)
* [Installation](#installation)

  * [Running with Docker](#running-with-docker)
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
* **Backend:** ASP.NET Core (C#)
* **Database:** PostgreSQL

## Installation

### Running with Docker

1. Clone the repository and navigate into it:

   ```bash
   git clone https://github.com/PalamarchukOleksii/url-shortener.git
   cd url-shortener
   ```

2. Build and start the application:

   ```bash
   docker-compose up --build
   ```

3. Open `http://localhost:3000` in your browser.

## Usage

1. Visit `http://localhost:3000`.
2. Enter a long URL and click "Shorten URL".
3. Use the generated short link to access the original URL.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
