using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project5thSem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = txtUsernameOrEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password)) 
            {
                lblMessage.Text = "Please enter both username/email and password.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connString = "SERVER = localhost; DATABASE = edmarkcollege_db; UID = root; PASSWORD = kiran78";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string sql = "SELECT user_id, username, user_type FROM users WHERE (username = @UsernameOrEmail OR email = @UsernameOrEmail) AND password = @Password";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userId = reader["user_id"].ToString();
                            string username = reader["username"].ToString();
                            string userType = reader["user_type"].ToString();

                            Session["UserID"] = userId;
                            Session["Username"] = username;
                            Session["UserType"] = userType;

                            if (string.Equals(userType, "Management", StringComparison.OrdinalIgnoreCase))
                            {
                                Response.Redirect("ManageSuggestions.aspx");
                            }
                            else
                            {
                                Response.Redirect("MainPage.aspx");
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Invalid username/email or password.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;    
                        }
                    }
                    
                }
            }
        }
    }
}