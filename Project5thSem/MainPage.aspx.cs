using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project5thSem
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               LoadSuggestions();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string suggestionContent = txtPostContent.Text.Trim();

            if (!string.IsNullOrEmpty(suggestionContent))
            {
                string connString = "SERVER = localhost; DATABASE = edmarkcollege_db; UID = root; PASSWORD = kiran78";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    string insertSql = "INSERT INTO suggestions (user_id, suggestion, created_at) VALUES (@UserId, @Suggestion, NOW())";
                    using (MySqlCommand cmd = new MySqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Suggestion", suggestionContent);

                        cmd.ExecuteNonQuery();
                        txtPostContent.Text = "";
                    }
                }
                LoadSuggestions();
            }            
        }
        private void LoadSuggestions()
        {
            string connString = "SERVER = localhost; DATABASE = edmarkcollege_db; UID = root; PASSWORD = kiran78";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string selectSql = @"
                    SELECT s.suggestion, s.created_at, s.reply, s.reply_at, u.username
                    FROM suggestions s
                    JOIN users u ON s.user_id = u.user_id
                    ORDER BY s.created_at DESC";

                using (MySqlCommand cmd = new MySqlCommand(selectSql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        string suggestionsHtml = "";

                        while (reader.Read())
                        {
                            string suggestionText = reader["suggestion"].ToString();
                            DateTime createdAt = Convert.ToDateTime(reader["created_at"]);
                            string username = reader["username"].ToString();
                            string replyText = reader["reply"] != DBNull.Value ? reader["reply"].ToString() : null;
                            DateTime? replyAt = reader["reply_at"] != DBNull.Value ? (DateTime?)reader["reply_at"] : null;

                            suggestionsHtml += "<div class='suggestion-item'>";
                            suggestionsHtml += $"<p><strong>{username}</strong> - {createdAt.ToString("yyyy-MM-dd")}</p>";
                            suggestionsHtml += $"<p>{suggestionText}</p>";

                            // Display reply if it exists
                            if (!string.IsNullOrEmpty(replyText))
                            {
                                suggestionsHtml += "<div class='reply-item'>";
                                suggestionsHtml += $"<p><strong>Management Reply:</strong> {replyText}</p>";
                                suggestionsHtml += $"<p><em>Replied on: {replyAt}</em></p>";
                                suggestionsHtml += "</div>";
                            }

                            suggestionsHtml += "<hr /></div>";
                        }

                        postContentDiv.InnerHtml = suggestionsHtml;
                    }
                }
            }
        }
        protected void Logout_Click()
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}