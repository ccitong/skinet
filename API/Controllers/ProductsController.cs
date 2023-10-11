using System.ComponentModel;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   
    public class ProductsController: BaseApiController
    {   
       
        private IGenericRepositrory<Product> _productsRepo ;
        private IGenericRepositrory<ProductBrand> _productBrandRepo ;
        private IGenericRepositrory<ProductType> _productTypeRepo ;
        private IMapper _mapper;

        public ProductsController(IGenericRepositrory<Product> productsRepo,
        IGenericRepositrory<ProductBrand> productBrandRepo, IGenericRepositrory<ProductType> productTypeRepo,
        IMapper mapper )
        {   
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts (){

            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }


         [HttpGet("{id}")]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct (int id){

            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            if(product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductToReturnDto>(product);
           
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands (){
            return Ok(await _productBrandRepo.ListAllAsync());
        }


         [HttpGet("types")]
        public async Task<ActionResult<ProductBrand>> GetProductTypes (){
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}