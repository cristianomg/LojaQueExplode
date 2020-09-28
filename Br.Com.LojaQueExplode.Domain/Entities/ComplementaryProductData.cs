namespace Br.Com.LojaQueExplode.Domain.Entities
{
    public class ComplementaryProductData : Entity
    {
        public int WarrantyTime { get; set; } // meses
        public int Weight { get; set; } // gramas

        public virtual Product Product { get; set; }
    }
}
