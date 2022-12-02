using BLL.DTO;
using BLL.Interfaces;
using System.Net.Mail;
using System.Net;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        public async  void SendMessageAboutNewTask(UserDTO user)
        {
                    MailMessage message = new MailMessage();
                message.From = new MailAddress("tts@com.ua", "Task Tracking System");
                message.To.Add(user.Email);
                message.Subject = "New task";
                message.Body = $"Dear {user.Name},\n You have a new Task ,sing in and try your hand!!!";
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Credentials = new NetworkCredential("zheplinska@gmail.com", "zheplinska0907");
                client.Port = 587;
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }

        }

        public async void SendMessageAboutSuccessfulRegistration(UserDTO user)
        {
            
                MailMessage message = new MailMessage();
                message.From = new MailAddress("tts@com.ua", "Task Tracking System");
                message.To.Add(user.Email);
                message.Subject = "Successful Registration";
                message.Body = $"Dear {user.Name},\n You are already registered on a Task Tracking System Site!\n Sign in and try your hand !!!";
                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Credentials = new NetworkCredential("zheplinska@gmail.com", "zheplinska0907");
                    client.Port = 587;
                    client.EnableSsl = true;
                   await client.SendMailAsync(message);
                }
            
        }
    }
}
