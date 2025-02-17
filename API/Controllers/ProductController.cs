using API.Data;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("products")]
public class ProductController : Controller
{
    private readonly IVareService _vareService;
    public ProductController(IVareService vareService)
    {
        _vareService = vareService;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _vareService.GetAllAsync();
        if (products.Any())
        {
            return Ok(products);
        }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{ean}")]
    public async Task<IActionResult> GetProductById([FromRoute] string ean)
    {
        if (string.IsNullOrEmpty(ean))
        {
            return BadRequest("EAN must be provided.");
        }
        var product = await _vareService.GetByEanAsync(ean);
        if (product != null)
        {
            return Ok(product);
        }
        return NotFound($"Product not found with ean - {ean}");
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPostAttribute("/products/create")]
    public async Task<IActionResult> CreateProduct([FromBody]VareDTO product)
    {
        var createStatus = await _vareService.Create(product);
        if (createStatus)
        {
            return Ok();
        }

        return BadRequest("Error creating product - DTO incorrect");
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPutAttribute("/products/update")]
    public async Task<IActionResult> UpdateProduct([FromBody] VareDTO product)
    {
        var status = await _vareService.Update(product);
        if (status)
        {
            return Ok();
        }
        return NotFound("Product not found");
    }
}