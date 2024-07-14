using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заголовок обязателен для заполнения.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Url обязателен для заполнения.")]
        public string Url { get; set; }
    }
}
