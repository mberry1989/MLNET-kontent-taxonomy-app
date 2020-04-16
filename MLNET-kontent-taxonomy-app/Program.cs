using System;
using System.Linq;
using KenticoKontentModels;
using Microsoft.Extensions.Configuration;
using MLNET_kontent_taxonomy_app.Configuration;

namespace MLNET_kontent_taxonomy_app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting program...");


            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            var keys = new KontentKeys();

            ConfigurationBinder.Bind(configuration.GetSection("KontentKeys"), keys);

            MovieListing movieListing = new MovieListing(keys);
            TaxonomyPredictor predictor = new TaxonomyPredictor();
            TaxonomyImporter importer = new TaxonomyImporter(keys);

            var movies = movieListing.GetMovies();

            foreach (Movie movie in movies.Result.Items)
            {
                if (movie.ListedIn.Count() < 1)
                {
                    string formatted_prediction = predictor.GetTaxonomy(movie);
                    var upsertResponse = importer.UpsertTaxonomy(movie, formatted_prediction).Result;

                    Console.WriteLine(upsertResponse);
                }
            }

            Console.WriteLine("Program finished.");
        }        
    }
}
