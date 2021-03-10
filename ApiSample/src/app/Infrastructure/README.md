# Infrastructure Layer

This layer contains classes for accessing external resources such as databases, file systems, web services, smtp, and so on.
These classes should be based on interfaces defined within the application layer.

Key points
- Contains classes for accessing external resources such as file systems, web services, SMTP, etc.
* Implements abstractions / interfaces defined within the Application layer
* No layers depend on Infrastructure layer. e.g. Presentation layer. (This is not quite true, as in practice it references it so as to configure the IoC container, but logically this should apply. You could create a separate project for IoC configuration to avoid this but it would not add much value).
