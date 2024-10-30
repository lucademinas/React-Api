using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        List<Client> Get();
        Client? Get(int id);
        void Add(UserCreateDTO dto);
        void Update(int id, UserUpdateDTO dto);
        void Delete(int id);
    }
        
       
}
