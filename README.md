# EnterpriseStarterKit

An advanced microservice-based backend architecture built with **.NET 9+**, **Minimal APIs**, and **DDD** principles — designed for real-world, large-scale enterprise systems like automotive, e-commerce, retail, and logistics platforms.

## 🧱 Architecture Overview

- **Microservices**: Independent and scalable services
- **SSO + Identity**: Centralized secure authentication & authorization
- **API Gateway**: Unified entry point for routing, auth, throttling
- **CI/CD**: Full GitHub Actions integration
- **Monitoring & Logging**: Serilog, ElasticSearch, OpenTelemetry
- **Infrastructure as Code**: Dockerized development & deployment

## 🧪 Tech Stack

| Area                  | Tools/Tech                                               |
|-----------------------|----------------------------------------------------------|
| Language              | C# (.NET 9 / 10 Preview)                                 |
| Authentication        | JWT, IdentityServer, SSO                                |
| Architecture Style    | DDD, Clean/Onion/Vertical (per domain needs)            |
| APIs                  | Minimal API                                             |
| Communication         | gRPC / HTTP REST                                        |
| Observability         | Serilog + ElasticSearch + OpenTelemetry + Grafana       |
| CI/CD                 | GitHub Actions + Docker Compose                         |
| Dev Automation        | n8n                                                     |
| Containerization      | Docker (Windows + Linux compatible)                     |

## 📁 Folder Structure

EnterpriseStarterKit/
├── README.md
├── src/
│ ├── IdentityService/
│ ├── InventoryService/
│ └── ...
├── tests/
│ ├── IdentityService.Tests/
│ └── ...
├── docker/
│ └── docker-compose.yml
└── .github/
└── workflows/
└── ci.yml