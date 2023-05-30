using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            /*if(!context.ProductBrands.Any()) //on vérifie si il y a déjà des ProductBrands
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }            
            if(!context.ProductTypes.Any()) //on vérifie si il y a déjà des ProductTypes
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }
            if(!context.Products.Any()) //on vérifie si il y a déjà des Products, important d'être après les 2 autres 
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }*/
            AddRange<ProductBrand>(context.ProductBrands, "brands.json");
            AddRange<ProductType>(context.ProductTypes, "types.json");
            AddRange<Product>(context.Products, "products.json");
            AddRange<DeliveryMethod>(context.DeliveryMethods, "delivery.json");

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }            
        private static void AddRange<TEntity>(DbSet<TEntity> dbSet, string file) where TEntity : class
        {
             if(!dbSet.Any()) //on vérifie si il y a déjà quelque chose
            {
                var data = File.ReadAllText("../Infrastructure/Data/SeedData/"+ file);
                var jsonDeserialized = JsonSerializer.Deserialize<List<TEntity>>(data);
                dbSet.AddRange(jsonDeserialized);
            }
        }
    }
}