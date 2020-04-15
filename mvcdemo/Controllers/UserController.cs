using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mvcdemo.Models;
namespace mvcdemo.Controllers
{
    public class UserController : Controller
    {
        SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bhoomi Vekariya\Documents\bhoomidatabasemvc.mdf;Integrated Security=True;Connect Timeout=30");

        // GET: User
        public ActionResult Index()
        {
            
            string query = "select * from [User]";
            SqlCommand cmd = new SqlCommand(query,cn);
            cn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            List<UserViewModel> lsUser = new List<UserViewModel>();
            while (rdr.Read()) {
                UserViewModel user = new UserViewModel();
                user.Id = Convert.ToInt32(rdr["ID"]);
                user.Name = rdr["Name"].ToString();
                user.Email = rdr["Email"].ToString();
                user.Phone = rdr["Phone"].ToString();
                lsUser.Add(user);
            }
            cn.Close();
            return View(lsUser);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            string query = "select * from [User] where Id="+id;
            SqlCommand cmd = new SqlCommand(query, cn);
            cn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            UserViewModel user = new UserViewModel();
            while (rdr.Read())
            {
                user.Id = Convert.ToInt32(rdr["ID"]);
                user.Name = rdr["Name"].ToString();
                user.Email = rdr["Email"].ToString();
                user.Phone = rdr["Phone"].ToString();
            }
            cn.Close();
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string query = "insert into [User](Name,Email,Phone) values(@Name,@Email,@Phone)";
                
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Name",collection["Name"].ToString()); 
                cmd.Parameters.AddWithValue("@Email",collection["Email"].ToString()); 
                cmd.Parameters.AddWithValue("@Phone",collection["Phone"].ToString());
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            string query = "select * from [User] where Id="+id;
            SqlCommand cmd = new SqlCommand(query, cn);
            cn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            UserViewModel user = new UserViewModel();
            if (rdr.Read()) {
                user.Id = Convert.ToInt32(rdr["Id"]);
                user.Name = Convert.ToString(rdr["Name"]);
                user.Email = Convert.ToString(rdr["Email"]);
                user.Phone = Convert.ToString(rdr["Phone"]);
            }
            cn.Close();
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                string query = "update [User] set Name=@Name,Email=@Email,Phone=@Phone where Id=@Id";

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(collection["Id"]));
                cmd.Parameters.AddWithValue("@Name", collection["Name"].ToString());
                cmd.Parameters.AddWithValue("@Email", collection["Email"].ToString());
                cmd.Parameters.AddWithValue("@Phone", collection["Phone"].ToString());
                cn.Open();  
                cmd.ExecuteNonQuery();
                cn.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            string query = "select * from [User] where Id="+id;
            SqlCommand cmd = new SqlCommand(query, cn);
            cn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            UserViewModel user = new UserViewModel();
            while (rdr.Read())
            {
                user.Id = Convert.ToInt32(rdr["ID"]);
                user.Name = rdr["Name"].ToString();
                user.Email = rdr["Email"].ToString();
                user.Phone = rdr["Phone"].ToString();
            }
            cn.Close();
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string query = "delete [User] where Id="+id;
                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}