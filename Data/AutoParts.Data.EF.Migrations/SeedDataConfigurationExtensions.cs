namespace AutoParts.Data.EF.Migrations
{
    using System;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using Microsoft.Extensions.DependencyInjection;

    using Data.Model.SeedData;
    using Data.Model.Entities;

    using Data.EF;

    public static class SeedDataConfigurationExtensions
    {
        public static void SeedData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<DatabaseContext>();

            var seedFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SeedDataConstants.SeedDataFolderName);
            var seedFiles = Directory.GetFiles(seedFolder);

            SeedAutoPartsCatalogs(context, seedFiles);

            context.SaveChanges();
        }

        private static void SeedAutoPartsCatalogs(DatabaseContext context, string[] seedFiles)
        {
            if (context.AutoPartsCatalogGroups.Any())
            {
                return;
            }

            var entities = ReadSeedDataFromFile<AutoPartsCatalogGroup>(seedFiles, SeedDataConstants.AutoPartsCatalogGroupsFileName);

            if (entities == null || !entities.Any())
            {
                return;
            }

            context.AutoPartsCatalogGroups.AddRange(entities);
        }

        public static void SeedCountries(DatabaseContext context, string[] seedFiles)
        {
            if (context.Countries.Any())
            {
                return;
            }

            var entities = ReadSeedDataFromFile<Country>(seedFiles, SeedDataConstants.CountriesFileName);

            if (entities == null || !entities.Any())
            {
                return;
            }

            context.Countries.AddRange(entities);
        }

        private static TEntity[] ReadSeedDataFromFile<TEntity>(string[] seedFiles, string fileName)
            where TEntity : class
        {
            var filePath = seedFiles.FirstOrDefault(file => Path.GetFileName(file) == fileName);
            var json = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<TEntity[]>(json);
        }
    }
}
