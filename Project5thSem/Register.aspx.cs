using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project5thSem
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if(username != "" && email != "" && password != "")
            {
                if (password == confirmPassword)
                {
                    string connString = "SERVER = localhost; DATABASE = edmarkcollege_db; UID = root; PASSWORD = kiran78";

                    using (MySqlConnection conn = new MySqlConnection(connString)) 
                    {
                        conn.Open();

                        string checkUserSql = "SELECT COUNT(*) FROM users WHERE username = @Username";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkUserSql, conn))
                        {
                            checkCmd.Parameters.AddWithValue("username", username);
                            int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (userCount > 0) {
                                lblMessage.Text = "Username already existl Please choose another username.";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }

                        string sql = "INSERT INTO users (username, email, password, user_type) VALUES ('" + username + "', '" + email + "', '" + password + "', 'Student')";
                        MySqlCommand cmd = new MySqlCommand(@sql, conn);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0) 
                        {
                            lblMessage.ForeColor= System.Drawing.Color.Green;
                            lblMessage.Text = "Successfully Registered.";

                            txtUsername.Text = "";
                            txtEmail.Text = "";
                            txtPassword.Text = "";
                            txtConfirmPassword.Text = "";

                            Response.Redirect("~/Login.aspx");
                        }
                        else
                        {
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "Registration Failed.";
                        }
                    }
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Password do not match.";
                }
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "All fields are required.";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";

            txtUsername.Focus();
        }
    }
}