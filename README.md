# AbInbev Frontend Project 🚀

## Overview

This project is a frontend application that provides a user interface for AbInbev related operations.

## 🛠️ Prerequisites

- Docker
- Docker Compose

## 🚀 Getting Started

### Running the Application

1. Clone the repository:
```bash
git clone https://github.com/caiohrm/AbinbevFE.git
cd AbinbevFE
```

2. Start the application using Docker:
```bash
docker-compose up --build
```

The application will be available at: [http://localhost:3000](http://localhost:3000)

## 🔐 Authentication

### Default Credentials
| Field    | Value     |
|----------|-----------|
| User     | 12345678  |
| Password | Hol@Test1 |

## 🔧 Development

### Local Development
The project is containerized using Docker for consistent development environments across the team.

### Building for Production
```bash
docker-compose -f docker-compose.prod.yml up --build
```

## 📝 Contributing

1. Create a new branch for your feature
2. Make your changes
3. Submit a pull request

---
⭐ Don't forget to star this repository if you find it useful!