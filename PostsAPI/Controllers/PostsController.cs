using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using PostsAPI.Interfaces;
using PostsAPI.Models;

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

        [HttpPost("")]
        public async Task<IActionResult> GetPostsAsync(PaginationModel model)
        {
            var posts = _dbContext.GetPosts().AsEnumerable();
            if (!posts.Any())
            {
                var hackerPosts = await _hackerNewsService.GetLatestPostsAsync();
                foreach (var post in hackerPosts)
                {
                    _dbContext.AddPost(post);
                }
            }
            posts = _dbContext.GetPosts()
                    .Skip(model.PageNumber * model.PageSize)
                    .Take(model.PageSize);
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

        [HttpPost("Add")]
        public IActionResult AddPost(Post post)
        {
            _dbContext.AddPost(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }
    }
}
