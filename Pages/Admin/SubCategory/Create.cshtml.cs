using ForumFörFöräldrar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Net;

namespace ForumFörFöräldrar.Pages.Admin.SubCategory
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Data.ForumFörFöräldrarContext _context;
        public CreateModel(Data.ForumFörFöräldrarContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            ViewData["ForumMainCategoryId"] = new SelectList(_context.ForumMainCategory, "Id", "Name");
            return Page();
        }
        [BindProperty]
        public ForumSubCategory ForumSubCategory { get; set; } = default!;
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ForumSubCategory == null || ForumSubCategory == null)
            {
                return Page();
            }
            _context.ForumSubCategory.Add(ForumSubCategory);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}