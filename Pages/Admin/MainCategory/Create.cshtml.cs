using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Net;

namespace ForumFörFöräldrar.Pages.Admin.MainCategory
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
            private readonly ForumFörFöräldrar.Data.ForumFörFöräldrarContext _context;
            public CreateModel(ForumFörFöräldrar.Data.ForumFörFöräldrarContext context)
            {
                _context = context;
            }
            public IActionResult OnGet()
            {
                return Page();
            }
            [BindProperty]
            public Models.ForumMainCategory ForumMainCategory { get; set; } = default!;
            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid || ForumMainCategory == null)
                {
                    return Page();
                }
                DAL.ForumMainCategoryManager forumMainCategoryManager = new();
                bool createResult = await forumMainCategoryManager.CreateForumMainCategory(ForumMainCategory);
                if (createResult)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                //ovdje treba prikazat error na trenutnoj stranici, a ne prikazati ovakav error
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
        }
    }