// Infrastructure/Services/TransientMessageService.cs
using DrClinicSystem.Core.Services;

public class TransientMessageService : IMessageService
{
    private readonly Guid _id = Guid.NewGuid();

    public string GetMessage() => $"Transient: {_id}";
}