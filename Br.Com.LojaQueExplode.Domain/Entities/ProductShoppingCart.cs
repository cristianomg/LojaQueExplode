using System;

namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class ProductShoppingCart : Entity
    {
        public ProductShoppingCart()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

    }
}
