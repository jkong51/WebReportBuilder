using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class ChooseReportToDisable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[1].Text == "1")
                    {
                        CheckBox chkbox = (CheckBox)e.Row.FindControl("chkDisable");
                        chkbox.Checked = true;
                    }
                }
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                int status = 0;
                int reportId = Int32.Parse(row.Cells[0].Text);
                CheckBox chk = (CheckBox)row.FindControl("chkDisable");
                if (chk.Checked)
                {
                    status = 1;
                }
                updateStatus(status, reportId);
            }
            Response.Write("<script>alert('" + "Reports updated successfully." + "')</script>");
            Response.Redirect("Homepage.aspx");
        }

        protected void updateStatus(int status, int reportId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE Report SET status = @status WHERE reportId = @reportId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@reportId", reportId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}