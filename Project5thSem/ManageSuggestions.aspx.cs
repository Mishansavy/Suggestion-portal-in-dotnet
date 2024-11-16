using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project5thSem
{
    public partial class ManageSuggestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSuggestions();
            }
        }

        private void LoadSuggestions()
        {
            string connString = "SERVER=localhost; DATABASE=edmarkcollege_db; UID=root; PASSWORD=kiran78";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string selectSql = @"
                    SELECT s.suggestion_id, s.suggestion, s.created_at, s.reply, s.reply_at, u.username
                    FROM suggestions s
                    JOIN users u ON s.user_id = u.user_id
                    WHERE s.reply IS NULL -- Only load suggestions without replies
                    ORDER BY s.created_at DESC";

                using (MySqlCommand cmd = new MySqlCommand(selectSql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        rptSuggestions.DataSource = reader;
                        rptSuggestions.DataBind();
                    }
                }
            }
        }

        protected void btnReply_Command(object sender, CommandEventArgs e)
        {
            int suggestionId = Convert.ToInt32(e.CommandArgument);
            string replyContent = ((TextBox)((Button)sender).NamingContainer.FindControl("txtReply")).Text.Trim();

            if (!string.IsNullOrEmpty(replyContent))
            {
                string connString = "SERVER=localhost; DATABASE=edmarkcollege_db; UID=root; PASSWORD=kiran78";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    string updateSql = "UPDATE suggestions SET reply = @Reply, reply_at = NOW() WHERE suggestion_id = @SuggestionId";
                    using (MySqlCommand cmd = new MySqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Reply", replyContent);
                        cmd.Parameters.AddWithValue("@SuggestionId", suggestionId);

                        cmd.ExecuteNonQuery();
                    }
                }

                LoadSuggestions(); // Reload suggestions after reply is posted
            }
        }

    }
}