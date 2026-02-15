// Api/Controllers/TestController.cs
using DrClinicSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly IMessageService _transient1;
    private readonly IMessageService _transient2;
    private readonly IMessageService _scoped1;
    private readonly IMessageService _scoped2;
    private readonly IMessageService _singleton1;
    private readonly IMessageService _singleton2;

    public TestController(
        [FromKeyedServices("transient")] IMessageService transient1,
        [FromKeyedServices("transient")] IMessageService transient2,
        [FromKeyedServices("scoped")] IMessageService scoped1,
        [FromKeyedServices("scoped")] IMessageService scoped2,
        [FromKeyedServices("singleton")] IMessageService singleton1,
        [FromKeyedServices("singleton")] IMessageService singleton2)
    {
        _transient1 = transient1;
        _transient2 = transient2;
        _scoped1 = scoped1;
        _scoped2 = scoped2;
        _singleton1 = singleton1;
        _singleton2 = singleton2;
    }

    [HttpGet("lifetimes")]
    public IActionResult GetLifetimes()
    {
        return Ok(new
        {
            Transient1 = _transient1.GetMessage(),
            Transient2 = _transient2.GetMessage(),
            Scoped1 = _scoped1.GetMessage(),
            Scoped2 = _scoped2.GetMessage(),
            Singleton1 = _singleton1.GetMessage(),
            Singleton2 = _singleton2.GetMessage()
        });
    }
}