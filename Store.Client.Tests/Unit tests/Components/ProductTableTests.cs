using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Store.Client.Components;
using Store.Client.Services;
using Store.Shared.DTO;

namespace Store.Client.Tests;

public class ProductTableTests : TestContext
{
    private readonly Mock<IProductService> _mockProductService;

    public ProductTableTests()
    {
        //Ofc initialize the mock
        _mockProductService = new Mock<IProductService>();
        //tell context that there is a service for it to use. This will inject mock instead of usual ProductService
        Services.AddSingleton<IProductService>(_mockProductService.Object);
    }

    [Fact]
    public void ProductTable_Should_Start_With_Null_Products_And_Loading()
    {
        //arrange
        var productTable = RenderComponent<ProductTable>();
        //act
        
        //assert
        Assert.Null(productTable.Instance.products);
    }
    [Fact]
    public async void ProductTable_Should_Load_Products_On_Initialization()
    {
        //arrange
  
        // Step 1: Mock product data for the service
        var mockProducts = new List<BasicVareDTO>
        {
            new() { ItemDescription = "Pants", SuggestedRetailPrice = 10, ItemGroupName = "Category 1", Quantity = 5, EAN = "12345678", Currency = "DKK"},
            new() { ItemDescription = "Shirt", SuggestedRetailPrice = 15, ItemGroupName = "Category 2", Quantity = 0, EAN = "87654321", Currency = "DKK"}
        };
        // Fake the call to the Service.
        _mockProductService.Setup(service => service.GetAllBasicVare())
            .ReturnsAsync(mockProducts);
        // Ordering is important. Now the productTable will use the mocked service. 
        var productTable = RenderComponent<ProductTable>();

        //act
        productTable.WaitForState(() => productTable.Instance.products.Count > 0, TimeSpan.FromSeconds(2));
        
        // //assert
        Assert.NotNull(productTable.Instance.products);
        Assert.Equal(mockProducts.Count, productTable.Instance.products.Count);
    }
    /// <summary>
    /// User enters search term, FilteredProducts gets updated based on search.
    /// Only match products in rendered table.
    /// </summary>
    [Fact]
    public async void ProductTable_Filtering_Should_Filter_Products()
    {
        
        //arrange
        var mockProducts = new List<BasicVareDTO>
        {
            new() { ItemDescription = "Pants", SuggestedRetailPrice = 10, ItemGroupName = "Category 1", Quantity = 5, EAN = "12345678", Currency = "DKK"},
            new() { ItemDescription = "Shirt", SuggestedRetailPrice = 15, ItemGroupName = "Category 2", Quantity = 0, EAN = "87654321", Currency = "DKK"}
        };
        _mockProductService.Setup(service => service.GetAllBasicVare())
            .ReturnsAsync(mockProducts);
        
        var productTable = RenderComponent<ProductTable>();
        var searchInput = productTable.Find("input");

        //act
        productTable.WaitForState(() => productTable.Instance.products.Count > 0, TimeSpan.FromSeconds(2));
        
        // //assert we have the starting 2 products
        Assert.NotNull(productTable.Instance.products);
        Assert.Equal(mockProducts.Count, productTable.Instance.products.Count);
        // Now that we've verified things are correct in setup, lets check on the Input affecting filtering.
        //Act
        searchInput.Change("Pants");
        productTable.Render();
        // Assert things have changed.
        Assert.Contains("Pants", productTable.Markup);
        Assert.Single(productTable.Instance.PaginatedProducts);
    }
    
    
}