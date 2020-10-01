using Br.Com.LojaQueExplode.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.WebServices.Abstract
{
    public interface IImageStorageWebService
    {
        UploadFileResult Upload(string image);
        
    }
}
