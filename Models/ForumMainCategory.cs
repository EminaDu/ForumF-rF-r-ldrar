using System.Text.Json.Serialization;

namespace ForumFörFöräldrar.Models
{
	public class ForumMainCategory
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("date")]
		public DateTime Date { get; set; }
		public List<Models.ForumSubCategory>? ForumSubCategories { get; set; }
	}
}