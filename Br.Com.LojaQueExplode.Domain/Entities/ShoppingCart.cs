﻿using System;
using System.Collections.Generic;

namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class ShoppingCart : Entity
    {
        public Guid PurchaseStatusId { get; set; }
        public DateTime EndedDate { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual PurchaseStatus PurchaseStatus { get; set; }
        public virtual ICollection<ProductShoppingCart> ProductShoppingCarts { get; set; }


    }
}