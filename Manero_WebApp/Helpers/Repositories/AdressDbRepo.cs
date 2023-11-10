using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories.MainRepo;
using Manero_WebApp.Models.Entities;

namespace Manero_WebApp.Helpers.Repositories;

public class AdressDbRepo : MainDbRepo<AdressEntity>
{
    public AdressDbRepo(DataContext db) : base(db)
    {
    }
}
