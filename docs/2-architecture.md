# 🧱 Architecture

This project is built as a microservices system using modern communication and deployment patterns.

---

## 🔄 High-Level Architecture

- **Client Apps** (browser, mobile) send requests to:
- **NGINX Ingress** (Kubernetes)
- **Gateway Service** handles routing and forwarding
- Each microservice handles its own domain logic and data storage


---

## 🧩 Microservices Breakdown

| Service             | DB         | Role                                                                |
|---------------------|------------|---------------------------------------------------------------------|
| Identity Service    | PostgreSQL | User authentication/authorization (JWT), not on RabbitMQ            |
| Auction Service     | PostgreSQL | Auction creation, status tracking, publishes events                 |
| Bidding Service     | MongoDB    | Handles bids, connects via **gRPC** to Auction Service              |
| Search Service      | MongoDB    | Indexes auctions for search, subscribes to auction events           |
| Notification Service| —          | Uses **SignalR** to push updates, subscribes to events              |
| Gateway Service     | —          | Routes all API requests                                             |

---

## 🔁 Communication Patterns

### 🟧 Event Bus (RabbitMQ)

| Publisher        | Subscribers              |
|------------------|---------------------------|
| Auction Service  | Search, Notification      |

- Events like `AuctionCreated`, `AuctionUpdated`, etc., are published and consumed asynchronously.

### 🟢 gRPC

- **Bidding Service ↔ Auction Service**: Used for real-time bid validation and communication.

### 📡 SignalR

- Used by **Notification Service** to send real-time updates to frontend clients about live auctions and bids.

---

## 🔐 Security

- JWT tokens are issued by the Identity Service.
- Gateway forwards token in requests for authorization in downstream services.
