using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalksViewModel
    {
        public Walker walker { get; set; }
        public List<Walks> Walks { get; set; }
    }
}
