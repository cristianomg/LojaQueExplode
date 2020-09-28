using Br.Com.LojaQueExplode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.DTOs
{
    public class ResultAuthentication
    {
        public User User { get; set; }
        public string Token { get; set; }
        public string Permission { get; set; }
    }
}
