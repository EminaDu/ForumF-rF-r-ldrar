using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ForumFörFöräldrar.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public DateTime Date { get; set; }
		public bool Offensive { get; set; }
		public string UserId { get; set; }
		public virtual Post? Post { get; set; }
		public int PostId { get; set; }
	}
}