using IamtuseTechMakeEasyWeb.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace IamtuseTechMakeEasyWeb.Extensions
{
    public static class ConvertExtensions
    {
        public static List<SelectListItem> ConvertToSelectListItem<T>(this IEnumerable<T> collection, int selectedValue) where T : IPrimaryProperties
        {
            return collection.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString(),
                Selected = (x.Id == selectedValue)
            }).ToList();
        }
    }
}
