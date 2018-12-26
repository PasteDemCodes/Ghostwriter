using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Ghostwriter.Models;


namespace Ghostwriter.Controllers
{
    [RequireHttps]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private pubsEntities db = new pubsEntities();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }
        
        public ActionResult Database()
        {
            return View();
        }
        
        public ActionResult Reports()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TopBooks(string DateFrom, string DateΤο, string BookAmount)
        {
            DateTime dateFrom;
            DateTime dateTo;
            int bookAmount;

            if (DateFrom == "") {
                // a very early date
                dateFrom = DateTime.Parse("1900 - 01 - 01 00:00:00.000");
            }
            else {
                dateFrom = DateTime.Parse(DateFrom);
            }

            if (DateΤο == "")
            {
                // a date not coming very soon
                dateTo = DateTime.Parse("2200 - 01 - 01 00:00:00.000");
            }
            else
            {
                dateTo = DateTime.Parse(DateΤο);
            }

            if (BookAmount == "")
            {
                // Max Int
                bookAmount = 2147483647;
            }
            else
            {
                bookAmount = Int32.Parse(BookAmount);
            }     

            /**
             * Query am trying to recreate using linq
             * 
             *  SELECT TOP 3 authors.au_lname, authors.au_fname, authors.phone, authors.address,
             *                  authors.city, authors.state, authors.zip, authors.contract
             *  FROM authors
             *  INNER JOIN titleauthor
             *      ON authors.au_id = titleauthor.au_id
             *  INNER JOIN titles
             *      ON titleauthor.title_id = titles.title_id
             *  INNER JOIN sales
             *      ON sales.title_id = titles.title_id
             *  WHERE sales.ord_date BETWEEN '1994-09-14 00:00:00.000' AND '1994-09-14 00:00:00.000'
             *  ORDER BY titles.ytd_sales DESC; ";
             *
             **/

            var result = from a in db.authors
                         join tl in db.titleauthors on a.au_id equals tl.au_id
                         join t in db.titles on tl.title_id equals t.title_id
                         join s in db.sales on t.title_id equals s.title_id
                         where (s.ord_date >= dateFrom) && (s.ord_date <= dateTo)
                         orderby t.ytd_sales descending
                         select new
                         {
                             a.au_lname,
                             a.au_fname,
                             a.phone,
                             a.address,
                             a.city,
                             a.state,
                             a.zip,
                             a.contract
                         };
            
            var books = new List<author>();
            int counter = 0;
            foreach (var b in result)
            {
                if (counter >= bookAmount) { break; }

                books.Add(new author()
                {
                    au_lname = b.au_lname,
                    au_fname = b.au_fname,
                    phone = b.phone,
                    address = b.address,
                    city = b.city,
                    state = b.state,
                    zip = b.zip,
                    contract = b.contract                  
                });

                counter++;
            }
            return View(books);
        }

        [HttpPost]
        public ActionResult Order(string DateFrom, string DateΤο, string StoreName)
        {
            DateTime dateFrom;
            DateTime dateTo;

            if (DateFrom == "")
            {
                // a very early date
                dateFrom = DateTime.Parse("1900 - 01 - 01 00:00:00.000");
            }
            else
            {
                dateFrom = DateTime.Parse(DateFrom);
            }

            if (DateΤο == "")
            {
                // a date not coming very soon
                dateTo = DateTime.Parse("2200 - 01 - 01 00:00:00.000");
            }
            else
            {
                dateTo = DateTime.Parse(DateΤο);
            }

            /**
             * Query am trying to recreate using linq
             * 
             *   SELECT sales.ord_num, titles.title, stores.stor_name, stores.stor_address,
             *           stores.city, stores.state, stores.zip
             *   FROM stores
             *   INNER JOIN sales
	         *       ON sales.stor_id = stores.stor_id
             *   INNER JOIN titles
	         *       ON titles.title_id = sales.title_id
             *   WHERE sales.ord_date BETWEEN '1994-09-14 00:00:00.000' AND '1994-09-14 00:00:00.000'
             *   AND stores.stor_name LIKE '%';";
             *
             **/

            var result = from s in db.stores
                         join sl in db.sales on s.stor_id equals sl.stor_id
                         join tl in db.titles on sl.title_id equals tl.title_id
                         where (sl.ord_date >= dateFrom) && (sl.ord_date <= dateTo)
                         && (s.stor_name.Contains(StoreName))
                         select new
                         {
                             sl.ord_num,
                             tl.title1,
                             s.stor_name,
                             s.stor_address,
                             s.city,
                             s.state,
                             s.zip
                         };

            var order = new List<Order> ();
            foreach (var o in result)
            {
                order.Add(new Order()
                {
                    OrderNumber = o.ord_num,
                    Title = o.title1,
                    StoreName = o.stor_name,
                    Address = o.stor_address,
                    City = o.city,
                    State = o.state,
                    Zip = o.zip
                    });
            }

            return View(order);
        }
    }
}