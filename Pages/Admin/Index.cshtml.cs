using ForumFörFöräldrar.Areas.Identity.Data;
using ForumFörFöräldrar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ForumFörFöräldrar.Pages.Admin
{
    public class IndexModel : PageModel
    {
        [Authorize(Roles = "Admin")]
        public void OnGet()
        {
        }

    }
}
