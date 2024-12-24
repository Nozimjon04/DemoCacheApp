using CacheDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace CacheDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IEmailsController : ControllerBase
{
    private readonly IEmailService emailService;

    public IEmailsController(IEmailService emailService)
    {
        this.emailService = emailService;
    }



    [HttpPost("verify")]

    public async Task<IActionResult> VerifyEmailAsync(string email)
    {
        var data = await this.emailService.VerifyEmailAsync(email);

        return Ok(data);
    }


    [HttpPost("getcode")]

    public async Task<IActionResult> GetCodeAsync(string email, string code)
    {
        var data = await this.emailService.GetCodeAsync(email, code);
        return Ok(data);
    }
}
