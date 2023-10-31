using Manero_WebApp.Contexts;


namespace Manero_WebApp.Helpers.Services.ProductServices;

public class DeleteOneProductService
{
    #region constructors & private fields

    private readonly DataContext _context;


    public DeleteOneProductService(DataContext context)
    {
        _context = context;
    }

    #endregion

	public async Task<bool> DeleteAsync(Guid articleNumber)
	{
		try
		{
			var productEntity = await _context.Products.FindAsync(articleNumber);
			if (productEntity != null)
			{
				_context.Products.Remove(productEntity);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}
}
