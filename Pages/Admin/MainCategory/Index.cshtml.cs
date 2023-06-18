using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace ForumFörFöräldrar.Pages.Admin.MainCategory
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Data.ForumFörFöräldrarContext _context;

        public IndexModel(Data.ForumFörFöräldrarContext context)
        {
            _context = context;
        }
        public IList<Models.ForumMainCategory> ForumMainCategory { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ForumMainCategory = await DAL.ForumMainCategoryManager.GetAllForumMainCategories();
        }

    }
}