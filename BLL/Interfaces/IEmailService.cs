using BLL.DTO;
namespace BLL.Interfaces
{
    public interface IEmailService
    {
        public void SendMessageAboutSuccessfulRegistration(UserDTO user);
        public void SendMessageAboutNewTask(UserDTO user);
    }
}
