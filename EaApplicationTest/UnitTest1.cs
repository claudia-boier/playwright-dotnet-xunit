using AutoFixture.Xunit2;
using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using EaFramework.Config;
using EaFramework.Driver;
using Microsoft.Playwright;

namespace EaApplicationTest;

public class UnitTest1 : IClassFixture<PlaywrightDriverInitializer>
{
    private readonly PlaywrightDriver _playwrightDriver;
    private readonly PlaywrightDriverInitializer _playwrightDriverInitializer;
    private readonly TestSettings _testSettings;

    public UnitTest1(PlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _testSettings = ConfigReader.ReadConfig();
        _playwrightDriverInitializer = playwrightDriverInitializer;
        _playwrightDriver = new PlaywrightDriver(_testSettings, _playwrightDriverInitializer);
    }

    [Fact]
    public async Task Test1()
    {
        var page = await _playwrightDriver.Page;

        await page.GotoAsync(_testSettings.ApplicationUrl);

        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();

        await page.GetByLabel("UserName").FillAsync("admin");

        await page.GetByLabel("Password").FillAsync("password");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" }).ClickAsync();
    }

    [Theory]
    // Employee creation test data
    [InlineData("Alice Smith", 90000, 2, "Senior", "alice.smith@example.com")]
    [InlineData("Bob Johnson", 65000, 3, "Mid", "bob.johnson@example.com")]
    [InlineData("Carol White", 105000, 4, "Lead", "carol.white@example.com")]
    [InlineData("David Brown", 35000, 1, "Junior", "david.brown@example.com")]
    public async Task Test_WithInlineData(string name, decimal salary, int duration, string grade, string email)
    {
        var page = await _playwrightDriver.Page;
        
        await page.GotoAsync(_testSettings.ApplicationUrl);

        ProductListPage productListPage = new ProductListPage(page);
        ProductPage productPage = new ProductPage(page);
        
        
        await productPage.CreateProductWithParameters(name, salary, duration, grade, email);
        await productPage.ClickCreate();
        
        await productListPage.ClickProductFromList(name);

        
        var element = productListPage.IsProductCreated(name);
        await Assertions.Expect(element).ToBeVisibleAsync();
    }
    
    
    [Fact]
    public async Task TestWithConcreteTypes()
    {
        var page = await _playwrightDriver.Page;

        var employee = new Employee()
        {
            Name = "John Doe",
            Salary = 75000,
            Duration = 5,
            Grade = Grade.Senior,
            Email = "john.doe@example.com"  
        };
        
        await page.GotoAsync(_testSettings.ApplicationUrl);

        ProductListPage productListPage = new ProductListPage(page);
        ProductPage productPage = new ProductPage(page);
        
        
        await productListPage.CreateProductAsync();
        await productPage.CreateProduct(employee);
        await productPage.ClickCreate();
        
        await productListPage.ClickProductFromList(employee.Name);

        
        var element = productListPage.IsProductCreated(employee.Name);
        await Assertions.Expect(element).ToBeVisibleAsync();
    }
    
    [Theory, AutoData]
    public async Task TestWithAutoFixtureData(Employee employee)
    {
        var page = await _playwrightDriver.Page;

        await page.GotoAsync("http://localhost:8000/");

        ProductListPage productListPage = new ProductListPage(page);
        ProductPage productPage = new ProductPage(page);
        
        
        await productListPage.CreateProductAsync();
        await productPage.CreateProduct(employee);
        await productPage.ClickCreate();
        
        await productListPage.ClickProductFromList(employee.Name);

        
        var element = productListPage.IsProductCreated(employee.Name);
        await Assertions.Expect(element).ToBeVisibleAsync();
    }
}