using Blog001.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog001.DataAccessLayer
{
    public class UsersOperations
    {

        public List<Users> getUsers()
        {
            var list = new List<Users>();
            using(var db = new LiteDatabase(@"myDatabase.db"))
            {
                var users = db.GetCollection<Users>("Users");
                foreach (Users item in users.FindAll())
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public Users getUserById(int id)
        {
            var result = new Users();
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var users = db.GetCollection<Users>("Users");
                result = users.Find(x => x.Id == id).FirstOrDefault();
            }
            return result;
        }

        public void addUser(Users user)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var users = db.GetCollection<Users>("Users");
                users.Insert(user);
            }
        }

        public void updateUser(Users user)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var users = db.GetCollection<Users>("Users");
                users.Update(user);
            }
        }

        public void deleteUser(int id)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var users = db.GetCollection<Users>("Users");
                users.Delete(id);
            }
        }

        public Users loginUser(Users user)
        {
            var result = new Users();
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var users = db.GetCollection<Users>("Users");
                result = users.Find(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
            }
            return result;
        }

    }
}
