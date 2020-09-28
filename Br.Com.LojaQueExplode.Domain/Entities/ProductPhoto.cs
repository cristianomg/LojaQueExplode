using System;

namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class ProductPhoto : Entity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string MimiType { get; set; }
        public string Url { get; set; }
        public virtual Product Product { get; set; }
    }
}
