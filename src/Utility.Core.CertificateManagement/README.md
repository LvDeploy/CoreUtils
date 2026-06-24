# Introduction 
This a project was made to retrieve certificate files that require password imported from S3 Storage.
You can retrieve a single file that can filtered by SerialNumber.

TargetFramework: NETSTANDART2.1
# Getting Started
Before import this library make sure these parameters are configured in your appsettings:

```text
CertificateManagement:
├── AccessKey
├── AccessSecret
├── S3BucketName
├── Region
└── SessionToken
```
- For Inject the Dependencies use the static class extension: Bootstrapper.RegisterCertificateFileServices(configuration)

The `configuration` must come from `IConfiguration`.

- To access the main methods use the contract name: `IFileP12`

# Build 
Add the Utility.Core.CertificateManagement as a project reference to your solution then build and run your main application
e.g.: dotnet run --project src/{ProjectName}.WebApi
