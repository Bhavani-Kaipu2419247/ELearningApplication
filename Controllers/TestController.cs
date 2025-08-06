
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("greeting")]
    public IActionResult CorsCheck()
    {
        Console.WriteLine("CORS endpoint hit");
        return Ok("CORS is working");
    }
}