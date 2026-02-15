// Infrastructure/Services/ScopedMessageService.cs
using DrClinicSystem.Core.Services;

public class ScopedMessageService : IMessageService
{
    private readonly Guid _id = Guid.NewGuid();

    public string GetMessage() => $"Scoped: {_id}";
}