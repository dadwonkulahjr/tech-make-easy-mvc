using IamtuseTechMakeEasyWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;



namespace IamtuseTechMakeEasyWeb.Controllers
{
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int categoryItemId)
        {
            Entities.Content content = await (from myContent in _context.Contents
                                              where myContent.CategoryItem.Id == categoryItemId
                                              select new Entities.Content
                                              {
                                                  Title = myContent.Title,
                                                  VideoLink = myContent.VideoLink,
                                                  HtmlContent = myContent.HtmlContent
                                              }
                                              ).FirstOrDefaultAsync();
            //Entities.Content content = await _context.Contents
            //                                   .Where(c => c.CategoryItem.Id == categoryItemId)
            //                                   .Select(x => new Entities.Content
            //                                   {
            //                                       Title = x.Title,
            //                                       VideoLink = x.VideoLink,
            //                                       HtmlContent = x.HtmlContent
            //                                   }).FirstOrDefaultAsync();



            return View(content);
        }
    }
}
