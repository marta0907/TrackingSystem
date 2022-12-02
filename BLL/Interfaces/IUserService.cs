using BLL.DTO;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IUserService:ICrud<UserDTO>
    {
        public UserDTO FindUserByLoginAndPassword(string login, string pwd);
        public UserDTO FindByLogin(string login);
    }

}
