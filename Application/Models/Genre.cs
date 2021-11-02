using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    [Table("genres")]
    public class Genre
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
    }
}