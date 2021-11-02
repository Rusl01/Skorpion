using System.Collections.Generic;
using System.Linq;
using Application.Models;

namespace Application.ViewModels
{
    public class GameViewModel
    {
        public List<Game> Games { get; set; }
        public List<Genre> Genres { get; set; }
    }
}