using KenticoKontentModels;
using MLNET_kontent_taxonomy_appML.Model;
using System;
using System.Linq;

namespace MLNET_kontent_taxonomy_app
{
    class TaxonomyPredictor
    {
        public string GetTaxonomy(Movie movie)
        {
            // Add input data
            var input = new ModelInput();

            input.Title = movie.Title;
            input.Rating = movie.Rating.ToList().First().Name;
            input.Description = movie.Description;

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            Console.WriteLine("Listing best match: " + result.Prediction);

            //formatting to meet Kontent codename requirements 
            //ex: Children & Family Movies => children___family_movies
            var formatted_prediction = result.Prediction.Replace(" ", "_").Replace("&", "_").ToLower();

            return formatted_prediction;
        }
    }
}
