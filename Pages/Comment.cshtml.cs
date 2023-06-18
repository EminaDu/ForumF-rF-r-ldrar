using ForumFörFöräldrar.Data;
using ForumFörFöräldrar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.Design;
using System.Drawing.Printing;
using System.Security.Policy;
using ForumFörFöräldrar.Areas.Identity.Data;
using Intercom.Core;

namespace ForumFörFöräldrar.Pages
{
	public class CommentModel : PageModel
	{
		private readonly ForumFörFöräldrarContext _context;
		private readonly UserManager<ForumFörFöräldrarUserT> _userManager;
		private readonly IConfiguration Configuration;
		public CommentModel(ForumFörFöräldrarContext context, UserManager<ForumFörFöräldrarUserT> userManager, IConfiguration configuration)
		{
			_context = context;
			_userManager = userManager;
			Configuration = configuration;
		}
		public Models.Post Post { get; set; }
		[BindProperty]
		public Models.Report Report { get; set; }
		public List<ForumFörFöräldrarUserT> AllUsers { get; set; }
		public ForumFörFöräldrarUserT MyUser { get; set; }
		[BindProperty]
		public string EditText { get; set; }
		[BindProperty]
		public string EditPostText { get; set; }
		public bool IsAdmin { get; set; }
		private static int _id;
		public PaginatedList<Models.Comment> Comments { get; set; }

		public async Task<IActionResult> OnGetAsync(int id, string userid, int postid,
			int deletepostid, bool deletebool, int unflagpostid, int? pageIndex)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser != null)
			{
				IsAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
			}
			if (id != 0)
			{
				_id = id;
			}
			var pageSize = Configuration.GetValue("PageSize", 10);
			Comments = await PaginatedList<Models.Comment>.CreateAsync(
				_context.Comment.Where(x => x.PostId == _id)
				, pageIndex ?? 1, pageSize);

			Post = await _context.Post.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == _id);
			AllUsers = await _context.Users.ToListAsync();
			MyUser = AllUsers.Where(x => x.Id == Post.UserId).SingleOrDefault();

			if (deletepostid != 0 && deletebool == true)
			{
				await DeletePostAsync(deletepostid, deletebool);
				return Redirect("./Post");
			}
			if (unflagpostid != 0)
			{
				string redirectUrl = await UnFlagAsync(unflagpostid);
				return Redirect(redirectUrl);
			}
			return Page();
		}
		public async Task<IActionResult> OnPostAsync(int editpostid, bool editbool, int reportpostid)
		{
			if (reportpostid != 0)
			{
				string redirectUrl = await ReportAsync(reportpostid);
				return Redirect(redirectUrl);
			}

			if (editpostid != 0 && editbool == true)
			{
				await EditPostAsync(editpostid, editbool);
			}
			string url = "./Comment?id=" + _id.ToString();
			return Redirect(url);
		}
		private async Task EditPostAsync(int editpostid, bool editbool)
		{
			Models.Post editPost = await _context.Post.FindAsync(editpostid);
			if (editPost != null && EditPostText != null)
			{
				editPost.Text = EditPostText;
				editbool = false;
				await _context.SaveChangesAsync();
			}
		}
		private async Task<string> ReportAsync(int reportpostid)
		{
			if (reportpostid != 0)
			{
				Report.PostId = reportpostid;
				_context.Post.FirstOrDefault(x => x.Id == reportpostid).Offensive = true;
				_context.Reports.Add(Report);
				await _context.SaveChangesAsync();
			}
			string url = "./Comment?id=" + _id.ToString();
			return url;
		}
		private async Task<string> UnFlagAsync(int unflagpostid)
		{
			if (unflagpostid != 0)
			{
				var post = await _context.Post.Where(p => p.Id == unflagpostid).FirstOrDefaultAsync();
				post.Offensive = false;
				var reportedPost = await _context.Reports.Where(r => r.PostId == unflagpostid).ToListAsync();

				_context.Reports.RemoveRange(reportedPost);
				await _context.SaveChangesAsync();
			}
			string url = "./Comment?id=" + _id.ToString();
			return url;
		}
		private async Task DeletePostAsync(int deletepostid, bool deletebool)
		{
			Models.Post deletePost = await _context.Post.FindAsync(deletepostid);
			List<Models.Report> deletePostReport = await _context.Reports.Where(x => x.PostId == deletepostid).ToListAsync();
			List<Models.Comment> reportedComments = await _context.Comment.Where(x => x.PostId == deletepostid && x.Offensive == true).ToListAsync();
			if (deletePost != null)
			{
				_context.Post.Remove(deletePost);
				if (deletePostReport != null)
				{
					_context.Reports.RemoveRange(deletePostReport);
				}
				await _context.SaveChangesAsync();
				deletebool = false;
			}
		}
	}
}