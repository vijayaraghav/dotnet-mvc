using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using loginmvc.Models;
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

namespace loginmvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string connstr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            List<userfile> fileList = new List<userfile>();
            MySqlConnection con3 = new MySqlConnection(connstr);
            var allFileQuery = "SELECT UserId,file1,file2,file3 FROM user";
            var allFileCmd = new MySqlCommand(allFileQuery, con3);
            con3.Open();
            MySqlDataReader rdr = allFileCmd.ExecuteReader();
            while (rdr.Read())
            {
                userfile f = new userfile();
                f.UserId = rdr["UserId"].ToString();
                f.file1 = rdr["file1"].ToString();
                f.file2 = rdr["file2"].ToString();
                f.file3 = rdr["file3"].ToString();
                fileList.Add(f);


            }
            ViewBag.filelist = fileList;
            return View();
        }
        [HttpPost]
        public ActionResult upload(User userModel,List<HttpPostedFileBase> postedFile)
        {
            int count = 3;
            string connstr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            if (postedFile != null)
            {
                
               
                MySqlConnection con1 = new MySqlConnection(connstr);
                con1.Open();
                for (int i = 1; i <= 3; i++)
                {
                    string str = "file" + i.ToString();
                    string query1 = @"SELECT " + str + " FROM user WHERE Email = '" + userModel.Email + "'";
                    MySqlCommand cmd1 = new MySqlCommand(query1, con1);
                    MySqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        var a = dr.GetValue(0).ToString();
                        if (!string.IsNullOrEmpty(a))
                        {
                            count--;
                        }
                        
                    }
                    
                    dr.Close();
                }
               
                con1.Close();
                if (postedFile.Count <= count )
                {
                        
                        for (int j=0; j< postedFile.Count; j++)
                        {
                        int n = 0;
                        MySqlConnection con2 = new MySqlConnection(connstr);
                            con2.Open();
                            for (int i = 1; i <= 3; i++)
                            {
                                string str = "file" + i.ToString();
                                string query1 = @"SELECT " + str + " FROM user WHERE Email = '" + userModel.Email + "'";
                                MySqlCommand cmd1 = new MySqlCommand(query1, con2);
                                MySqlDataReader dr = cmd1.ExecuteReader();
                                while (dr.Read())
                                {
                                    var a = dr.GetValue(0).ToString();
                                    if (!string.IsNullOrEmpty(a))
                                    {
                                    }
                                    else
                                    {
                                        n = i;
                                        break;
                                    }
                                }
                                if (n != 0)
                                {

                                    break;


                                }

                                dr.Close();
                            }

                            con2.Close();


                            Stream fs = postedFile[j].InputStream;
                            BinaryReader br = new BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        br.Close();
                        fs.Close();
                        MySqlConnection con = new MySqlConnection(connstr);
                            con.Open();
                        string query = @"UPDATE user SET file" + n.ToString() + " = '" + bytes + "',name" + n.ToString() + "='" + postedFile[j].FileName.ToString() + "',type"+ n.ToString() +"='"+ postedFile[j] .ContentType.ToString()+ "' WHERE Email = '" + userModel.Email + "'";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                           

                        }
                    return RedirectToAction("Index", "Login");
                }


                
                else
                {
                    ViewBag.upload = false;
                }



                //string query = @"INSERT INTO user(file1) VALUES('" + bytes + "') WHERE Email = "+userModel.Email+"";
                //if (n <= 3)
                //{

                //}


            }

            return RedirectToAction("Index", "Login");
        }

        public void download(string id, string val)
        {
            byte[] fileContent = null;
            string connstr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            var allFileQuery = "SELECT file"+val+",name"+val+",type"+val+" FROM user WHERE UserId ='"+id+"'";
            MySqlConnection con3 = new MySqlConnection(connstr);
            var allFileCmd = new MySqlCommand(allFileQuery, con3);
            con3.Open();
            MySqlDataReader rdr = allFileCmd.ExecuteReader();
            MySqlDataAdapter sda = new MySqlDataAdapter();
            var contentType = "";
            var fileName = "";
            while (rdr.Read())
            {
                fileContent = (byte[])rdr["file"+val+""];
                contentType = (string)rdr["type" + val + ""];
                fileName = (string)rdr["name" + val + ""];
            }
            char[] characters = fileContent.Select(b => (char)b).ToArray();

              var value=characters.ToString();
            //var result = string.Concat(fileContent.Select(b => Convert.ToString(b, 2)));

            Response.Buffer = true;

            Response.Charset = "";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = contentType;
            //Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Transfer-Encoding", "binary\n");

            Response.AddHeader("content-disposition", "attachment;filename="

            + fileName);

            Response.AddHeader("Content-Length", Convert.ToString(fileContent.Length));
            MemoryStream stream = new MemoryStream(fileContent);
            string output = String.Empty;
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                output = reader.ReadToEnd();
            }
                var st = System.Text.Encoding.ASCII.GetString(fileContent);

            Response.BinaryWrite(fileContent);

            Response.Flush();

            Response.End();

            //string str = Encoding.UTF8.GetString(fileContent, 0, fileContent.Length);
            //var stri = BitConverter.ToString(fileContent); 
            //Console.WriteLine(BitConverter.ToString(fileContent));
            // string myFileName = @"C:\Users\User 101\Downloads\myfile.txt";
            // string result = System.Text.Encoding.UTF8.GetString(fileContent);
            // System.IO.File.WriteAllText(myFileName, result);

            //string string2 = Encoding.UTF8.GetString(fileContent);
            // var fies=BitConverter.ToString(fileContent);
            //Response.Buffer = true;

            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //Response.ContentType = "text/html";
            //Response.AddHeader("content-disposition", "attachment;filename=myfile.txt");
            //Response.BinaryWrite(fileContent);

            //Response.Flush();

            //Response.End();
            //return View();
            // MemoryStream ms = new MemoryStream(fileContent);
            // return File(fileContent, "Application/octet-stream");
            //Response.Clear();
            //MemoryStream ms = new MemoryStream(fileContent);
            //Response.ContentType = "application/*";
            //Response.AddHeader("content-disposition", "attachment;filename=labtest.txt");
            //Response.Buffer = true;
            //ms.WriteTo(Response.OutputStream);
            //Response.End();
            //return new FileStreamResult(Response.OutputStream, "application/*");
            //File.WriteA
            //return File(fileContent, "text/html");


            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "text/html";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=name.txt");
            //Response.BinaryWrite(BitConverter.ToString(fileContent));
            //Response.Flush();
            //Response.End();
        }
    }
}