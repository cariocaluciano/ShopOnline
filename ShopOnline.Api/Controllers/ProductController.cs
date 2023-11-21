using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Data;
using ShopOnline.Api.Repositories;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;
using System.Runtime.CompilerServices;
using ShopOnline.Api.Extensions;

namespace ShopOnline.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
  private readonly IProductRepository productRepository;

  public ProductController(IProductRepository productRepository)
  {
    this.productRepository = productRepository;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
  {
    try
    {
      var products = await this.productRepository.GetItems();
      var productCategories = await this.productRepository.GetCategories();

      if (products == null || productCategories == null)
      { 
       return NotFound(); 
      }
      else
      {
        DtoConversions dtoConversion = new DtoConversions();
        var productDto = dtoConversion.ConvertToDto(products, productCategories);

        return Ok(productDto);
      }
    }
    catch (Exception)
    {

      return StatusCode(StatusCodes.Status500InternalServerError, "Error retieving data from the database");
    }

  }
}
