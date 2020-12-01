using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.DTOs
{
    public class DTOInsertProductOnShoppingCart
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
    }
}
