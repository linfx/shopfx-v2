using System.Threading.Tasks;
using Mall.Services;
using Mall.Web.Models.CatalogViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mall.Web.Controllers
{
    public class CatalogController : Controller
    {
        private ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int page = 1, int limit = 10, [FromQuery]string errorMsg = default)
        {
            var vm = new IndexViewModel
            {
                Brands = await _catalogService.GetBrands(),
                Types = await _catalogService.GetTypes(),
                CatalogItems = await _catalogService.GetCatalogItems(page, limit, brandFilterApplied, typesFilterApplied),
                BrandFilterApplied = brandFilterApplied ?? 0,
                TypesFilterApplied = typesFilterApplied ?? 0,
            };
            ViewBag.BasketInoperativeMsg = errorMsg;
            return View(vm);
        }
    }
}