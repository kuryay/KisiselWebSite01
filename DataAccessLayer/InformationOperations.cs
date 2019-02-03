using Blog001.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog001.DataAccessLayer
{
    public class InformationOperations
    {

        public List<Information> getInformations()
        {
            var list = new List<Information>();
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var informations = db.GetCollection<Information>("Information");
                foreach (Information item in informations.FindAll())
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public Information getInformationById(int id)
        {
            var result = new Information();
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var informations = db.GetCollection<Information>("Information");
                result = informations.Find(x => x.Id == id).FirstOrDefault();
            }
            return result;
        }

        public void addInformation(Information information)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var informations = db.GetCollection<Information>("Information");
                informations.Insert(information);
            }
        }

        public void updateInformation(Information information)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var informations = db.GetCollection<Information>("Information");
                informations.Update(information);
            }
        }



    }
}
