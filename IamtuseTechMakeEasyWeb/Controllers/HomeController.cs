using IamtuseTechMakeEasyWeb.Data;
using IamtuseTechMakeEasyWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IamtuseTechMakeEasyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext applicationDbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        private  IEnumerable<GroupedCategoryItemsByCategoryModel> GetGroupedCategoryItemsByCategories(
            IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModels)
        {
            return from categoryItem in categoryItemDetailsModels
                   group categoryItem by categoryItem.CategoryId into g
                   select new GroupedCategoryItemsByCategoryModel
                   {
                       Id = g.Key,
                       Title = g.Select(x => x.CategoryTitle).FirstOrDefault(),
                       Items = g
                   };
                         
        }
        private async Task<IEnumerable<CategoryItemDetailsModel>> GetCategoryItemsDetailsForUser(string userId)
        {
            return await (from catItem in _applicationDbContext.CategoryItems
                          join category in _applicationDbContext.Categories
                          on catItem.CategoryId equals category.Id
                          join content in _applicationDbContext.Contents
                          on catItem.Id equals content.CategoryItem.Id
                          join userCat in _applicationDbContext.UserCategories
                          on category.Id equals userCat.CategoryId
                          join mediaType in _applicationDbContext.MediaTypes
                          on catItem.MediaTypeId equals mediaType.Id
                          where userCat.UserId == userId
                          select new CategoryItemDetailsModel
                          {
                              CategoryId = category.Id,
                              CategoryTitle = category.Title,
                              CategoryItemId = catItem.Id,
                              CategoryItemTitle = catItem.Title,
                              CategoryItemDescription = catItem.Description,
                              MediaImagePath = mediaType.ThumbnailImagePath
                          }).ToListAsync();
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModels = null;
            IEnumerable<GroupedCategoryItemsByCategoryModel> groupedCategoriesModels = null;

            CategoryDetailsModel model = new();

            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    categoryItemDetailsModels = await GetCategoryItemsDetailsForUser(user.Id);
                    groupedCategoriesModels = GetGroupedCategoryItemsByCategories(categoryItemDetailsModels);

                    model.GroupedCategoryItemsByCategoryModels = groupedCategoriesModels;
                }

            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
