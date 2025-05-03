# 🧩 Microservices — Detailed Breakdown

This section explains each microservice, its responsibilities, database design, and how it communicates with other parts of the system.

---

## 🧍 Identity Service

- **Purpose**: Handles user registration, login, and JWT token issuance.
- **Technology**: .NET, PostgreSQL
- **Auth**: Issues signed JWT tokens used for accessing protected endpoints
- **Communication**: Does not use RabbitMQ (standalone)

---

## 🚘 Auction Service

- **Purpose**: Manages auction lifecycle — creation, status, expiration
- **Technology**: .NET, PostgreSQL
- **Communication**:
  - **Publishes events**: `AuctionCreated`, `AuctionUpdated`, `AuctionFinished`
  - **gRPC Server** for receiving calls from BiddingService


💸 Bidding Service
Purpose: Handles bid logic — placing bids, checking if they meet criteria

Technology: .NET, MongoDB

Communication:

gRPC Client: Talks to Auction Service to validate auctions

Publishes: BidPlaced, BidRejected

🔍 Search Service
Purpose: Provides search capabilities for active auctions

Technology: .NET, MongoDB

Communication:

Subscribes to: AuctionCreated, AuctionUpdated

Indexes auctions in MongoDB for text-based search

📢 Notification Service
Purpose: Sends real-time updates to clients via SignalR

Technology: .NET, SignalR

Communication:

Subscribes to: AuctionUpdated, BidPlaced

Pushes updates like "New highest bid" or "Auction closed" to frontend

🛣 Gateway Service
Purpose: Central API entrypoint for all client traffic

Technology: .NET

Responsibilities:

Routing requests to appropriate services

Forwarding JWTs

Applying middleware (rate limiting, logging, etc.)


📦 Event Summary
Event	               Publisher	                  Subscribers
AuctionCreated	    Auction Service	                Search Service
AuctionUpdated	    Auction Service	                Search, Notification
AuctionFinished     Auction Service	                Notification
BidPlaced	        Bidding Service	                Notification
BidRejected	        Bidding Service	                Notification (optional)

🔁 Communication Summary
Service	             gRPC	   RabbitMQPublisher	RabbitMQSubscriber	SignalR
Identity Service	 ❌	             ❌	                 ❌	          ❌
Auction Service	     ✅	             ✅	                 ❌	          ❌
Bidding Service	     ✅	             ✅	                 ❌	          ❌
Search Service	     ❌	             ❌	                 ✅	          ❌
Notification Service ❌	             ❌	                 ✅	          ✅
Gateway Service	     ❌	             ❌	                 ❌	          ❌