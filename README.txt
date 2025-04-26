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

Real-time Communication: SignalR

Database: (Mention if you used MongoDB, SQL Server, PostgreSQL, etc.)

⚙️ Features
🔑 Secure User Authentication & Authorization (via Identity Service)

🚘 Post and manage car listings

💰 Place bids on available cars

🥇 Live bidding competition using SignalR

📈 Set reserved price for cars

⏳ Track how long cars have been listed

🧩 Decentralized microservices architecture

🔁 Service-to-service messaging with RabbitMQ and gRPC

🧪 Thorough Unit and Integration Testing using xUnit

📦 Docker Compose support for local development

☁️ Kubernetes deployment (both local and cloud)