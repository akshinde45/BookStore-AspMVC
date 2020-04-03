using FantasticBookStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasticBookStore.Controllers
{
    public class HomeController : Controller
    {

        FantasticContext BookDb = new FantasticContext();

        //........Front Page For User
        public ActionResult Index()
        {

            using (FantasticContext BookDb = new FantasticContext())
            {

                return View(BookDb.books.ToList());
                // return View(data);
                // ServiceReference.TestWcfClient client = null;
                //return View(FantasticContext.Books.ToList());
                // return View(client.GetuserTypeDetails());

            }
        }


        [HttpPost]
        public ActionResult Index(NameValueCollection nvclc)
        {


            using (FantasticContext BookDb = new FantasticContext())
            {

                string name = Convert.ToString(Session["name"]);
                if (name == "")
                {
                    return RedirectToAction("Index", "home");

                }

                else
                {
                    //ServiceReference.TestWcfClient client = null;
                    return View(BookDb.books.ToList());
                    //return View(client.GetuserTypeDetails());

                }
            }

        }

        //..........Login by User...........

        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(NameValueCollection nvclc)
        {
            nvclc = Request.Form;
            string name = nvclc["uname"];
            string pass = nvclc["pass"];
            using (FantasticContext context = new FantasticContext())
            {
                user user = context.users.SingleOrDefault(d => d.username == name && d.pass == pass);
                if (user != null)
                {
                    if (user.type == "admin")
                    {
                        Session["name"] = user.username;
                        Session["id"] = user.id;
                        return RedirectToAction("AdminIndex", "admin");
                    }

                    else
                        Session["name"] = user.username;
                    Session["id"] = user.id;
                    return RedirectToAction("userIndex", "Home");

                }

                else
                    return RedirectToAction("Login");
            }
           

        }

        //..........User After Login..........



        [HttpGet]
        public ActionResult userIndex()
        {

            using (FantasticContext BookDb = new FantasticContext())
            {


                return View(BookDb.books.ToList());
                // return View(data);

            }

        }
        [HttpPost]
        public ActionResult userIndex(NameValueCollection nvclc)
        {



            nvclc = Request.Form;
            string id = nvclc["bid"];
            string bname = nvclc["bname"];
            double price = Convert.ToDouble(nvclc["price"]);
            string aname = nvclc["aname"];
            string userid = Convert.ToString(Session["id"]);
            string category = nvclc["category"];
            string publishYear = nvclc["publishYear"];


            using (FantasticContext BookDb = new FantasticContext())
            {

                cart cat = BookDb.carts.SingleOrDefault(c => c.cartId == id);
                if (cat == null)
                {
                    cart cart = new cart();

                    cart.cartId = id;
                    cart.bookId = id;
                    cart.bookName = bname;
                    cart.authorName = aname;
                    cart.categoryName = category;
                    cart.userId = userid;
                    cart.price = price;
                    BookDb.carts.Add(cart);
                    BookDb.SaveChanges();
                    return RedirectToAction("AddTocart", "Home");
                }

                else
                {
                    ViewBag.Message = "Already Added";
                   
                    return View(BookDb.books.ToList());
                }


            }

            using (FantasticContext BookDb = new FantasticContext())
            {


                return View(BookDb.books.ToList());
                // return View(data);

            }

        }

        //.......Add to Cart.......

        //Add to the Cart part 
        [HttpGet]
        public ActionResult AddTocart()
        {
            string name = Convert.ToString(Session["name"]);
            if (name == "")
            {
                return RedirectToAction("Login", "home");

            }

            else

                using (FantasticContext BookDb = new FantasticContext())
                {
                    return View(BookDb.carts.ToList());
                }

        }

        [HttpPost]
        public ActionResult AddTocart(NameValueCollection nvclc)
        {

            nvclc = Request.Form;
            string a = nvclc["quantity"];
            int quantity = Convert.ToInt32(a);
            //int quantity = 1;

            string userId = nvclc["userId"];
            string bookId = nvclc["bookId"];
            string categoryName = nvclc["categoryName"];
            double price = Convert.ToDouble(nvclc["price"]);
            string authorName = nvclc["authorName"];
            string bookName = nvclc["bookName"];
            using (FantasticContext context = new FantasticContext())
            {


                checkout check = new checkout();

                check.bookId = bookId;
                check.userId = userId;
                check.price = price * quantity;
                check.quantity = quantity;
                check.authorName = authorName;
                check.categoryName = categoryName;
                check.bookName = bookName;



                context.checkouts.Add(check);
                context.SaveChanges();

                //cart cart = context.carts.SingleOrDefault(d => d.cartId == bookId);
                //context.carts.Remove(cart);
                //context.SaveChanges();
                return Redirect("/home/checkout");



            }

            return RedirectToAction("checkout", "home");

        }

        [HttpGet]
        public ActionResult RemoveIteam(string id)
        {

            using (FantasticContext BookDb = new FantasticContext())
            {

                cart cart = BookDb.carts.SingleOrDefault(d => d.cartId == id);
                BookDb.carts.Remove(cart);
                BookDb.SaveChanges();
                return Redirect("/home/AddTocart");

            }

        }

        public ActionResult checkout()
        {
            return View();
        }

    }
}