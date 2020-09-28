using System;
using System.Collections.Generic;

namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public int Price { get; set; } // centavos
        public int Quantity { get; set; }
        public Guid ComplementaryProductDataID { get; set; }

        public virtual Category Category { get; set; }
        public virtual ComplementaryProductData ComplementaryProductData { get; set; }
        public virtual ICollection<ProductPhoto> Photos { get; set; }
        public virtual ICollection<ProductShoppingCart> ProductShoppingCarts { get; set; }


    }
}
