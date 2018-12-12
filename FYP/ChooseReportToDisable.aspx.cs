using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            ArrayList CheckBoxArray;
            if (ViewState["CheckBoxArray"] != null)
            {
                CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            }
            else
            {
                CheckBoxArray = new ArrayList();
            }

            Session.Timeout = 60;
            if (IsPostBack) {
                int CheckBoxIndex;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkDisable");
                        CheckBoxIndex = GridView1.PageSize * GridView1.PageIndex + (i + 1);
                        if (chk.Checked)
                        {
                            if (CheckBoxArray.IndexOf(CheckBoxIndex) == -1)
                            {
                                CheckBoxArray.Add(CheckBoxIndex);
                            }
                        }
                        else
                        {
                            if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                            {
                                CheckBoxArray.Remove(CheckBoxIndex);
                            }
                        }
                    }
                }
                ViewState["CheckBoxArray"] = CheckBoxArray;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void DataBind() {
        //    if (ViewState["gv"] == null) {
               
        //    }
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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

        

      

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
                {
                 int CheckBoxIndex = GridView1.PageSize * (GridView1.PageIndex) + (i + 1);
                   if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                        {
                            CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkDisable");
                            chk.Checked = true;
                        }
                    }
                }
            }
            //var selectedRows = (Session["checkedIDs"] != null) ? Session["checkedIDs"] as List<int> : new List<int>();
            ////current page. set the checked ids to a list
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    //get the checkbox in the row
            //    CheckBox checkedBox = row.FindControl("chkDisable") as CheckBox;
            //    var rowID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
            //    bool isRowPresent = selectedRows.Contains(rowID);
            //    if (checkedBox.Checked && !isRowPresent)
            //    {
            //        selectedRows.Add(rowID);
            //    }
            //    if (!checkedBox.Checked && isRowPresent)
            //    {
            //        selectedRows.Remove(rowID);
            //    }
            //}
            //GridView1.PageIndex = e.NewPageIndex;
            //GridView1.DataBind();
            ////get the select ids and make the gridview checkbox checked accordingly
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    CheckBox checkBox = row.FindControl("chkDisable") as CheckBox;
            //    var rowID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
            //    if (selectedRows.Contains(rowID))
            //    {
            //        checkBox.Checked = true;
            //    }
            //    else
            //    {
            //        checkBox.Checked = false;
            //    }
            //}
            //Session["checkedIDs"] = (selectedRows.Count > 0) ? selectedRows : null;
        }
    }