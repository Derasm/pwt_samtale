using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _unitOfWork.Repository<Varer>().GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{ID:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int ID)
    {
        var product = await _unitOfWork.Repository<Varer>().GetByIdAsync(ID);
        return Ok(product);
    }
}