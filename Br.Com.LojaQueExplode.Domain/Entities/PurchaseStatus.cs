using System.Collections.Generic;

namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class PurchaseStatus : Entity
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
