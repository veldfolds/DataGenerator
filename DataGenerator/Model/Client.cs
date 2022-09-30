namespace DataGenerator.Model;

internal class Client
{
    public int ClientId { get; set; }

    public string Name { get; set; }

    public string CompanyName { get; set; }

    public string Email { get; set; }

    public List<Project> Projects { get; set; } = new();

}
