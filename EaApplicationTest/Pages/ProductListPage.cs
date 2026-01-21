using Microsoft.Playwright;

namespace EaApplicationTest.Pages;

public class ProductListPage
{
    private readonly IPage _page;

    public ProductListPage(IPage page)
    {
        _page = page;
    }

    private ILocator _lnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });
    private ILocator _lnkCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });
    public async Task CreateProductAsync()
    {
        await _lnkProductList.ClickAsync();
        await _lnkCreate.ClickAsync();
    }

    public async Task Login()
    {
        await _page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();
        await _page.GetByLabel("UserName").FillAsync("admin");
        await _page.GetByLabel("Password").FillAsync("password");
        await _page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
    }

    public async Task ClickProductFromList(string name)
    {
        await _page.GetByRole(AriaRole.Row, new() { Name = name })
            .GetByRole(AriaRole.Link, new() { Name = "Details" }).ClickAsync();
    }
    
    public ILocator IsProductCreated(string product)
    {
        return _page.GetByText(product, new() { Exact = true });
    }

}