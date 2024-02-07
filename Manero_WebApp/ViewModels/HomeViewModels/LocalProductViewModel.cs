using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.ViewModels.HomeViewModels;

public class LocalProductViewModel
{
    public IEnumerable<ProductModel> Products { get; set; }
    public int ArticleNumber { get; set; }


}
