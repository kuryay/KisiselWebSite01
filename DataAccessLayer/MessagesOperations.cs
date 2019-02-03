using Blog001.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog001.DataAccessLayer
{
    public class MessagesOperations
    {

        public List<Messages> getMessages()
        {
            var list = new List<Messages>();
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var messages = db.GetCollection<Messages>("Messages");
                foreach (Messages item in messages.FindAll())
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public Messages getMessageById(int id)
        {
            var result = new Messages();
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var messages = db.GetCollection<Messages>("Messages");
                result = messages.Find(x => x.Id == id).FirstOrDefault();
            }
            return result;
        }

        public void addMessage(Messages message)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var messages = db.GetCollection<Messages>("Messages");
                messages.Insert(message);
            }
        }

        public void deleteMessage(int id)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var message = db.GetCollection<Messages>("Messages");
                message.Delete(id);
            }
        }

    }
}
