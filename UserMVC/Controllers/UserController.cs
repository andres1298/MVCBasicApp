using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMVC.Models;

namespace UserMVC.Controllers
{
    public class UserController : Controller
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True;MultipleActiveResultSets=True";
        // GET: User
        public ActionResult Index()
        {
            List<User> users = new List<User>();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM TBL_USER";
                    DataTable table = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = new SqlCommand(query, conn);

                    dataAdapter.Fill(table);

                    foreach(DataRow row in table.Rows)
                    {
                        var user = new User();

                        user.ID = int.Parse(row["ID"].ToString());
                        user.Name = row["Name"].ToString();
                        user.LastName = row["LastName"].ToString();
                        user.Email = row["Email"].ToString();
                        user.Password = row["Password"].ToString();
                        users.Add(user);
                    }
                }
                return View(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            User user = new User();
            DataTable dataTable = new DataTable();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM TBL_USER WHERE @ID = ID";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ID", id);
                sqlDataAdapter.Fill(dataTable);

                if(dataTable.Rows.Count > 0)
                {
                    user.ID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                    user.Name = dataTable.Rows[0][1].ToString();
                    user.LastName = dataTable.Rows[0][2].ToString();
                    user.Email = dataTable.Rows[0][3].ToString();
                    user.Password = dataTable.Rows[0][4].ToString();

                    return View(user);
                }
                else
                {
                    RedirectToAction("Index");
                }
                return View();
            }
        }

        // GET: User/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO TBL_USER VALUES (@NAME, @LASTNAME, @EMAIL, @PASSWORD)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@NAME", user.Name);
                command.Parameters.AddWithValue("@LASTNAME", user.LastName);
                command.Parameters.AddWithValue("@EMAIL", user.Email);
                command.Parameters.AddWithValue("@PASSWORD", user.Password);

                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User user = new User();
            DataTable dataTable = new DataTable();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM TBL_USER WHERE ID = @PID";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PID", id);
                sqlDataAdapter.Fill(dataTable);

                if(dataTable.Rows.Count > 0)
                {
                    user.ID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                    user.Name = dataTable.Rows[0][1].ToString();
                    user.LastName = dataTable.Rows[0][2].ToString();
                    user.Email = dataTable.Rows[0][3].ToString();
                    user.Password = dataTable.Rows[0][4].ToString();
                    return View(user);
                }
                else
                {
                    RedirectToAction("Index");
                }
            }
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE TBL_USER SET NAME = @NAME, LASTNAME = @LASTNAME, EMAIL = @EMAIL, PASSWORD = @PASSWORD WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@ID", user.ID);
                command.Parameters.AddWithValue("@NAME", user.Name);
                command.Parameters.AddWithValue("@LASTNAME", user.LastName);
                command.Parameters.AddWithValue("@EMAIL", user.Email);
                command.Parameters.AddWithValue("@PASSWORD", user.Password);

                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            User user = new User();
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM TBL_USER WHERE ID = @PID";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PID", id);
                sqlDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    user.ID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                    user.Name = dataTable.Rows[0][1].ToString();
                    user.LastName = dataTable.Rows[0][2].ToString();
                    user.Email = dataTable.Rows[0][3].ToString();
                    user.Password = dataTable.Rows[0][4].ToString();
                    return View(user);
                }
                else
                {
                    RedirectToAction("Index");
                }
            }
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM TBL_USER WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@ID", user.ID);

                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
