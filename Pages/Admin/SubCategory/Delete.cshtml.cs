using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumFörFöräldrar.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using ForumFörFöräldrar.Models;

namespace ForumFörFöräldrar.Pages.Admin.SubCategory
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly Data.ForumFörFöräldrarContext  _context;
        public DeleteModel(Data.ForumFörFöräldrarContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ForumSubCategory ForumSubCategory { get; set; } = default;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ForumSubCategory == null)
            {
                return NotFound();
            }
            var forumsubCategory = await _context.ForumSubCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (forumsubCategory == null)
            {
                return NotFound();
            }
            else
            {
                ForumSubCategory = forumsubCategory;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ForumSubCategory == null)
            {
                return NotFound();
            }
            var forumSubCategory = await _context.ForumSubCategory.FindAsync(id);

            if (forumSubCategory != null)
            {
                ForumSubCategory = forumSubCategory;
                _context.ForumSubCategory.Remove(ForumSubCategory);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}