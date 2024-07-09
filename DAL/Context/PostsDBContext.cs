using DAL.Models;
using Microsoft.Data.Sqlite;

namespace DAL.Context
{
    public class PostsDbContext
    {
        private readonly string _connectionString;

        public PostsDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(
                    "CREATE TABLE IF NOT EXISTS Posts (Id INTEGER PRIMARY KEY, Title TEXT, Url TEXT)",
                    connection
                ))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Post> GetPosts()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT * FROM Posts", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var posts = new List<Post>();
                        while (reader.Read())
                        {
                            posts.Add(new Post
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Url = reader.GetString(2)
                            });
                        }
                        return posts;
                    }
                }
            }
        }

        public Post GetPost(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT * FROM Posts WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Post
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Url = reader.GetString(2)
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public void AddPost(Post post)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("INSERT INTO Posts (Title, Url) VALUES (@Title, @Url)", connection))
                {
                    command.Parameters.AddWithValue("@Title", post.Title);
                    command.Parameters.AddWithValue("@Url", post.Url);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
