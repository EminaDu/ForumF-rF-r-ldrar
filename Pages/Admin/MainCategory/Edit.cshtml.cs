using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Net;

namespace ForumFörFöräldrar.Pages.Admin.MainCategory
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly Data.ForumFörFöräldrarContext _context;
        public EditModel(Data.ForumFörFöräldrarContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Models.ForumMainCategory ForumMainCategory { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumMainCategory = await DAL.ForumMainCategoryManager.GetOneForumMainCategory(id.Value);
            if (forumMainCategory == null)
            {
                return NotFound();
            }

            ForumMainCategory = forumMainCategory;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            DAL.ForumMainCategoryManager forumMainCategoryManager = new();
            bool updateResult = await forumMainCategoryManager.UpdateForumMainCategory(ForumMainCategory);
            if (updateResult)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}