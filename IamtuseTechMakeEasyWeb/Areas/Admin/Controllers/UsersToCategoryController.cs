using IamtuseTechMakeEasyWeb.Areas.Admin.ViewModels;
using IamtuseTechMakeEasyWeb.Data;
using IamtuseTechMakeEasyWeb.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IamtuseTechMakeEasyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersToCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersToCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersForCategory(int categoryId)
        {
            UserCategoryListViewModel userCategoryListViewModel = new();

            var allUsers = await GetAllUsers();
            var selectedUserForCategory = await GetSavedSelectedUsersForCategory(categoryId);

            userCategoryListViewModel.Users = allUsers;
            userCategoryListViewModel.UsersSelected = selectedUserForCategory;

            return PartialView("_UserListViewPartial", userCategoryListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context
                                .Categories
                                .ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSelectedUsers([Bind("CategoryId,UsersSelected")] UserCategoryListViewModel model)
        {
            List<UserCategory> usersSelectedForCategoryToAdd = null;

            if(model.UsersSelected != null)
            {
                usersSelectedForCategoryToAdd = await GetUserCategoriesToAdd(model);
            }

            var usersSelectedForCategoryToDelete = await GetUserCategoriesToDelete(model.CategoryId);

            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.RemoveRange(usersSelectedForCategoryToDelete);
                    await _context.SaveChangesAsync();
                    if (usersSelectedForCategoryToAdd != null)
                    {
                        await _context.AddRangeAsync(usersSelectedForCategoryToAdd);
                        await _context.SaveChangesAsync();
                    }
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.DisposeAsync();
                }
            }

            model.Users = await GetAllUsers();
            return PartialView("_UserListViewPartial", model);
        }

        private async Task<List<UserViewModel>> GetAllUsers()
        {
            return await _context.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToListAsync();
        }

        private async Task<List<UserCategory>> GetUserCategoriesToDelete(int categoryId)
        {
            return await _context.UserCategories
                                    .Where(x => x.CategoryId == categoryId)
                                    .Select(x => new UserCategory
                                    {
                                        Id = x.Id,
                                        CategoryId = x.CategoryId,
                                        UserId = x.UserId
                                    }).ToListAsync();
        }

        private static async Task<List<UserCategory>> GetUserCategoriesToAdd(UserCategoryListViewModel model)
        {
            return await Task.FromResult(model.UsersSelected.Select(x => new UserCategory()
            {
                CategoryId = model.CategoryId,
                UserId = x.Id
            }).ToList());
        }

        private async Task<List<UserViewModel>> GetSavedSelectedUsersForCategory(int categoryId)
        {
            return await _context.UserCategories
                            .Where(u => u.CategoryId == categoryId)
                            .Select(x => new UserViewModel
                            {
                                Id = x.UserId
                            }).ToListAsync();
        }
    }
}
