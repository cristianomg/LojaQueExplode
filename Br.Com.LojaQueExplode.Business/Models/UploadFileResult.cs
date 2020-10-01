using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.Models
{
    public class UploadFileResult
    {
        public string Filename { get; set; }
        public string Name { get; set; }
        public string Mime { get; set; }
        public string Extension { get; set; }
        public string Url { get; set; }
    }
}
