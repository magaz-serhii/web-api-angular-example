@ECHO OFF
dotnet ef migrations add "%1" --startup-project ../Products.WebApi