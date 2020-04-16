using Kentico.Kontent.Management;
using Kentico.Kontent.Management.Models.Items;
using Kentico.Kontent.Management.Models.StronglyTyped;
using KenticoKontentModels;
using MLNET_kontent_taxonomy_app.Configuration;
using System.Threading.Tasks;

namespace MLNET_kontent_taxonomy_app
{
    class TaxonomyImporter  
    {
        ManagementClient client;

        public TaxonomyImporter(KontentKeys keys)
        {
            ManagementOptions options = new ManagementOptions
            {
                ProjectId = keys.ProjectId,
                ApiKey = keys.ManagementApiKey,
            };

            // Initializes an instance of the ManagementClient client
            client = new ManagementClient(options);
        }

        public async Task<string> UpsertTaxonomy(Movie movie, string listing_prediction)
        {

            MovieImport stronglyTypedElements = new MovieImport
            {
                ListedIn = new[] { TaxonomyTermIdentifier.ByCodename(listing_prediction) }
            };

            // Specifies the content item and the language variant
            ContentItemIdentifier itemIdentifier = ContentItemIdentifier.ByCodename(movie.System.Codename);
            LanguageIdentifier languageIdentifier = LanguageIdentifier.ByCodename(movie.System.Language);
            ContentItemVariantIdentifier identifier = new ContentItemVariantIdentifier(itemIdentifier, languageIdentifier);

            // Upserts a language variant of your content item
            ContentItemVariantModel<MovieImport> response = await client.UpsertContentItemVariantAsync(identifier, stronglyTypedElements);

            return response.Elements.Title + " updated.";
        }
    }
}
