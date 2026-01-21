namespace EaApplicationTest.Models;

public class Employee
{
    public string? Name { get; set; }
    public decimal Salary { get; set; }
    public int Duration { get; set; }
    public Grade Grade { get; set; }

    public string? Email { get; set; }

}

public enum Grade
{
    Junior,
    Middle,
    Senior,
    CLevel
}
