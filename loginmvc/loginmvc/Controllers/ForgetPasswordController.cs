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
    public class ForgetPasswordController : Controller
    {
        
        // GET: ForgetPassword
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult generateOTP(User userModel)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);
            Random generator = new Random();
            String str = generator.Next(0, 999999).ToString("D6");
            conn.Open();
            //string query = @"UPDATE user SET OTP='"+str+"' WHERE Email='"+userModel.Email+"'";
            string query = @"INSERT INTO otp(otp,UserId) VALUES ('" + str + "',(SELECT UserId FROM user WHERE Email='" + userModel.Email + "'))";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            HttpCookie cookname = new HttpCookie("Name");
            cookname.Value = userModel.Email;
            ViewData["email"] = userModel.Email;
            userdetails.uname = userModel.Email;
           
            conn.Close();
            return RedirectToAction("Index", "ChangePassword",userModel);
        }
    }
}