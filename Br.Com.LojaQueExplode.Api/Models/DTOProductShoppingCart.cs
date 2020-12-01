using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Api.Models
{
    public class DTOProductShoppingCart
    {
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public virtual DTOProduct Product { get; set; }
    }
}
