using System.Collections.Generic;

namespace Application.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
    }
}