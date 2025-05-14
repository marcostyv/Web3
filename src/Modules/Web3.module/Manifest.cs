using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Web3.module",
    Author = "The Orchard Core Team",
    Website = "https://orchardcore.net",
    Version = "0.0.1",
    Description = "Web3.module",
    Category = "Content Management",
    Dependencies = new[] { "OrchardCore.ContentFields" , "OrchardCore.Media"}
)]
