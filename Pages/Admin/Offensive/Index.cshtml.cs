using ForumFörFöräldrar.Areas.Identity.Data;
using ForumFörFöräldrar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ForumFörFöräldrar.Pages.Admin.Offensive
{
    public class IndexModel : PageModel
    {
        private readonly Data.ForumFörFöräldrarContext _context;
        public UserManager<ForumFörFöräldrarUserT> _userManager;
        public List<Models.Post> ReportedPosts { get; set; }
        public List<Models.Comment> ReportedComments { get; set; }
        public List<ForumFörFöräldrarUserT> Users { get; set; }
        public IndexModel(Data.ForumFörFöräldrarContext context, UserManager<ForumFörFöräldrarUserT> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(int changeId, int deleteId)
        {
            ReportedPosts = _context.Post.Where(p => p.Offensive == true).ToList();
            Users = await _userManager.Users.ToListAsync();
            if (deleteId != 0)
            {
                Models.Post post = await _context.Post.FindAsync(deleteId);
                if (post != null)
                {
                    _context.Post.Remove(post);
                    await _context.SaveChangesAsync();

                    return Redirect("~/");
                }
            }
            if (changeId != 0)
            {
                Post offensivePost = await _context.Post.FindAsync(changeId);

                if (offensivePost != null)
                {
                    offensivePost.Offensive = false;
                    await _context.SaveChangesAsync();
                    return Redirect("~/");
                }
            }
            return Page();
        }
    }
}