using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using PostsAPI.Interfaces;

namespace PostsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostsDbContext _dbContext;
        private readonly IHackerNewsService _hackerNewsService;

        public PostsController(IHackerNewsService hackerNewsService)
        {
            _dbContext = new PostsDbContext("Data Source=db.db;");
            _dbContext.CreateDatabase();
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostsAsync()
        {
            var posts = _dbContext.GetPosts();
            if (!posts.Any())
            {
                var hackerPosts = await _hackerNewsService.GetLatestPostsAsync();
                foreach (var post in hackerPosts)
                {
                    _dbContext.AddPost(post);
                }
                posts = _dbContext.GetPosts();
            }
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var post = _dbContext.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            _dbContext.AddPost(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }
    }
}
