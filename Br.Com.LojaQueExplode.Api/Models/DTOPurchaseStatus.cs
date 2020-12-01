using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Api.Models
{
    public class DTOPurchaseStatus
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
