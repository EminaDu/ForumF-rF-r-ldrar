namespace ForumFörFöräldrar.Models
{
	public class ForumSubCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ForumMainCategory? ForumMainCategory { get; set; }
		public int ForumMainCategoryId { get; set; }
		public List<Models.Post>? Posts { get; set; }
		public DateTime Date { get; set; }
	}
}