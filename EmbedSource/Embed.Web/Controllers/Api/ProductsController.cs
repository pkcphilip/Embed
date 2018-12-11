using AutoMapper;
using Embed.Core.Entities;
using Embed.Persistance.Repositories;
using Embed.Web.Core.Dtos;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Embed.Web.Controllers.Api
{
    [Authorize]
    public class ProductsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("api/products/")]
        public IHttpActionResult GetProducts()
        {
            var products = _unitOfWork.Products.GetAllProducts();

            var responseDto = new ProductResponseBasicDto()
            {
                Id = Request.GetCorrelationId().ToString(),
                Timestamp = DateTime.UtcNow,
                Products = products.Select(Mapper.Map<Product, ProductBasicDto>).ToList()
            };

            return Ok(BuildProductBasicDtoResponse(products, Request.GetCorrelationId().ToString()));
        }

        [HttpGet]
        [Route("api/products/{id:long}")]
        public IHttpActionResult GetProduct(long id)
        {
            var product = _unitOfWork.Products.GetProduct(id);

            if (product == null)
                return NotFound();

            var responseDto = new ProductResponseBasicDto()
            {
                Id = Request.GetCorrelationId().ToString(),
                Timestamp = DateTime.UtcNow,
                Products = new List<ProductBasicDto>() { Mapper.Map<ProductBasicDto>(product) }
            };

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("api/products/{ids}")]
        public IHttpActionResult GetProducts([FromUri] string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return NotFound();

            try
            {
                var productIds = ids.Split(',').Select(long.Parse).ToList();
                var products = _unitOfWork.Products.GetProductsByIds(productIds);

                if (products == null)
                    return NotFound();

                return Ok(BuildProductBasicDtoResponse(products, Request.GetCorrelationId().ToString()));
            }
            catch (FormatException)
            {
                return BadRequest("Invalid products ids passed to query parameters.");
            }
            catch (OverflowException)
            {
                return BadRequest("Invalid products ids passed to query parameters. The id is too large.");
            }
        }

        [HttpPut]
        [Route("api/products/")]
        public IHttpActionResult PutNewProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newProduct = _unitOfWork.Products.Add(Mapper.Map<ProductDto, Product>(productDto));

            _unitOfWork.Complete();

            return Ok(BuildProductDtoResponse(newProduct, Request.GetCorrelationId().ToString()));
        }

        [HttpPut]
        [Route("api/products/{id:long}")]
        public IHttpActionResult PutProduct(long id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _unitOfWork.Products.GetProduct(id);

            if (product == null)
                return NotFound();

            product.Modify(productDto.Name, productDto.Quantity, productDto.SaleAmount);

            _unitOfWork.Complete();

            return Ok(BuildProductDtoResponse(product, Request.GetCorrelationId().ToString()));
        }

        [HttpPut]
        [Route("api/products/all")]
        public IHttpActionResult PutProducts([FromBody] IEnumerable<ProductDto> productDtos)
        {
            if (productDtos == null || !productDtos.Any())
                return BadRequest("Empty product data in request body.");

            foreach (var dto in productDtos)
                Validate(dto);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productDtosWithId = productDtos
                        .Where(p => p.Id != null)
                        .Select(i => i.Id.GetValueOrDefault()).ToList();

            if (productDtosWithId.Count() > 0)
            {
                var productsToBeUpdated = _unitOfWork.Products.GetProductsByIds(productDtosWithId);
                var invalidProductIds = productDtosWithId.Except(productsToBeUpdated.Select(p => p.Id));

                if (invalidProductIds.Count() > 0)
                    return BadRequest($"Product(s) not found for the following invalid product Id(s) in the list: {string.Join(",", invalidProductIds)}.");
            }

            var processedProducts = new List<Product>();

            foreach (var productDto in productDtos)
            {
                if (productDto.Id.GetValueOrDefault() == 0)
                {
                    processedProducts.Add(_unitOfWork.Products.Add(Mapper.Map<ProductDto, Product>(productDto)));
                    continue;
                }

                var product = _unitOfWork.Products.GetProduct(productDto.Id.GetValueOrDefault());

                if (product != null)
                {
                    product.Modify(productDto.Name, productDto.Quantity, productDto.SaleAmount);
                    processedProducts.Add(product);
                }
            }

            _unitOfWork.Complete();

            var response = new ProductResponseDto()
            {
                Id = Request.GetCorrelationId().ToString(),
                Timestamp = DateTime.UtcNow,
                Products = Mapper.Map<List<Product>, List<ProductDto>>(processedProducts)
            };

            return Ok(response);
        }

        private ProductResponseDto BuildProductDtoResponse(Product product, string correlationId)
        {
            return new ProductResponseDto()
            {
                Id = correlationId,
                Timestamp = DateTime.UtcNow,
                Products = new List<ProductDto> { Mapper.Map<Product, ProductDto>(product) }
            };
        }

        private ProductResponseBasicDto BuildProductBasicDtoResponse(IEnumerable<Product> products, string correlationId)
        {
            return new ProductResponseBasicDto()
            {
                Id = correlationId,
                Timestamp = DateTime.UtcNow,
                Products = products.Select(Mapper.Map<Product, ProductBasicDto>).ToList()
            };
        }
    }
}
