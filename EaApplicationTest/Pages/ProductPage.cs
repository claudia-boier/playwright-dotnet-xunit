using EaApplicationTest.Models;
using Microsoft.Playwright;

namespace EaApplicationTest.Pages;

public interface IProductPage
{
    Task ClickCreate();
    Task CreateProduct(Employee employee);
    Task CreateProductWithParameters(string name, decimal salary, int duration, string grade, string email);
}

public class ProductPage : IProductPage
{
    private readonly IPage _page;


    public ProductPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;


    private ILocator nameField => _page.GetByLabel("Name");

    private ILocator salaryField => _page.GetByLabel("Salary");

    private ILocator durationField => _page.GetByLabel("DurationWorked");

    private ILocator gradeField => _page.GetByRole(AriaRole.Combobox, new() { Name = "Grade" });

    private ILocator emailField => _page.GetByLabel("Email");
    private ILocator createButton => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });


    public async Task CreateProduct(Employee employee)
    {
        await nameField.FillAsync(employee.Name);
        await salaryField.FillAsync(employee.Salary.ToString());
        await durationField.FillAsync(employee.Duration.ToString());
        await gradeField.SelectOptionAsync(employee.Grade.ToString());
        await emailField.FillAsync(employee.Email);
    }

    public async Task CreateProductWithParameters(string name, decimal salary, int duration, string grade, string email)
    {
        await nameField.FillAsync(name);
        await salaryField.FillAsync(salary.ToString());
        await durationField.FillAsync(duration.ToString());
        await gradeField.SelectOptionAsync(grade);
        await emailField.FillAsync(email);
    }

    public async Task ClickCreate() => await createButton.ClickAsync();

}