using DAL.Models;
using Newtonsoft.Json;
using PostsAPI.Interfaces;

namespace PostsAPI.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";

        public async Task<Post[]> GetLatestPostsAsync()
        {
            string latestPostsUrl = $"{BaseUrl}newstories.json";
            string jsonResponse = await _httpClient.GetStringAsync(latestPostsUrl);
            int[] postIds = JsonConvert.DeserializeObject<int[]>(jsonResponse);

            Post[] posts = new Post[10];
            for (int i = 0; i < 10; i++)
            {
                string postUrl = $"{BaseUrl}item/{postIds[i]}.json";
                string postResponse = await _httpClient.GetStringAsync(postUrl);
                var post = JsonConvert.DeserializeObject<Post>(postResponse);
                posts[i] = post;
            }

            return posts;
        }
    }
}
