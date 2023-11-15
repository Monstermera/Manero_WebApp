using Manero_WebApp.Models.Entities;
using System.Linq.Expressions;

namespace Manero_WebApp.Helpers.Services.UserServices;

public interface ICheckIfUserExistsService
{
    Task<bool> UserExistsAsync(Expression<Func<UserEntity, bool>> expression);
}
