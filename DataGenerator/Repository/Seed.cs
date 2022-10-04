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
        #region ASSET_GENERATOR
        int assetId = 1;

        var assetGenerator = new Bogus.Faker<Asset>()
                                      .RuleFor(a => a.AssetId, _ => assetId++)
                                      .RuleFor(a => a.Password, _ => Guid.NewGuid().ToString())
                                      .RuleFor(a => a.Name, f => f.Lorem.Word())
                                      .RuleFor(a => a.Description, f => f.Lorem.Sentences(1))
                                      .RuleFor(a => a.Asset_Platform, f => f.PickRandom<Platform>())
                                      #region FOREIGN_KEYS
                                      .RuleFor(a => a.ProjectId, f => f.PickRandom<int>(Projects.Select(p => p.ProjectId)));
                                      #endregion

        #endregion

        #region PROJECT_GENERATOR
        int projectId = 1;

        var projectGenerator = new Bogus.Faker<Project>()
                                        .RuleFor(p => p.ProjectId, _ => projectId++)
                                        .RuleFor(p => p.Name, f => f.Lorem.Word())
                                        .RuleFor(p => p.StartDate, f => f.Date.Past(1))
                                        .RuleFor(p => p.EndDate, (f, p) => f.Date.Future(refDate: p.StartDate))
                                        #region FOREIGN_KEYS
                                        .RuleFor(p => p.ClientId, f => f.PickRandom<int>(Clients.Select(c => c.ClientId)));
                                        #endregion  

        #endregion

        #region CLIENT_GENERATOR
        int clientId = 1;

        var clientGenerator = new Bogus.Faker<Client>()
                                       .RuleFor(c => c.ClientId, _ => clientId++)
                                       .RuleFor(c => c.Name, f => f.Name.FullName())
                                       .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Name))
                                       .RuleFor(c => c.CompanyName, f => f.Company.CompanyName() + f.Company.CompanySuffix());
                                       
        //Generating clients
        Clients.AddRange(clientGenerator.Generate(5));
        //Generating projects
        Projects.AddRange(projectGenerator.Generate(10));
        //Generating assets
        Assets.AddRange(assetGenerator.Generate(15));

        #endregion  
    }
}
