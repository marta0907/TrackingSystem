using System.Collections.Generic;
using BLL.DTO;
namespace BLL.Interfaces
{
    public interface IRoleService
    {
        public IEnumerable<RoleDTO> GetRoles();
    }
}
