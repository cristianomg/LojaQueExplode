using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Api.Models
{
    public class DTOProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public int Price { get; set; } // centavos
        public int Quantity { get; set; }
        public virtual DTOCategory Category { get; set; }
        public virtual DTOComplementaryProductData ComplementaryProductData { get; set; }
        public virtual ICollection<DTOProductPhoto> Photos { get; set; }

    }
    public class DTOProductPhoto
    {
        public string Name { get; set; }
        public string MimiType { get; set; }
        public string Url { get; set; }
    }

    public class DTOComplementaryProductData
    {
        public int WarrantyTime { get; set; } // meses
        public int Weight { get; set; } // gramas

    }
}
