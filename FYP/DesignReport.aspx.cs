using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace FYP
{
    public partial class DesignReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string tdate = DateTime.Now.ToString("yyyy-MM-dd");
                string txtTitle = Session["rptTitle"].ToString();
                string txtDesc = Session["rptDesc"].ToString();
                string wantDate = Session["wantDate"].ToString();

                if (wantDate == "yes")
                {
                    lblDate.Text = tdate;
                }
                else if (wantDate == "no")
                {
                    lblDate.Text = " ";
                }

                lblRptTitle.Text = txtTitle;
                lblRptDesc.Text = txtDesc;

                txtRptTitle.Text = txtTitle;
                txtRptDesc.Text = txtDesc;

                //take variables to report
                List<string> checkedBoxList = new List<string>();
                checkedBoxList = System.Web.HttpContext.Current.Session["SelectedTables"] as List<string>;
                DataTable dt = getFormDataDB(checkedBoxList);
                //add check db here if needed
                DataTable formTable = getFormData(dt);
                reportGridView.DataSource = formTable;
                reportGridView.DataBind();
                Session.Remove("rptTitle");
                Session.Remove("rptDesc");
            }
        }

        private DataTable getFormDataDB(List<string> mapId)
        {
            string connStrFormDB = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStrFormDB))
            {
                con.Open();
                foreach (string str in mapId)
                {
                    SqlCommand cmd = new SqlCommand("SELECT tableName, colName FROM Mapping WHERE mappingId = @mappingId", con);
                    cmd.Parameters.AddWithValue("@mappingId", str);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 0;
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                        dt.Rows[i]["tableName"].ToString();
                        dt.Rows[i]["colName"].ToString();
                        i++;
                    }
                }
            }
            return dt;
        }

        private DataTable getFormData(DataTable colNameDT)
        {
            string connStrReportDB = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStrReportDB))
            {
                string tableNames = "";
                string columns = "";
                // add in filters here
                for (int i = 0; i < colNameDT.Rows.Count; i++)
                {
                    //syntax dt.Rows[rowindex][columnName/columnIndex]
                    if (i == 0)
                    {
                        tableNames = colNameDT.Rows[i]["tableName"].ToString();
                        columns = colNameDT.Rows[i]["colName"].ToString();
                    }
                    else if (i == colNameDT.Rows.Count-1) {
                        if (!colNameDT.Rows[i]["tableName"].ToString().Equals(colNameDT.Rows[i-1]["tableName"].ToString())) { 
                        tableNames += ", " + colNameDT.Rows[i]["tableName"].ToString() + " ";
                        }
                        columns += ", " + colNameDT.Rows[i]["colName"].ToString() + " ";
                    }
                    else {
                        if (!colNameDT.Rows[i]["tableName"].ToString().Equals(colNameDT.Rows[i - 1]["tableName"].ToString()))
                        {
                            tableNames += ", " + colNameDT.Rows[i]["tableName"].ToString();
                        }
                        columns += ", " + colNameDT.Rows[i]["colName"].ToString();
                    }
                }
                string query = "SELECT " + columns + "FROM " + tableNames;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
    }
}