using EaApplicationTest.Models;
using Microsoft.Playwright;

namespace EaApplicationTest.Pages;

public interface IProductListPage
{
    Task ClickProductFromList(string name, string link);
    Task CreateProductAsync();
    ILocator IsProductCreated(string employee);
    Task Login();
}

public class ProductListPage : IProductListPage
{
    private readonly IPage _page;

    public ProductListPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

    private ILocator _lnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Employee List" });
    private ILocator _lnkCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create New" });
    public async Task CreateProductAsync()
    {
        await _lnkProductList.ClickAsync();
        await _lnkCreate.ClickAsync();
    }

    public async Task Login()
    {
        await _page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await _page.GetByLabel("UserName").FillAsync("admin");
        await _page.GetByLabel("Password").FillAsync("password");
        await _page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
    }

    public async Task ClickProductFromList(string name, string link)
    {
        await _page.GetByRole(AriaRole.Row, new() { Name = name })
            .GetByRole(AriaRole.Link, new() { Name = link }).ClickAsync();
    }

    public ILocator IsProductCreated(string employee)
    {
        return _page.GetByText(employee);
    }

}