using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumFörFöräldrar.Areas.Identity.Data;
using ForumFörFöräldrar.Data;
using Microsoft.EntityFrameworkCore;

namespace ForumFörFöräldrar.Pages
{
    [Authorize]

    public class InboxModel : PageModel
    {

		private readonly ForumFörFöräldrarContext _context;

		private readonly UserManager<ForumFörFöräldrarUserT> _userManager;
		public InboxModel(ForumFörFöräldrarContext context, UserManager<ForumFörFöräldrarUserT> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		[BindProperty]
		public Models.Message NewMessage { get; set; }

		public List<Models.Message> Messages { get; set; }
		public List<ForumFörFöräldrarUserT> Users { get; set; }


		public async Task<IActionResult> OnGetAsync(int deletemessageid)
		{
			Users = await _userManager.Users.ToListAsync();
			var user = Users.FirstOrDefault(x => x.Email == User.Identity.Name);


			if (_context.Message != null)
			{
				var messages = await _context.Message
					.Where(m => m.ReceiverId == user.Id)
					.OrderByDescending(m => m.Timestamp)
					.ToListAsync();

				Messages = messages;
			}
			if (deletemessageid != 0)
			{
				string redirectUrl = await DeleteMessageAsync(deletemessageid, user.Id);
				return Redirect(redirectUrl);
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{

			if (NewMessage.Content != null && NewMessage.ReceiverId != null)
			{
				if (NewMessage.Title == null)
				{
					NewMessage.Title = "Ingen rubrik";
				}
				NewMessage.Timestamp = DateTime.Now;
				await _context.Message.AddAsync(NewMessage);
				await _context.SaveChangesAsync();

			}
			return RedirectToPage("./Inbox");


		}

		private async Task<string> DeleteMessageAsync(int deletemessageid, string userid)
		{
			var deleteMessage = await _context.Message.FindAsync(deletemessageid);
			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userid);

			_context.Message.Remove(deleteMessage);
			await _context.SaveChangesAsync();
			var redirectUrl = "/Inbox?userid=" + user.Id;
			return redirectUrl;
		}
	}
}
