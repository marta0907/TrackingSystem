using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Entities;
using AutoMapper;
using System.Linq;
using BLL.Validation;
using System;
namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _mapper =MapperInitializer.CreateMapperProfile();
            _unitOfWork = unitOfWork;
        }
        public void Add(UserDTO model)
        {
            var user = _mapper.Map<UserDTO, User>(model);
            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
        }

        public void DeleteById(int modelId)
        {
            _unitOfWork.Users.Delete(modelId);
            _unitOfWork.Save();
        }


        public IEnumerable<UserDTO> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            return _mapper.Map< IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public UserDTO GetById(int id)
        {
            var user = _unitOfWork.Users.Get(id);
            return _mapper.Map<User, UserDTO>(user);
        }

        public void Update(UserDTO model)
        {
            var user = _unitOfWork.Users.Get(model.Id);
            user.Name = model.Name;
            user.Age = model.Age;
            user.Email = model.Email;
            user.Password = model.Password;
            user.RoleId = user.RoleId;
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }

        public UserDTO FindUserByLoginAndPassword(string login, string pwd)
        {
            var user = _unitOfWork.Users.Find(x => x.Email == login && x.Password == pwd).FirstOrDefault();
            if (user == null)
                return null;
            else
                return _mapper.Map<User, UserDTO>(user);
        }

        public UserDTO FindByLogin(string login)
        {
            var user = _unitOfWork.Users.Find(x => x.Email == login).FirstOrDefault();
            if (user == null)
                return null;
            else
                return _mapper.Map<User, UserDTO>(user);
        }

    }
}
