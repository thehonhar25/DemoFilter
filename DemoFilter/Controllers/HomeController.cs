using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DemoFilter.Models;
using DemoFilter.Attribute;

namespace DemoFilter.Controllers
{

    public class HomeController : Controller
    {

        [Check]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult userlogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult userlogin(UserDetails userDetails)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["mori"].ToString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_validateUser";
                        cmd.Parameters.AddWithValue("@username", userDetails.UserName);
                        cmd.Parameters.AddWithValue("@pass", userDetails.PassWord);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            Session["user"] = userDetails.UserName;
                            return RedirectToAction("Index");
                        }

                    }


                }
            }
            catch
            {

            }
            return View();
            
        }

    }
}
