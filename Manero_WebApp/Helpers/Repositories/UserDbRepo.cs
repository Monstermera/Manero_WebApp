using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories.MainRepo;
using Manero_WebApp.Models.Entities;

namespace Manero_WebApp.Helpers.Repositories;

public class UserDbRepo : MainDbRepo<UserEntity>
{
    public UserDbRepo(DataContext db) : base(db)
    {
    }
}
