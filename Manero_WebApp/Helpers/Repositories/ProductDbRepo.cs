using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories.MainRepo;
using Manero_WebApp.Models.Entities;

namespace Manero_WebApp.Helpers.Repositories;

public class ProductDbRepo : MainDbRepo<ProductEntity>
{
    public ProductDbRepo(DataContext db) : base(db)
    {
    }
}
