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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;

        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }

        public List<Admin> Get()
        {
            return _repository.GetAll();
        }

        public Admin? Get(int id)
        {
            return _repository.Get(id);
        }

        public void Add(UserCreateDTO dto)
        {
            var admin = new Admin()
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                UserRol = "Admin"
            };

            _repository.Add(admin);
        }

        public void Update(int id, UserUpdateDTO dto)
        {
            var admin = _repository.Get(id);
            if (admin != null)
            {
                admin.Name = dto.Name;
                admin.Email = dto.Email;

                _repository.Update(admin);
            }
        }

        public void Delete(int id)
        {
            var admin = _repository.Get(id);
            if (admin != null)
            {
                _repository.Delete(admin);
            }
        }
    }
}
