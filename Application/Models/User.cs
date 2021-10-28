using System.Collections.Generic;

namespace Application.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}