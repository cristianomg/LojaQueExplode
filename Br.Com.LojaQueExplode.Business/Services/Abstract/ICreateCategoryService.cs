using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.Services.Abstract
{
    public interface ICreateCategoryService
    {
        Category Execute(DTOCreateCategory category);
    }
}
