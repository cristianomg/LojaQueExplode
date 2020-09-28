using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Api.Models
{
    public class DTOResultAuthentication
    {
        public DTOUser User { get; set; }
        public string Token { get; set; }
        public string Permission { get; set; }
    }
}
