# 🚀 CreateKestrel - My Own Web Server

A study-driven web server built from C# focusing on Low-Level Network, Pipelines, HTTP Parsing and Routing. 

## 📌 About the Project

This project is an experimental implementation of Kestrel built directly on top of TCP Sockets.

Instead of using existing Frameworks, the goal is to understand how Kestrel works internally, from raw byte HTTP parsing to HTTP Request Handling. 

## 🎯 Goals:

* Understand about HTTP protocol;
* Learn how Kestrel and ASP.Net Core work under the hood;
* Create my own Kestrel and ASP.NET;
* Explore TCP communication and Network protocols;
* Learning core concepts of modern web servers.

## ⚙️ Core Concepts

This repository is based in a few core concepts:

### 🔌 TCP Communication

Implementing a client-server communication using low-level Sockets API.

### 🔄️ Pipelines

Understanding `System.IO.Pipelines` to efficiently process streaming data.

### 📦 Buffers

Breaking data in small chunks, simulating the same behaviour of real servers.

### 🌐 HTTP Parsing

Implementing HTTP Parsing collecting core information, like Method, Path and Headers. 

### 🧭 Routing

Creating a basic Routing system that maps paths to responses. 

## 🧠 How does it works? 

This web server follows this flow: 

1. Accept incoming TCP connections;
2. Read data incoming data into a Pipeline;
3. Parse HTTP Requests;
4. Process the Request and implementing a simple routing system;
5. Send the HTTP Request back to the client.

## 🛠️ Technolgies:

- C#;
- .NET;
- TCP Sockets;
- System.IO.Pipelines.

## 🚀 Features:

- Asynchronous request handling;
- Manual HTTP Parsing;
- Simple Routing System; 
- Stream-based processing using Pipelines.

## 💡 Design Decisions

- **Pipelines over tradicional streams**
  - Chosen to understand how high performance servers manage memory and throughput.
- **Manual HTTP Parsing**
  - Chosen to understand about HTTP protocol.
- **Simple Routing Approach**
  - Focused on learning rather than scalability.

## ⚠️ Limitations

This is a study project and does not aim to be production-ready.

- No Full HTTP specification support;
- No concurrency optimizations beyond basic async handling;
- Minimal error handling;
- Simple Routing approach.

# 📚 Inspirations

This project is inspired by how web servers (like Kestrel) work internally.

# 🗺️ Future Roadmap

- [] HTTP Headers Support;
- [] Middleware System;
- [] Better Routing;
- [] Logging;

# 🫱🏼‍🫲🏼 Contribution

Feel free to open issues or to suggest improvements! 