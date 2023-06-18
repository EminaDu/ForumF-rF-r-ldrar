using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ForumFörFöräldrar.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ForumFörFöräldrarUserTT class
public class ForumFörFöräldrarUserT : IdentityUser
{
	[PersonalData]
	public string? FirstName { get; set; }

	[PersonalData]
	public string? LastName { get; set; }

	[PersonalData]
	public string? NickName { get; set; }

	[PersonalData]
	public string? ImageUrl { get; set; }
	[PersonalData]
	public DateTime DateJoined { get; set; }
}

