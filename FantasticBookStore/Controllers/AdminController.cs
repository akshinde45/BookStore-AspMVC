using FantasticBookStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasticBookStore.Controllers
{
    public class AdminController : Controller
    {
         FantasticContext BookDb = new FantasticContext();
        // GET: Admin
      
        [HttpGet]
       
        public ActionResult AdminIndex(/*NameValueCollection nvclc*/)
        {
            //  SqlDataReader dr;
            //nvclc = Request.Form;
            //string name = nvclc["uname"];
            //string pass = nvclc["pass"];
            using (FantasticContext BookDb = new FantasticContext())
            {

               // user user = BookDb.users.SingleOrDefault(d => d.username == name && d.pass == pass);
                //  user user=BookDb.users.SingleOrDefault()

                 string name = Convert.ToString(Session["name"]);


                //var type = "select type from users where username-@username and password=@password";
              //  var userdetail = BookDb.users.Where(m => m.username == name && m.pass == pass).FirstOrDefault();
               // user user= BookDb.users.Find
                if (name == "admin")
                    {
                        return View(BookDb.categories.ToList());

                    }
                    else

                        return RedirectToAction("Login", "home");

                
               
            }
        }
        [HttpPost]
        public ActionResult AdminIndex(HttpPostedFileBase file)
        {
            if (file != null)
            {
                NameValueCollection nvclc = Request.Form;
                string pic = nvclc["bkid"] + ".jpg";
                //string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }



                using (FantasticContext BookDb = new FantasticContext())
                {
                    Book books = new Book();
                    books.bookId = nvclc["bkid"];
                    books.bookName = nvclc["bkname"];
                    books.authorName = nvclc["atname"];
                    books.catId = nvclc["cat"];
                    books.publishYear = nvclc["pbyear"];
                    books.quantity = Convert.ToInt32(nvclc["quantity"]);
                    books.price = Convert.ToDouble(nvclc["price"]);
                    BookDb.books.Add(books);
                    BookDb.SaveChanges();
                    return RedirectToAction("List", "Admin");


                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("List", "Admin");
        }

       

        //...........List of Books..............

        public ActionResult List()
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string name = Convert.ToString(Session["name"]);
                if (name == "admin")
                {
                    return View(BookDb.books.ToList());

                }


                else

                    return RedirectToAction("Login", "home");


            }
        }

        //...........Get Category List........
        [HttpGet]
        public JsonResult GetCategoryList()
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                return Json(BookDb.categories.ToList());
            }

        }

        [HttpPost]
        public ActionResult List(NameValueCollection nvclc)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string name = Convert.ToString(Session["name"]);
                if (name == "admin")
                {


                    nvclc = Request.Form;
                    string search = nvclc["search"];
                    if (search != "")
                    {

                        Book dept = BookDb.books.SingleOrDefault(d => d.bookName.StartsWith(search));
                        if (dept != null)
                        {
                            ViewBag.vs += "<tr>" + "</td>" + "<td>" + dept.bookName + "</td>" + "<td>" + dept.bookId + "</td>" + "<td>" + dept.authorName + "</td>" + "<td>" + dept.price + "</td>" + "<td>" + dept.publishYear + "</td><td>" + dept.quantity + "</td>" + "</tr>";
                            return View(BookDb.books.ToList());
                        }

                        return View(BookDb.books.ToList());
                    }

                    return View(BookDb.books.ToList());

                }


                else

                    return RedirectToAction("Login", "home");


            }
        }
        //........Search Book.......
        public ActionResult AdminFindBook(string Searchby, string search)
        {
            if (Searchby == "Book_Name")
            {
                var model = BookDb.books.Where(book => book.bookName == search || search == null).ToList();
                return View(model);

            }
            else
            {
                var model = BookDb.books.Where(book => book.authorName.StartsWith(search) || search == null).ToList();
                return View(model);
            }
        }
        //.........Edit ...............
        [HttpGet]
        public ActionResult Edit(string id)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string name = Convert.ToString(Session["name"]);
                if (name == "admin")
                {

                    Book dept = BookDb.books.SingleOrDefault(d => d.bookId == id);
                    return View(dept);
                }

                else

                    return RedirectToAction("Login", "home");
            }
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                Book booktoup = BookDb.books.SingleOrDefault(b => b.bookId == book.bookId);
                booktoup.bookName = book.bookName;
                booktoup.authorName = book.authorName;
                booktoup.price = book.price;
                booktoup.publishYear = book.publishYear;
                booktoup.quantity = book.quantity;

                BookDb.SaveChanges();
            }
            return RedirectToAction("List", "Admin");
        }

        //*********Details........
        [HttpGet]
        public ActionResult Details(string id)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string name = Convert.ToString(Session["name"]);
                if (name == "admin")
                {
                    Book book = BookDb.books.SingleOrDefault(b => b.bookId == id);
                    return View(book);
                }

                else

                    return RedirectToAction("Login", "home");
            }
        }

        //..............Reports...........
        [HttpGet]
        public ActionResult report()
        {
            string name = Convert.ToString(Session["name"]);
            if (name == "admin")

                using (FantasticContext BookDb = new FantasticContext())
                {
                    var x = (from r in BookDb.checkouts select r.quantity);
                    if (x != null)
                    {
                        int p = x.Sum();
                        ViewBag.bag = p;
                        return View(BookDb.categories.ToList());
                    }
                    else
                        return View(BookDb.categories.ToList());
                }
            return RedirectToAction("Login", "home");
        }
        [HttpPost]
        public ActionResult report(NameValueCollection nvclc)
        {
            nvclc = Request.Form;
            string cat = nvclc["cat"];
            using (FantasticContext BookDb = new FantasticContext())
            {
                var x = (from r in BookDb.checkouts select r.quantity);
                int p = x.Sum();
                ViewBag.bag = p;

                var dataset2 = (from recordset in BookDb.checkouts where recordset.categoryName == cat select recordset.quantity);
                int p1 = dataset2.Sum();
                ViewBag.cat = p1;
                return View(BookDb.categories.ToList());
            }

        }

        //...........Delete .............
        [HttpGet]
        public ActionResult Delete(string id)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string name = Convert.ToString(Session["name"]);
                if (name == "admin")
                {
                    Book book = BookDb.books.SingleOrDefault(b => b.bookId == id);
                    return View(book);
                }

                else

                    return RedirectToAction("Login", "home");
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            string photoName = id + ".jpg";
            string fullPath = Request.MapPath("~/images/" + photoName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            using (FantasticContext BookDb = new FantasticContext())
            {
                Book book = BookDb.books.SingleOrDefault(b => b.bookId == id);
                BookDb.books.Remove(book);
                BookDb.SaveChanges();
            }
            return RedirectToAction("List");
        }

        //.......Category List........
        [HttpGet]
        public ActionResult catList()
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string name = Convert.ToString(Session["name"]);
                if (name == "admin")
                {

                    return View(BookDb.categories.ToList());
                    // return View(data);
                }

                else

                    return RedirectToAction("Login", "home");

            }
        }


        //...........Create Category..........

        [HttpGet]

        public ActionResult createCat()
        {
            string name = Convert.ToString(Session["name"]);
            if (name == "admin")
            {

                return View();
            }

            else

                return RedirectToAction("Login", "home");
        }

        [HttpPost]

        public ActionResult createCat(category cat)
        {

            using (FantasticContext BookDb = new FantasticContext())
            {
                if (ModelState.IsValid)
                {
                    BookDb.categories.Add(cat);
                    BookDb.SaveChanges();
                    return RedirectToAction("catList", "Admin");
                }
                else
                {
                    return View(cat);
                }

            }
        }

        //.........Cat Edit.......
        [HttpGet]
        public ActionResult cEdit(string id)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string catName = Convert.ToString((Session["name"]));
                if (catName == "admin")
                {
                    category cat = BookDb.categories.SingleOrDefault(c => c.catId == id);
                    return View(cat);
                }
                else

                    return RedirectToAction("Login", "home");

            }
        }


        [HttpPost]
        public ActionResult cEdit(category category)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                category cat = BookDb.categories.SingleOrDefault(b => b.catId == category.catId);
                cat.catName = category.catName;

                BookDb.SaveChanges();
            }
            return RedirectToAction("catList", "Admin");
        }

        //........Cat Details..........
        [HttpGet]
        public ActionResult catDetails(string id)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string catname = Convert.ToString(Session["name"]);
                if (catname == "admin")
                {
                    category cat = BookDb.categories.SingleOrDefault(c => c.catId == id);
                    return View(cat);
                }

                else

                    return RedirectToAction("Login", "home");
            }
        }
        //.........Cat Delete........
        [HttpGet]

        public ActionResult catDelete(string id)
        {
            using (FantasticContext BookDb = new FantasticContext())
            {
                string catname = Convert.ToString(Session["name"]);
                if (catname == "admin")
                {
                    category cat = BookDb.categories.SingleOrDefault(c => c.catId == id);
                    return View(cat);
                }

                else

                    return RedirectToAction("Login", "home");
            }
        }

        [HttpPost]
        [ActionName("catDelete")]
        public ActionResult confirmDelete(string id)
        {


            using (FantasticContext BookDb = new FantasticContext())
            {
                category cat = BookDb.categories.SingleOrDefault(c => c.catId == id);
                BookDb.categories.Remove(cat);
                BookDb.SaveChanges();
            }
            return RedirectToAction("catList");
        }





        //..........Log Out..........

        public ActionResult Logout()
        {
            Session.Remove("name");


            return Redirect("/home/Login");

        }


    }
}