namespace ForumFörFöräldrar.Models
{
	public class Report
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string? Text { get; set; }
		public string? Category { get; set; }
	}
}