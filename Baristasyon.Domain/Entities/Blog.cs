using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime BlogDate { get; set; } = DateTime.UtcNow;

        public string Category { get; set; } = null!;
        public string Author { get; set; } = null!;
    }
}
