using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrderDto
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public List<OrderDetailCreateDto> OrderDetails { get; set; }
    }
}
