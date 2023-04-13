using Ahsan.Domain.Entities;
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
        public async Task<IActionResult> SendVerificationCode(User user)
        {
            var result = await this.emailverification.SendAsync(user);
            return Ok(result);
        }
    }
}
