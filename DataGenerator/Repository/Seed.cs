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
                             .RuleFor(a => a.Name, f => f.Lorem.Word())
                             .RuleFor(a => a.Asset_Platform, f => f.PickRandom<Platform>())
                             .RuleFor(d => d.Description, f => f.Lorem.Sentences(1))
                             .RuleFor(a => a.Password, _ => Guid.NewGuid().ToString());

        #endregion

        #region PROJECT_GENERATOR
        int projectId = 1;

        var projectGenerator = new Bogus.Faker<Project>()
                                        .RuleFor(p => p.ProjectId, _ => projectId++)
                                        .RuleFor(p => p.Name, f => f.Lorem.Word())
                                        .RuleFor(p => p.StartDate, f => f.Date.Past(1))
                                        .RuleFor(p => p.EndDate, (f, p) => f.Date.Future(refDate: p.StartDate))
                                        .RuleFor(p => p.Assets, (f, p) =>
                                        {
                                            assetGenerator.RuleFor(a => a.ProjectId, _ => p.ProjectId);

                                            List<Asset> assets = assetGenerator.Generate(2);

                                            Assets.AddRange(assets);

                                            return assets;
                                        });

        Projects.AddRange(projectGenerator.Generate(4));
        #endregion

        #region CLIENT_GENERATOR
        int clientId = 1;

        var clientGenerator = new Bogus.Faker<Client>()
                                       .RuleFor(c => c.ClientId, _ => clientId++)
                                       .RuleFor(c => c.Name, f => f.Name.FullName())
                                       .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Name))
                                       .RuleFor(c => c.CompanyName, f => f.Company.CompanyName() + f.Company.CompanySuffix())
                                       .RuleFor(c => c.Projects, (f, c) =>
                                       {
                                           //create a rule to create our Project entity's foreign key ClientId
                                           projectGenerator.RuleFor(p => p.ClientId, _ => c.ClientId);
                                           
                                           //generate 5 projects for this Client instance
                                           List<Project> projects = projectGenerator.Generate(5);

                                           //add the generated projects to our list of projects
                                           projects.AddRange(projects);
                                           
                                           return projects;
                                       });

        Clients.AddRange(clientGenerator.Generate(5));
        #endregion  
    }
}
