﻿using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        List<User> Get();
        User? Get(int id);
        void Add(UserCreateDTO dto);
        void Update(UserUpdateDTO dto, int id);
        void Delete(int id);
    }
}
