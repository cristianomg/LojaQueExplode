using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Exceptions;
using Br.Com.LojaQueExplode.Business.Models;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Business.WebServices.Abstract;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.Repositories.Concrete;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Br.Com.LojaQueExplode.Business.Services.Concrete
{
    public class CreateProductService : ICreateProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageStorageWebService _imageStorageWebService;

        public CreateProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, ICategoryRepository categoryRepository,
            IImageStorageWebService imageStorageWebService)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageStorageWebService = imageStorageWebService;
        }

        public Product Execute(DTOCreateProduct createProduct)
        {
            if (string.IsNullOrEmpty(createProduct.Name) || string.IsNullOrEmpty(createProduct.Description))
                throw new ValidationOnServiceException("Um ou mais campos estão invalidos.");

            var checkIfCategoryExists = _categoryRepository.GetById(createProduct.CategoryId);

            if (checkIfCategoryExists == null)
                throw new ValidationOnServiceException("A categoria informada não existe.");

            var checkIfProductExists = _productRepository.GetByName(createProduct.Name);

            if (checkIfProductExists != null)
                throw new ValidationOnServiceException("Nome do produto já está em uso.");

            var complementaryData = new ComplementaryProductData
            {
                WarrantyTime = createProduct.WarrantyTime,
                Weight = createProduct.Weight
            };


            var photos = GetUploadedPhotos(createProduct.Photos);

            var newProduct = new Product
            {
                Name = createProduct.Name,
                Description = createProduct.Description,
                CategoryId = checkIfCategoryExists.Id,
                Price = createProduct.Price,
                Quantity = createProduct.Quantity,
                ComplementaryProductData = complementaryData,
                Photos = photos.Select(x => new ProductPhoto
                {
                    Name = x.Name,
                    Url = x.Url,
                    MimiType = x.Mime
                }).ToList()
            };

            var createdUser = _productRepository.Insert(newProduct);

            _unitOfWork.Save();

            return createdUser;
        }

        private List<UploadFileResult> GetUploadedPhotos(List<string> photos)
        {
            var uploadedPhotos = new List<UploadFileResult>();

            foreach(var photo in photos)
            {
                uploadedPhotos.Add(_imageStorageWebService.Upload(photo));
            }

            return uploadedPhotos;
        }
    }
}
