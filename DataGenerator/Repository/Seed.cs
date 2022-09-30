using Bogus;
using DataGenerator.Model;

namespace DataGenerator.Repository;

internal class Seed
{
    public List<Asset> Assets { get; } = new List<Asset>();
    public List<Project> Projects { get; } = new List<Project>();

    public List<Client> Clients { get; } = new List<Client>();

    public Seed()
	{
        #region ASSETS
        int assetId = 1;

        var asset = new Bogus.Faker<Asset>()
                             .RuleFor(a => a.AssetId, (f, a) => assetId++)
                             .RuleFor(x => x.Name, (x, y) => x.Lorem.Word())
                             .RuleFor(a => a.Asset_Platform, (f, a) => f.PickRandom<Platform>())
                             .RuleFor(d => d.Description, (f, d) => f.Lorem.Sentences(1))
                             .RuleFor(a => a.Password, (f, a) => a.Password = Guid.NewGuid().ToString());
        #endregion
        Assets.AddRange(asset.Generate(5));
    }
}
