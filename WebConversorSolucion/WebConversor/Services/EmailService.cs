using System.Net.Mail;

namespace WebConversor.Services
{

    public interface IEmailService
    {
        Task SendEmail(string emailReceptor, string theme, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        // private readonly string _email;
        // private readonly string _password;
        // private readonly string _host;
        // private readonly string _port;
        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
            //_email = Environment.GetEnvironmentVariable("email");
            //_password = Environment.GetEnvironmentVariable("password");
            //_host = Environment.GetEnvironmentVariable("host");
            //_port = Environment.GetEnvironmentVariable("port");
        }

        public async Task SendEmail(string emailReceptor,string theme, string body)
        {
            // var emailEmisor = Environment.GetEnvironmentVariable("email");
            // var password = Environment.GetEnvironmentVariable("password");
            // var host = Environment.GetEnvironmentVariable("host");
            // var port = int.Parse(Environment.GetEnvironmentVariable("port"));

            var emailEmisor = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:EMAIL");
            var password = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:PASSWORD");
            var host = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:HOST");
            var port = configuration.GetValue<int>("CONFIGURACIONES_EMAIL:PORT");

            
            // Console.WriteLine("Contra"+password);
            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            
            smtpClient.Credentials = new System.Net.NetworkCredential(emailEmisor, password);
            var message = new MailMessage(emailEmisor!,emailReceptor,theme,body);
            await smtpClient.SendMailAsync(message);
        }
    }
}
