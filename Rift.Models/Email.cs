

using System.Net.Mail;

namespace Rift.Models
{
    public class Email
    {
        SmtpClient client = new SmtpClient();

        private readonly string _emailUser;
        private readonly string _passwordUser;

        public Email(string email, string senha)
        {
            _emailUser = email;
            _passwordUser = senha;

            EnviarEmail();
        }
        public void EnviarEmail()
        {
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("guisempresantos@gmail.com", "Deus1234");
            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress("guisempresantos@gmail.com", "Guilherme");
            mail.From = new MailAddress("guisempresantos@gmail.com");
            mail.To.Add(new MailAddress(_emailUser));
            mail.Subject = "Lembrete de Senha!!!";
            mail.Body = "Olá, sua senha é " + _passwordUser;
            client.Send(mail);

        }
        
    }
}
