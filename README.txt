Car Portal Sales 🚗
Car Portal Sales is a modern microservices-based web application for buying and selling cars through a live bidding system.
Each service is autonomous and communicates with others through RabbitMQ and gRPC when needed.
Live bids are handled through SignalR for real-time updates.

🛠 Tech Stack
Frontend: Next.js

Backend Services: .NET 

Service Communication:

RabbitMQ – Event-driven communication (Service Bus)

gRPC – Synchronous service-to-service communication

Authentication: Identity Service (Custom .NET Identity)

# 🚗 Car Portal Sales - Microservices Application

A fully containerized, scalable, microservice-based car sales and auction platform built with **.NET**, **Next.js**, **RabbitMQ**, **gRPC**, and **SignalR**, deployed via **Docker Compose** and **Kubernetes**.

---


## 🧩 Microservices Overview

| Service              | Description                                                                 |
|----------------------|-----------------------------------------------------------------------------|
| **Gateway Service**  | API Gateway routing requests to backend microservices                       |
| **Identity Service** | Handles user registration/login/auth (uses PostgreSQL)                      |
| **Auction Service**  | Manages auction lifecycle, reserved price, and duration (PostgreSQL)        |
| **Search Service**   | Indexes auctions for fast search (MongoDB)                                  |
| **Bidding Service**  | Handles bids and competition logic (MongoDB, gRPC communication)            |
| **Notification Service** | Sends live updates to clients via **SignalR**                        |

---

## 🛠️ Technologies Used

- **Frontend:** Next.js (React)
- **Backend:** .NET 9 (C#)
- **Service Communication:**
  - 🟢 **gRPC:** Auction ↔ Bidding Service
  - 🟠 **RabbitMQ (Event Bus):** Asynchronous communication
    - Auction publishes events (e.g., AuctionCreated)
    - Search and Notification Services subscribe
- **Real-time Updates:** SignalR for live bids
- **Authentication:** Identity microservice with JWT
- **Databases:**
  - PostgreSQL for Identity & Auction Services
  - MongoDB for Search & Bidding Services

---

## 📦 Deployment

### 🐳 Docker Compose (Local)

Each service is containerized and runs locally using `docker-compose`.

```bash
docker-compose up --build

Testing
Unit Testing: XUnit for core business logic

Integration Testing: Service-to-service tests using TestContainers

📌 Features
🔐 User registration and login

🚘 Post your car for auction with a reserved price and expiry time

🏁 Real-time bidding with auto-refresh via SignalR

🔍 Search available auctions with dynamic updates

📨 Notification system for auction and bid updates

💬 gRPC for fast bid processing

⚙️ Fully event-driven and scalable microservices architecture
