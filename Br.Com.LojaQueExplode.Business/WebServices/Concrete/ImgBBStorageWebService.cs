using Br.Com.LojaQueExplode.Business.Models;
using Br.Com.LojaQueExplode.Business.WebServices.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.WebServices.Concrete
{
    public class ImgBBStorageWebService : IImageStorageWebService
    {
        public UploadFileResult Upload(string image)
        {
            return new UploadFileResult
            {
                Filename = "1.png",
                Name = "1",
                Mime = "image.png",
                Extension = "png",
                Url = "https://i.ibb.co/Xz7d0d3/1.png",
            };
        }
    }
}
