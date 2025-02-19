using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Shared.DTO;

namespace API.Controllers;
[Authorize]
[ApiController]
[Route("products")]
public class ProductController : Controller
{
    private readonly IVareService _vareService;
    private readonly ILogger<ProductController> _logger;
    private readonly IVareDTOService _vareDTOService;
    public ProductController(
        IVareService vareService,
        ILogger<ProductController> logger, IVareDTOService vareDTOService)
    {
        _vareService = vareService;
        _logger = logger;
        _vareDTOService = vareDTOService;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _vareDTOService.GetAllFullWithStockAsync();
        //Now we make a new list, combining products and quanities
        if (products.Any())
        {
            return Ok(products);
        }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("/products/basic")]
    public async Task<IActionResult> GetAllProductsAsBasicDTO()
    {
        var products = await _vareDTOService.GetAllBasicWithStockAsync();
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
        var productDTO = await _vareDTOService.GetByEanAsync(ean);
        if (productDTO == null)
        {
            return NotFound($"Product not found with ean - {ean}");
        }
        return Ok(productDTO);
            


    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPostAttribute("/products/create")]
    public async Task<IActionResult> CreateProduct([FromBody]FullVareDTO productDTO)
    {
        var createStatus = await _vareDTOService.Create(productDTO);
        if (createStatus)
        {
            return Ok();
        }

        return BadRequest("Error creating product - DTO incorrect");
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPutAttribute("/products/update")]
    public async Task<IActionResult> UpdateProduct([FromBody] FullVareDTO productDTO)
    {
        var product = _vareDTOService.ToEntity(productDTO);
        var status = await _vareService.Update(product);
        if (status)
        {
            return Ok();
        }
        _logger.LogWarning($"Failed to update product - DTO incorrect - {product}");
        return NotFound("Product not updated");
    }
}