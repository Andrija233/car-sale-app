# 📘 Project Overview

## 🚗 Car Portal Sales — Microservices Application

**Car Portal Sales** is a real-time, scalable auction platform for buying and selling cars, built using a microservices architecture. Users can register, post cars for sale, place bids, and compete in live auctions — all in a modern, event-driven ecosystem.

---

## 🎯 Goals

- Demonstrate strong understanding of:
  - Distributed systems
  - Event-driven architecture
  - Clean architecture and service boundaries
  - Real-time communication with SignalR
  - Secure user authentication
  - Scalable deployment using Docker and Kubernetes

---

## 👨‍💻 Core Features

- 🔐 User registration and login (JWT-based)
- 🚘 Post cars for auction with reserved price and expiration time
- 📢 Real-time bidding via SignalR
- 🔍 Full-text search of ongoing auctions
- 📨 Event-driven updates and notifications
- ⚙️ gRPC communication between services
- 🐳 Dockerized and Kubernetes-ready

---

## 🛠️ Tech Stack

| Layer        | Tech                          |
|--------------|-------------------------------|
| Frontend     | Next.js (React)               |
| Backend      | .NET (C#)                     |
| Auth         | Identity Service (.NET + Postgres) |
| DBs          | PostgreSQL, MongoDB           |
| Messaging    | RabbitMQ (Event Bus)          |
| Real-Time    | SignalR                       |
| Inter-Service| gRPC (Auction ↔ Bidding)      |
| Deployment   | Docker Compose, Kubernetes    |

---

## 📎 Use Case Example

1. A user registers and logs in.
2. Posts a car with a reserved price and auction duration.
3. Other users place bids in real-time.
4. System updates UI instantly via SignalR.
5. Auction ends when time runs out or price is met.
