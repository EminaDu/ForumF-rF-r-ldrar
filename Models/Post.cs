using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ForumFörFöräldrar.Models
{
	public class Post
	{
		public int Id { get; set; }

		[Display(Name = "Titel")]
		[Required]
		public string Header { get; set; }
		[Display(Name = "Post")]
		[Required]
		public string Text { get; set; }
		public DateTime? Date { get; set; }
		public bool Offensive { get; set; }
		public string UserId { get; set; }
		public virtual ForumSubCategory? ForumSubCategory { get; set; }
		public int ForumSubCategoryId { get; set; }
		public List<Models.Comment>? Comments { get; set; }
	}
}