using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog001.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string MailAdress { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string CreatedDate { get; set; }

        public Messages()
        {

        }

        public Messages(string namesurname, string mailadress, string title, string message, string createddate)
        {
            this.NameSurname = namesurname;
            this.MailAdress = mailadress;
            this.Title = title;
            this.Message = message;
            this.CreatedDate = createddate;
        }

    }
}
