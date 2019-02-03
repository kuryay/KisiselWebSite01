using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog001.Models
{
    public class Information
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string WebsiteTitle { get; set; }
        public string WebsiteSubtitle { get; set; }
        public string About { get; set; }
        public string MailAdress { get; set; }

        public Information()
        {

        }

        public Information(string namesurname, string websitetitle, string websitesubtitle, string about, string mailadress)
        {
            this.NameSurname = namesurname;
            this.WebsiteTitle = websitetitle;
            this.WebsiteSubtitle = websitesubtitle;
            this.About = about;
            this.MailAdress = mailadress;
        }

    }
}
