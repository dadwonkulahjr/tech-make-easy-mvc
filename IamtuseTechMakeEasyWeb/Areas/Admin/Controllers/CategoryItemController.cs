using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IamtuseTechMakeEasyWeb.Data;
using IamtuseTechMakeEasyWeb.Entities;
using IamtuseTechMakeEasyWeb.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace IamtuseTechMakeEasyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoryItem
        public async Task<IActionResult> Index(int categoryId)
        {
            List<CategoryItem> categoryItems = await (from catItem in _context.CategoryItems
                                                      join contentItem in _context.Contents
                                                      on catItem.Id equals contentItem.CategoryItem.Id
                                                      into contentGroup
                                                      from subContent in contentGroup.DefaultIfEmpty()
                                                      where catItem.CategoryId == categoryId
                                                      select new CategoryItem()
                                                      {
                                                          Id = catItem.Id,
                                                          Title = catItem.Title,
                                                          Description = catItem.Description,
                                                          DateTimeItemReleased = catItem.DateTimeItemReleased,
                                                          CategoryId = categoryId,
                                                          MediaTypeId = catItem.MediaTypeId,
                                                          ContentId = (subContent != null) ? subContent.Id : 0
                                                      }).ToListAsync();








            ViewBag.CategoryId = categoryId;

            return View(categoryItems);
        }

        // GET: Admin/CategoryItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Create
        public async Task<IActionResult> Create(int categoryId)
        {
            List<MediaType> mediaTypes = await _context.MediaTypes.ToListAsync();
            CategoryItem categoryItem = new()
            {
                CategoryId = categoryId,
                MediaTypes = mediaTypes.ConvertToSelectListItem(0)
            };

            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DateTimeItemReleased,Description,MediaTypeId,CategoryId")] CategoryItem categoryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { categoryId = categoryItem.CategoryId });
            }
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<MediaType> mediaTypes = await _context.MediaTypes.ToListAsync();

            var categoryItem = await _context.CategoryItems.FindAsync(id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            categoryItem.MediaTypes = mediaTypes.ConvertToSelectListItem(categoryItem.MediaTypeId);

            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DateTimeItemReleased,Description,MediaTypeId,CategoryId")] CategoryItem categoryItem)
        {
            if (id != categoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryItemExists(categoryItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { categoryId = categoryItem.CategoryId });
            }
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryItem = await _context.CategoryItems.FindAsync(id);
            _context.CategoryItems.Remove(categoryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { categoryId = categoryItem.CategoryId });
        }

        private bool CategoryItemExists(int id)
        {
            return _context.CategoryItems.Any(e => e.Id == id);
        }
    }
}
