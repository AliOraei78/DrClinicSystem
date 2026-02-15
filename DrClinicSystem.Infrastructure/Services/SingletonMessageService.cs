// Infrastructure/Services/SingletonMessageService.cs
using DrClinicSystem.Core.Services;

public class SingletonMessageService : IMessageService
{
    private readonly Guid _id = Guid.NewGuid();

    public string GetMessage() => $"Singleton: {_id}";
}