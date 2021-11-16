using System;
using System.Text.Json.Serialization;

namespace Br.Com.LojaQueExplode.Business.DTOs
{
    public class DTOInsertProductOnShoppingCart
    {
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
    }
}
