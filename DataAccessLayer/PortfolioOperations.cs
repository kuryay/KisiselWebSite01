using Blog001.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog001.DataAccessLayer
{
    public class PortfolioOperations
    {

        public List<Portfolio> getPortfolio()
        {
            var list = new List<Portfolio>();
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var portfolio = db.GetCollection<Portfolio>("Portfolio");
                foreach (Portfolio item in portfolio.FindAll())
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public Portfolio getPortfolioById(int id)
        {
            var result = new Portfolio();
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var portfolio = db.GetCollection<Portfolio>("Portfolio");
                result = portfolio.Find(x => x.Id == id).FirstOrDefault();
            }
            return result;
        }

        public void addPortfolio(Portfolio portfolio)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var portfolioList = db.GetCollection<Portfolio>("Portfolio");
                portfolioList.Insert(portfolio);
            }
        }

        public void deletePortfolio(int id)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var portfolioList = db.GetCollection<Portfolio>("Portfolio");
                portfolioList.Delete(id);
            }
        }

        public void updatePortfolio(Portfolio portfolio)
        {
            using (var db = new LiteDatabase(@"myDatabase.db"))
            {
                var portfolioList = db.GetCollection<Portfolio>("Portfolio");
                portfolioList.Update(portfolio);
            }
        }

    }
}
