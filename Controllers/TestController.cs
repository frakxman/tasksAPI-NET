using Microsoft.AspNetCore.Mvc;

namespace TasksAPISmart.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Test endpoint called");
        return Ok(new { message = "API is working!" });
    }
} 