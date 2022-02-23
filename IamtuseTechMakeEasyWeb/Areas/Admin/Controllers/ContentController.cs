using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IamtuseTechMakeEasyWeb.Data;
using IamtuseTechMakeEasyWeb.Entities;
using Microsoft.AspNetCore.Authorization;

namespace IamtuseTechMakeEasyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int categoryId, int categoryItemId)
        {
            Content content = new()
            {
                CategoryId = categoryId,
                CatItemId = categoryItemId
            };
            return View(content);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HtmlContent,VideoLink,CatItemId,CategoryId")] Content content)
        {
            if (ModelState.IsValid)
            {
                content.CategoryItem = await _context.CategoryItems.FindAsync(content.CatItemId);
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "CategoryItem", new { categoryId = content.CategoryId });
            }
            return View(content);
        }


        public async Task<IActionResult> Edit(int categoryItemId, int categoryId)
        {
            if (categoryItemId == 0)
            {
                return NotFound();
            }

            var content = await _context.Contents.FirstOrDefaultAsync(c => c.CategoryItem.Id == categoryItemId);

            content.CategoryId = categoryId;
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HtmlContent,VideoLink,CategoryId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "CategoryItem", new {categoryId = content.CategoryId });
            }
            return View(content);
        }
        private bool ContentExists(int id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
