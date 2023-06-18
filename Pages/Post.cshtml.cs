using ForumFörFöräldrar.Areas.Identity.Data;
using ForumFörFöräldrar.Data;
using ForumFörFöräldrar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ForumFörFöräldrar.Pages
{
    public class PostModel : PageModel
    {

        private readonly ForumFörFöräldrarContext _context;
        private readonly IConfiguration Configuration;

        public PostModel(ForumFörFöräldrarContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public List<ForumFörFöräldrarUserT> Users { get; set; }
        public Models.ForumSubCategory ForumSubCategory { get; set; }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        [BindProperty]
        public Models.Post Post { get; set; }
        [BindProperty]
        public IFormFile UploadedImage { get; set; }
        public PaginatedList<Models.Post>Posts { get; set; }
        private static int _id;
        public async Task OnGetAsync(int id, int? pageIndex)
        {
            if (id != 0)
            {
                _id = id;
            }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
            ForumSubCategory = await _context.ForumSubCategory.Include(x => x.Posts).ThenInclude(x => x.Comments).FirstOrDefaultAsync(x => x.Id == _id);
            Users = await _context.Users.ToListAsync();

            var pageSize = Configuration.GetValue("PageSize", 10);
            Posts = await PaginatedList<Models.Post>.CreateAsync(
                _context.Post.OrderByDescending(x => x.Date).Where(x => x.ForumSubCategoryId == _id)
                , pageIndex ?? 1, pageSize);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Post.Text == null)
            {
                Post.Text = "";
            }
            if (Post.Header == null)
            {
                Post.Header = "Ny tråd";
            }
            _context.Add(Post);
            _context.SaveChanges();
            int id = await _context.Post.MaxAsync(p => p.Id);

            string url = "./Comment?id=" + id.ToString();

            return Redirect(url);
        }
    }
}