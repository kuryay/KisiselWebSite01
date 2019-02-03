using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog001.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string Image { get; set; }

        public Portfolio()
        {
                
        }

        public Portfolio(string title, string description, string createddate, string image)
        {
            
        }

    }
}
