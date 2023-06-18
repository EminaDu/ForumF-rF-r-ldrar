using ForumFörFöräldrar.Areas.Identity.Data;
using ForumFörFöräldrar.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ForumFörFöräldrar.Pages
{
	public class MessageModel : PageModel
	{
		private readonly ForumFörFöräldrarContext _context;
		private readonly UserManager<ForumFörFöräldrarUserT> _userManager;
		public MessageModel(ForumFörFöräldrarContext context, UserManager<ForumFörFöräldrarUserT> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		public Models.Message Message { get; set; }
		[BindProperty]
		public Models.Message NewMessage { get; set; }
		public ForumFörFöräldrarUserT User { get; set; }

		private static int _messageid;
		private static string _userid;

		public async Task<IActionResult> OnGetAsync(int messageid, int deletemessageid)
		{
			if (User != null)
			{
				_userid = User.Id;
			}
			if (messageid != 0)
			{
				_messageid = messageid;
			}
			Message = await _context.Message.FirstOrDefaultAsync(x => x.Id == _messageid);
			User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Message.ReceiverId);
			if (!Message.IsRead) Message.IsRead = true;
			await _context.SaveChangesAsync();

			if (deletemessageid != 0)
			{
				var deleteMessage = await _context.Message.FindAsync(deletemessageid);

				_context.Message.Remove(deleteMessage);
				await _context.SaveChangesAsync();
				var redirectUrl = "/Inbox?userid=" + _userid;
				return Redirect(redirectUrl);
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == NewMessage.SenderId);

			if (NewMessage.Content != null)
			{
				if (NewMessage.Title == null)
				{
					NewMessage.Title = "Ingen rubrik";
				}
				NewMessage.Timestamp = DateTime.Now;
				await _context.Message.AddAsync(NewMessage);
				await _context.SaveChangesAsync();

				var redirectUrl = "/Inbox?userid=" + _userid;
				return Redirect(redirectUrl);
			}
			var url = "/Message?messageid=" + _messageid;
			return Redirect(url);
		}


	}
}