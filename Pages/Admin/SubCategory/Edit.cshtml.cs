using ForumFörFöräldrar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace ForumFörFöräldrar.Pages.Admin.SubCategory
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
        public ForumSubCategory ForumSubCategory { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ForumSubCategory == null)
            {
                return NotFound();
            }
            var forumSubCategory = await _context.ForumSubCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (forumSubCategory == null)
            {
                return NotFound();
            }
            ForumSubCategory = forumSubCategory;
            ViewData["ForumMaincategoryId"] = new SelectList(_context.ForumMainCategory, "Id", "Id");
            return Page();
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(ForumSubCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubforumExists(ForumSubCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }
        private bool SubforumExists(int id)
        {
            return (_context.ForumSubCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}