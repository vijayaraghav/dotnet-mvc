using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using loginmvc.Models;
using System.Web.Configuration;
using MySql.Data.MySqlClient;

namespace loginmvc.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(User userModel)
        {

            if (userModel.Email == null || userModel.Password == null)
            {
                return View("Index", userModel);
            } else
            {
                string connstr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(connstr);
                con.Open();
                string query = @"INSERT INTO user(UserName, Email, Mobile, Password) VALUES('" + userModel.UserName + "','" + userModel.Email + "','" + userModel.Mobile + "','" + userModel.Password + "')";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return View("Index", "Login");
            }

        
            

        }
    }
}