using System.Text.Json;

namespace ForumFörFöräldrar.DAL
{
	public class ForumMainCategoryManager
	{
		private static Uri BaseAddress = new Uri("https://eminaforumapi.azurewebsites.net/");
		private static string basePath = "api/ForumMainCategory";
        public static async Task<List<Models.ForumMainCategory>> GetAllForumMainCategories()
		{
			List<Models.ForumMainCategory> forumMainCategories = new List<Models.ForumMainCategory>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = BaseAddress;
				HttpResponseMessage response = await client.GetAsync(basePath);
				if (response.IsSuccessStatusCode)
				{
					string responseString = await response.Content.ReadAsStringAsync();
					forumMainCategories = JsonSerializer.Deserialize<List<Models.ForumMainCategory>>(responseString);
				}
				return forumMainCategories;
			}
		}
		public static async Task<Models.ForumMainCategory> GetOneForumMainCategory(int Id)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = BaseAddress;
				HttpResponseMessage response = await client.GetAsync($"{basePath}/{Id}");
				if (response.IsSuccessStatusCode)
				{
					string responseString = await response.Content.ReadAsStringAsync();
					Models.ForumMainCategory forumMainCategory = JsonSerializer.Deserialize<Models.ForumMainCategory>(responseString);
					return forumMainCategory;
				}
			}
			return null;
		}
		public async Task<bool> DeleteForumMainCategory(int id)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = BaseAddress;
				HttpResponseMessage response = await client.DeleteAsync($"{basePath}/{id}");
				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		public async Task<bool> UpdateForumMainCategory(Models.ForumMainCategory forumMainCategory)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = BaseAddress;
				HttpResponseMessage response = await client.PutAsJsonAsync($"{basePath}/{forumMainCategory.Id}", forumMainCategory);
				return response.IsSuccessStatusCode;
			}
		}
		public async Task<bool> CreateForumMainCategory(Models.ForumMainCategory forumMainCategory)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = BaseAddress;
				HttpResponseMessage response = await client.PostAsJsonAsync($"{basePath}", forumMainCategory);
				return response.IsSuccessStatusCode;
			}
		}
	}
}