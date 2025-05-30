﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Api.Contexts;
using Northwind.Api.DTOs;
using Northwind.Api.Entities;
using Northwind.Api.Services.Interfaces;
using Northwind.Api.Enums;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductReadDTO>>> GetAll()
        {
            var productEntities = await _productService.GetAllProducts();

            if (productEntities == null)
            {
                return NotFound();
            }

            var productDtos = productEntities.Select(productEntity => new ProductReadDTO
            {
                ProductName = productEntity.ProductName,
                CategoryId = Convert.ToInt32(productEntity.CategoryId),
                SupplierId = Convert.ToInt32(productEntity.SupplierId),
                QuantityPerUnit = productEntity.QuantityPerUnit,
                UnitPrice = Convert.ToInt32(productEntity.UnitPrice),
                UnitsInStock = Convert.ToInt32(productEntity.UnitsInStock),
                UnitsOnOrder = Convert.ToInt32(productEntity.UnitsOnOrder),
                ReorderLevel = Convert.ToInt32(productEntity.ReorderLevel),
                Discontinued = productEntity.Discontinued,
                CategoryName = productEntity.Category.CategoryName
            });

            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDTO>> Get(int id)
        {
            var productEntity = await _productService.GetProduct(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            var productDto = new ProductReadDTO
            {
                ProductName = productEntity.ProductName,
                CategoryId = Convert.ToInt32(productEntity.CategoryId),
                SupplierId = Convert.ToInt32(productEntity.SupplierId),
                QuantityPerUnit = productEntity.QuantityPerUnit,
                UnitPrice = Convert.ToInt32(productEntity.UnitPrice),
                UnitsInStock = Convert.ToInt32(productEntity.UnitsInStock),
                UnitsOnOrder = Convert.ToInt32(productEntity.UnitsOnOrder),
                ReorderLevel = Convert.ToInt32(productEntity.ReorderLevel),
                Discontinued = productEntity.Discontinued,
                CategoryName = productEntity.Category.CategoryName
            };

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductCreateDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var product = new Product
            {
                ProductName = productDto.ProductName,
                CategoryId = productDto.CategoryId,
                SupplierId = productDto.SupplierId,
                QuantityPerUnit = productDto.QuantityPerUnit,
                UnitPrice = productDto.UnitPrice,
                UnitsInStock = Convert.ToInt16(productDto.UnitsInStock),
                UnitsOnOrder = Convert.ToInt16(productDto.UnitsOnOrder),
                ReorderLevel = Convert.ToInt16(productDto.ReorderLevel),
                Discontinued = productDto.Discontinued
            };

            ResponseCode response = await _productService.AddProduct(product);

            if (response == ResponseCode.Created)
            {
                return Created();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ProductCreateDTO productDto) //ProductUpdateDTO ??? Kanske mer cleant, men vi skippar det här. 
        {
            var productEntity = await _productService.GetProduct(id);   
            if (productEntity == null)
            {
                return NotFound();
            }

            productEntity.ProductName = productDto.ProductName;
            productEntity.CategoryId = productDto.CategoryId;
            productEntity.SupplierId = productDto.SupplierId;
            productEntity.QuantityPerUnit = productDto.QuantityPerUnit;
            productEntity.UnitPrice = productDto.UnitPrice;
            productEntity.UnitsInStock = Convert.ToInt16(productDto.UnitsInStock);
            productEntity.UnitsOnOrder = Convert.ToInt16(productDto.UnitsOnOrder);
            productEntity.ReorderLevel = Convert.ToInt16(productDto.ReorderLevel);
            productEntity.Discontinued = productDto.Discontinued;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productEntity = await _productService.GetProduct(id);
            if (productEntity != null)
            {
                return NoContent();
            }

            return NotFound();
        }

    }

}
