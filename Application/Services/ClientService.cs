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
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository) 
        {
            _repository = repository; 
        }

        public List<Client> Get()
        {
            return _repository.GetAll();
        }

        public Client? Get(int id)
        {
            return _repository.Get(id);
        }

        public void Add(UserCreateDTO dto)
        {
            var client = new Client()
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                UserRol = "Client"
            };

            _repository.Add(client);
        }

        public void Update(int id, UserUpdateDTO dto)
        {
            var client = _repository.Get(id);
            if (client != null)
            {
                client.Name = dto.Name;
                client.Email = dto.Email;
                
                _repository.Update(client);
            }
        }

        public void Delete(int id)
        {
            var client = _repository.Get(id);
            if (client != null)
            {
                _repository.Delete(client);
            }
        }

 
    }
}
