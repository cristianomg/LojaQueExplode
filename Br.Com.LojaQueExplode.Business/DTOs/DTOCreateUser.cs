using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.DTOs
{
    public class DTOCreateUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string CheckedPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
