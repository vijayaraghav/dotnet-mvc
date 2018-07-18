using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using loginmvc.Models;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace loginmvc.Controllers
{
    public class ChangePasswordController : Controller
    {
        
        public string email = "";
        // GET: ChangePassword
        public ActionResult Index()
        {
            string str = Request.QueryString["Email"];
            email = str;
           
            return View();
        }
        public ActionResult changePasswordMethod(User userModel)
        {
           
                bool auth = false;
                var user = userdetails.uname;
                string connStr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connStr);

                conn.Open();
                string str1 = Request.QueryString["Email"];
                /*if (Request.Cookies["Name"].Value == null)
                {
                   str1 = Request.Cookies["Name"].Value;
                }*/
                string query1 = @"SELECT otp FROM user u INNER JOIN otp o ON u.UserId=o.UserId WHERE Email='" + user + "'";
                MySqlCommand cmd1 = new MySqlCommand(query1, conn);
                MySqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {

                if (dr[0].ToString() == userModel.otp)
                {
                    auth = true;
                }
            }

                else
                {
                    auth = false;
                    HR_COE();
                }
                conn.Close();
                if (auth == true)
                {
                    MySqlConnection conn2 = new MySqlConnection(connStr);
                    conn2.Open();
                    string query = @"UPDATE user SET Password='" + userModel.Password + "' WHERE Email='" + user + "'";
                    MySqlCommand cmd = new MySqlCommand(query, conn2);
                    cmd.ExecuteNonQuery();
                    conn2.Close();
                }
                else
                {
                    Session["valid"] = "true";
                    ViewBag.Message = "OTP entered incorrect";
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                }
                return View("Index", userModel);
            
           
        }
        public ContentResult HR_COE()
        {
            return Content("<script language='javascript' type='text/javascript'>alert     ('Requested Successfully ');</script>");
        }

    }
}