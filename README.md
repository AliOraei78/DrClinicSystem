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

## Day 2: Advanced Dependency Injection  
Factory Methods, Options Pattern, Named Services, Decorators

**Completed Today:**
- Mastered advanced DI patterns in ASP.NET Core with practical implementation in DrClinicSystem project
- Implemented **Factory Methods**:
  - Created `NotificationFactory` to dynamically resolve notification channels (email/sms) based on type
  - Used `IServiceProvider.GetRequiredKeyedService` for safe keyed resolution
- Implemented **Options Pattern**:
  - Defined `ClinicOptions` strongly-typed configuration class
  - Configured with `Configure<ClinicOptions>` in Program.cs
  - Injected via `IOptions<ClinicOptions>` and used in `AppointmentService`
  - Tested with `/api/test/appointment-service` endpoint (displayed clinic settings)
- Implemented **Named/Keyed Services**:
  - Defined `INotificationChannel` interface in Core
  - Created concrete implementations (`EmailChannel`, `SmsChannel`) in Infrastructure
  - Registered with `AddKeyedTransient` in Program.cs
  - Injected with `[FromKeyedServices("email")]` and `[FromKeyedServices("sms")]` in `NotificationService`
- Implemented **Decorator Pattern**:
  - Created `LoggingAppointmentServiceDecorator` to log before/after appointment scheduling
  - Used Scrutor or manual decoration to wrap base `AppointmentService`
  - Demonstrated behavior extension without modifying original class
- Tested all patterns in practice:
  - Factory: dynamic channel selection via query parameter `type`
  - Options: clinic settings correctly loaded and displayed
  - Named Services: correct channel selected and executed
  - Decorator: logging appeared in console during scheduling
- Updated README with explanations, structure, and usage examples

**Key Learnings:**
- Factory Methods: Dynamic resolution of implementations at runtime (great for pluggable strategies)
- Options Pattern: Strongly-typed, validated, configuration injection (better than raw IConfiguration)
- Named/Keyed Services: Multiple implementations of same interface with safe selection via keys (.NET 8+)
- Decorators: Open-Closed Principle in practice – add behavior (logging, caching, metrics) without changing core classes
- All patterns respect Clean/Onion: Core stays pure, Application owns contracts, Infrastructure provides implementations