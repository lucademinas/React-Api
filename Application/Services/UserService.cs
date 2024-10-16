using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        } 

        public List<User> Get()
        {
            return _repository.GetAll();
        }

        public User? Get(int id)
        {
            return _repository.Get(id);
        }

        public void Add(UserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                UserRol = dto.UserRol
            };
            _repository.Add(user);
        }

        public void Update(UserDto dto, int id)
        {
            var userUpdate = _repository.Get(id);
            if (userUpdate != null)
            {
                userUpdate.Name = dto.Name;
                userUpdate.Email = dto.Email;
                userUpdate.Password = dto.Password;
                userUpdate.UserRol = dto.UserRol;

                _repository.Update(userUpdate);
            }
        }

        public void Delete(int id)
        {
            var userDelete = _repository.Get(id);
            if (userDelete != null)
            {
                _repository.Delete(userDelete);
            }
        }
    }
}
