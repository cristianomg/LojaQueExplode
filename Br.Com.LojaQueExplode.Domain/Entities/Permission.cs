using System.Collections.Generic;

namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class Permission : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
