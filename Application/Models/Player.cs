using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    [Table("players")]
    public class Player
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [NotMapped]
        public bool Selected { get; set; }

        public ICollection<GamePlayer> Games { get; set; }
    }
}