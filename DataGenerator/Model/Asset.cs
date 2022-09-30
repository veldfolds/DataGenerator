
namespace DataGenerator.Model;

internal class Asset
{
    
        public int AssetId { get; set; }

        public string Name { get; set; }

        public Platform Asset_Platform { get; set; }

        public string? Description { get; set; }

        public string Password { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }


}