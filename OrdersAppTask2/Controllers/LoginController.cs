using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersAppTask2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;


namespace OrdersAppTask2.Controllers
{
    public class LoginController : Controller
    {
        //this is the context needed to access the database
        Prog7311task2Context db = new Prog7311task2Context();
        public LoginController(Prog7311task2Context context)
        {
            db = context;
        }
        //this allows the web page to appear
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        //this posts results based on if the username exists in the database or not 
        [HttpPost]
        public IActionResult Login(String username, string password)
        {
            //this hashes the login password so that it can match the already hashed password
            var hash = Crypto.Hash(password);
            password = hash;
            decimal num = 0;
            User u = db.Users.Where(us => us.Username.Equals(username) && us.Password.Equals(password)).FirstOrDefault();

            if (u != null)
            {
                HttpContext.Session.SetString("LoggedInUser", u.Username);
                HttpContext.Session.SetString("LoggedStatus", u.Status);

                return RedirectToAction("Index", "Products");
            }


            else
            {
                ViewBag.Error = "Either the Username or Password entered are incorrect";
                return View();
            }
        }
    }
}
