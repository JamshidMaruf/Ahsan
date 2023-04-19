using Ahsan.Service.DTOs.Users;
using Ahsan.Service.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly EmailVerification emailverification;

        public EmailController(EmailVerification emailverification)
        {
            this.emailverification = emailverification;
        }

        [HttpPost]
        public async Task<IActionResult> SendVerificationCode(string email)
        {
            var result = await this.emailverification.SendAsync(email);
            return Ok(result);
        }
    }
}
