using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace testing.Models
{

    

    public class Comments
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Comment { get; set; } = string.Empty;
        public Stock? Stock { get; set; } = null;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
