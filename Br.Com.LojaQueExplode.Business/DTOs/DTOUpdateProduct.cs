using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Business.DTOs
{
    public class DTOUpdateProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public int Price { get; set; } // centavos
        public int Quantity { get; set; }
    }
}
