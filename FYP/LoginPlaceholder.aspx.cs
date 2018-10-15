using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace FYP
{
    public partial class LoginPlaceholder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr)) {
                SqlCommand cmd = new SqlCommand("SELECT Users.staffId, Users.username, Users.password, Users.positionId, User_detail.name, User_detail.faculty, User_detail.department FROM  User_detail INNER JOIN Users ON User_detail.staffId = Users.staffId WHERE(Users.username = @username) AND(Users.password = @password)",con);
                    cmd.Parameters.AddWithValue("@username", Login1.UserName);
                    cmd.Parameters.AddWithValue("@password", Login1.Password);
                    con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows) {
                    while (reader.Read()) {
                        string userId = reader["staffId"].ToString();
                        string posId = reader["positionId"].ToString();
                        string staffName = reader["name"].ToString();
                        string faculty = reader["faculty"].ToString();
                        string department = reader["department"].ToString();
                        Session["staffName"] = staffName;
                        Session["faculty"] = faculty;
                        Session["department"] = department;
                        Session["userId"] = userId;
                        Session["posId"] = posId;
                        // change back to homepage.aspx after testing
                        Response.Redirect("Homepage.aspx");
                    }
                }
                }
            }
        }
    }