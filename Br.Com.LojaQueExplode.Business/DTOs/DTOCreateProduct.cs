using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.DTOs
{
    public class DTOCreateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public int Price { get; set; } // centavos
        public int Quantity { get; set; }
        public int WarrantyTime { get; set; } // meses
        public int Weight { get; set; } // gramas
        public List<string> Photos { get; set; }

    }
}
