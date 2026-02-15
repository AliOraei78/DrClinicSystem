## Day 1: Dependency Injection Basics + Service Lifetimes  
Complete understanding of service lifetimes (Transient, Scoped, Singleton)

**Completed Today:**
-Created a brand-new independent project: DrClinicSystem (Doctor Clinic)
- Built the initial solution structure with clean separation of concerns:
  - DrClinicSystem.Core: pure domain entities, interfaces, services
  - DrClinicSystem.Application: use cases, business logic
  - DrClinicSystem.Infrastructure: concrete implementations (future: repositories, external services)
  - DrClinicSystem.Api: ASP.NET Core Web API as presentation layer
- Established correct project references to enforce dependency flow:
  - Api → Application + Infrastructure
  - Application → Core
  - Infrastructure → Core + Application
- Demonstrated all three main DI lifetimes with practical examples:
  - **Transient**: New instance every request/resolution (different GUID each time)
  - **Scoped**: Single instance per HTTP request (same GUID within one request, different between requests)
  - **Singleton**: Single instance for the entire application lifetime (same GUID always)
- Created a dedicated `TestController` to visually prove lifetime behavior:
  - Injected two instances of each lifetime type
  - Exposed endpoint `/api/test/lifetimes` to compare GUIDs in a single request
- Observed real-world behavior:
  - Transient instances differ even within the same request
  - Scoped instances are identical within one request, different across requests
  - Singleton instances are always the same
- Understood practical implications:
  - Transient: stateless lightweight services (calculators, factories)
  - Scoped: per-request stateful services (DbContext, user context)
  - Singleton: global shared resources (configuration, caches, loggers)

**Key Learnings:**
- DI lifetimes directly affect application behavior, memory usage, and correctness
- Misusing lifetimes (e.g., Singleton capturing Scoped DbContext) leads to bugs (captive dependency)
- Scoped is the most common lifetime in web applications (matches HTTP request scope)
- Transient is safe for stateless operations but can be expensive if overused
- Singleton should be used carefully (thread-safety, memory leaks)