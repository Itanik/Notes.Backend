# Notes.Backend
Simple ASP.NET webAPI for notes storage.

The project uses the following patterns and approaches:
1. Clean architecture (Mediator + CQRS + structural separation)
2. DB storage
3. Service locator
4. Middleware exception handling
5. Unit tests
6. OAuth authorization (an identity server <a href="https://github.com/Itanik/Notes.Identity">is here</a>)
7. Auto-generated API description (swagger)
8. Logging.

For these purposes, the following frameworks and libraries were used:
1. MediatR
2. Microsoft.EntityFrameworkCore, AutoMapper
3. ASP.NetCore
4. ASP.NetCore and FluentValidation
5. Xunit and Shouldly
6. Microsoft.AspNetCore.Authentication.JwtBearer
7. Swashbuckle.AspNetCore
8. Serilog
