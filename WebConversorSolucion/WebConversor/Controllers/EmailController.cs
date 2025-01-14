using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebConversor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) {
            _emailService = emailService;
        }

        [HttpPost("SendEmail")]
        public async Task<ActionResult> SendEmail(string email,string theme, string body)
        {
            await _emailService.SendEmail(email,theme,body);
            return Ok();
        }

        
        //hacer que contacte con nosotros
        [HttpPost("Contact")]
        public async Task<ActionResult> Contact(string theme, string body)
        {
            await _emailService.Contact(theme,body);
            return Ok();
        }
    }
}
