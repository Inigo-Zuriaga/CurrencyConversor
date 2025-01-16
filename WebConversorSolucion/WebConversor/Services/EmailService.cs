using System.Net.Mail;

namespace WebConversor.Services
{

    public interface IEmailService
    {
        Task SendEmail(string emailReceptor, string theme, string body);
        Task Contact(string theme, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _emailEmisor;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            // _emailEmisor = _configuration.GetValue<string>("CONFIGURACIONES_EMAIL:EMAIL");
            // _password = _configuration.GetValue<string>("CONFIGURACIONES_EMAIL:PASSWORD");
            _emailEmisor = Environment.GetEnvironmentVariable("EMAIL");
            _password = Environment.GetEnvironmentVariable("PASSWORD");
            _host = _configuration.GetValue<string>("CONFIGURACIONES_EMAIL:HOST");
            _port = _configuration.GetValue<int>("CONFIGURACIONES_EMAIL:PORT");
        }
        
        
        //AL registrar un usuario, enviar un correo de confirmación
        public async Task SendEmail(string emailReceptor,string theme, string body)
        {
            var smtpClient = new SmtpClient(_host, _port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            
            smtpClient.Credentials = new System.Net.NetworkCredential(_emailEmisor, _password);
            var message = new MailMessage(_emailEmisor!,emailReceptor,theme,body);
            await smtpClient.SendMailAsync(message);
        }
        //Harcodear correo de receptor
        public async Task Contact(string theme, string body)
        {
            var smtpClient = new SmtpClient(_host, _port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            
            smtpClient.Credentials = new System.Net.NetworkCredential(_emailEmisor, _password);
            var message = new MailMessage(_emailEmisor!,_emailEmisor,theme,body);
            await smtpClient.SendMailAsync(message);
            
        }
    }
}
