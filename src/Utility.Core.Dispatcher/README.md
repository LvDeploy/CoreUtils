# Introduction 
This project is a in-process dispatcher for single handler call, is not a service Bus for distributed event scenario.
It support pipelines behaviors and was adapt for .NET Standard 2.1, to be compatible with .NET Core 3.0+. The original .net10 project is credited to 
iammukeshm in github.

TargetFramework: NETSTANDART2.1
# Getting Started
Make sure the Utility.Core.Dispatcher is being used and inject the service like this:

```text
builder.Services.AddDispatcher(Assembly.GetExecutingAssembly());
```
- Behaviors available for injection

```text
builder.Services.AddPipelineBehavior(typeof(LoggingBehavior<,>));
```

# Build 
Add the Utility.Core.Dispatcher as a project reference to your solution then build and run your main application
e.g.: dotnet run --project src/{ProjectName}.WebApi
