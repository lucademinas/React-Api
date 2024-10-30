using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SysAdminRepository : BaseRepository<SysAdmin>, ISysAdminRepository
    {
        private readonly ApplicationContext _context;

        public SysAdminRepository(ApplicationContext context) : base(context) 
        {
            _context = context;
        }


    }
}
