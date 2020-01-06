using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using LinFx.Application.Abstractions;

namespace Mall.Web.Models.CatalogViewModels
{
    public class IndexViewModel
    {
        public IPagedResult<CatalogItem> CatalogItems { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public int? BrandFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
    }
}
