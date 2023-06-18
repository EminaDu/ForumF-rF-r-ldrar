using ForumFörFöräldrar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ForumFörFöräldrar.Pages.Admin.SubCategory
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Data.ForumFörFöräldrarContext _context;
        public IndexModel(Data.ForumFörFöräldrarContext context)
        {
            _context = context;
        }
        public IList<ForumSubCategory> ForumSubCategories { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.ForumSubCategory != null)
            {
                ForumSubCategories = await _context.ForumSubCategory
                .Include(s => s.ForumMainCategory).ToListAsync();
            }
        }
    }
}