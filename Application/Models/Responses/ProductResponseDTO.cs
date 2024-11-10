using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Stock {  get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public AdminResponseDTO Admin { get; set; }
    }
}
