using System.Collections.Generic;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Entities;
using BLL.DTO;
using AutoMapper;
namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _mapper =MapperInitializer.CreateMapperProfile();
            _unitOfWork = unitOfWork;
        }
    
        public IEnumerable<RoleDTO> GetRoles()
        {
            var roles = _unitOfWork.Roles.GetAll();
            return _mapper.Map<IEnumerable<Role>,IEnumerable <RoleDTO>> (roles);
        }
       
    }
}
