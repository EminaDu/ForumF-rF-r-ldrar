using ForumFörFöräldrar.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ForumFörFöräldrar.Pages
{
    public class ForumModel : PageModel
    {
        private readonly Data.ForumFörFöräldrarContext _context;

        public ForumModel(ForumFörFöräldrarContext context)
        {
            _context = context;
        }
        public List<Models.ForumMainCategory> ForumMainCategories { get; set; }
        public IList<Models.ForumSubCategory> ForumSubCategories { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? ChooseableTopics { get; set; }
        public SelectList? Topics { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            ForumMainCategories = await _context.ForumMainCategory.Include(x => x.ForumSubCategories).ThenInclude(x => x.Posts).ThenInclude(x => x.Comments).ToListAsync();

            var topics = from m in _context.ForumSubCategory
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                topics = topics.Where(t => t.Posts.Any(np => np.Header.ToLower().Contains(SearchString.ToLower())));
                Topics = new SelectList(await topics.SelectMany(t => t.Posts).Where(np => np.Header.Contains(SearchString.ToLower())).Select(np => np.Header).Distinct().ToListAsync());
            }
            else
            {
                Topics = new SelectList(await topics.SelectMany(t => t.Posts).Select(np => np.Header.ToLower()).Distinct().ToListAsync());
            }

            ForumSubCategories = await topics.ToListAsync();
        }
    }
}