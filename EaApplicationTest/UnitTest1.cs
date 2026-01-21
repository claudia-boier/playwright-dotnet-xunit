using AutoFixture.Xunit3;
using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using EaFramework.Config;
using EaFramework.Driver;
using Microsoft.Playwright;
using Xunit;
using Xunit.DependencyInjection;

namespace EaApplicationTest;

public class UnitTest1 
{
    private readonly TestSettings _testSettings;
    private readonly IProductListPage _productListPage;
    private readonly IProductPage _productPage;
    private readonly IPlaywrightDriver _playwrightDriver;

    public UnitTest1(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IProductListPage productListPage, IProductPage productPage)
    {
        _testSettings = testSettings;
        _productListPage = productListPage;
        _productPage = productPage;
        _playwrightDriver = playwrightDriver;
    }

    
    [Theory, AutoData]
    public async Task TestWithAutoFixtureData(Employee employee)
    {
        var page = await _playwrightDriver.Page;

        await page.GotoAsync(_testSettings.ApplicationUrl);
        
        
        await _productListPage.Login();
        await _productListPage.CreateProductAsync();
        await _productPage.CreateProduct(employee);
        await _productPage.ClickCreate();
        
        await _productListPage.ClickProductFromList(employee.Name, "Benefits");

        var element = _productListPage.IsProductCreated(employee.Name);
        await Assertions.Expect(element).ToBeVisibleAsync();
    }
}