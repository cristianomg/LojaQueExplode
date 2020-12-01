using System;
using System.Collections.Generic;

namespace Br.Com.LojaQueExplode.Api.Models
{
    public class DTOShoppingCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PurchaseStatusId { get; set; }
        public virtual DTOPurchaseStatus PurchaseStatus { get; set; }
        public virtual ICollection<DTOProductShoppingCart> ProductShoppingCarts { get; set; }
    }
}
