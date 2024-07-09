using DAL.Models;

namespace PostsAPI.Interfaces
{
    public interface IHackerNewsService
    {
        Task<Post[]> GetLatestPostsAsync();
    }
}
