using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentModels;
using MLNET_kontent_taxonomy_app.Configuration;
using System.Threading.Tasks;

namespace MLNET_kontent_taxonomy_app
{
    class MovieListing
    {
        IDeliveryClient client;

        public MovieListing(KontentKeys keys)
        {
            client = DeliveryClientBuilder
                .WithOptions(builder => builder
                .WithProjectId(keys.ProjectId)
                .UsePreviewApi(keys.PreviewApiKey)
                .Build())
                .Build();
        }        

        public async Task<DeliveryItemListingResponse<Movie>> GetMovies()
        {
            DeliveryItemListingResponse<Movie> response = await client.GetItemsAsync<Movie>(
                new EqualsFilter("system.type", "movie"),
                new ElementsParameter("title", "rating", "description", "listed_in")
                );

            return response;
        }        
    }
}
