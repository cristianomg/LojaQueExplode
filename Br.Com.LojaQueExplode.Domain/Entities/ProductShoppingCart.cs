using System;

namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class ProductShoppingCart : Entity
    {
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

    }
}
