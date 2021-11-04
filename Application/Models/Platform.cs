using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    [Table("platforms")]
    public class Platform
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [NotMapped]
        public bool Selected { get; set; }

        public ICollection<GamePlatform> Games { get; set; }
    }
}