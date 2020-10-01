using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.Services.Concrete
{
    public class CreateCategoryService : ICreateCategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public Category Execute(DTOCreateCategory createCategory)
        {
            if (string.IsNullOrEmpty(createCategory.Name) || string.IsNullOrEmpty(createCategory.Description))
                throw new ValidationOnServiceException("Um ou mais campos estão invalidos.");

            var checkIfCategoryExists = _categoryRepository.GetByName(createCategory.Name);

            if (checkIfCategoryExists != null)
                throw new ValidationOnServiceException("Nome da categoria já está em uso.");

            var newCategory = new Category
            {
                Name = createCategory.Name,
                Description = createCategory.Description,
            };

            var createdUser = _categoryRepository.Insert(newCategory);

            _unitOfWork.Save();

            return createdUser;
        }
    }
}
