using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts  { get; set; }
    }
}
