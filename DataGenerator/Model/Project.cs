namespace DataGenerator.Model;

internal class Project
{
    public int ProjectId { get; set; }

    public string Name { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int ClientId { get; set; }

    public Client Client { get; set; }

    public List<Asset> Assets { get; set; } = new();
}
