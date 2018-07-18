using loginmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace loginmvc.Controllers
{
    public class LoginController : Controller
    {
        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorise(User userModel, string button)
        {
            if (userModel.Email != null && userModel.Password !=null)
            {
                string connstr = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(connstr);
                con.Open();
                string query = @"SELECT * FROM user WHERE Email='" + userModel.Email + "' AND " + "Password='" + userModel.Password + "'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    //return RedirectToAction("Index","Home");
                    return RedirectToAction("Index", "Home");
                    //return Redirect("~/Home/Index");
                }
                else
                {
                    return View("Index", userModel);
                }
               
            }
            else
            {
                return View("Index", userModel);
            }
                
               
            
            
            
        }
       
    }
}