using System;
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
    public partial class ViewReport : System.Web.UI.Page
    {
        private class header_element {
            public int ReportID { get; set; }
            public string Value { get; set; }
            public string EleType { get; set; }
            public string XPos { get; set; }
            public string YPos { get; set; }
            public string FontType { get; set; }

            public header_element(int reportID, string val, string eleType, string xPos, string yPos, string fontType) {
                ReportID = reportID;
                Value = val;
                EleType = eleType;
                XPos = xPos;
                YPos = yPos;
                FontType = fontType;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { 
            Dictionary<int, object> headerEleDictionary = getHeadEle(System.Convert.ToInt32(Session["reportId"].ToString()));
            foreach (int key in headerEleDictionary.Keys) {
                Label newLabel = new Label();
                header_element headEle = (header_element)headerEleDictionary[key];
                newLabel.Text = headEle.Value;
                newLabel.ID = "lbl" + key;
                if (key == 0) {
                    newLabel.CssClass = "reportHeader1";
                }
                else if (key != 0) {
                    newLabel.CssClass = "reportHeader2";
                }
                newLabel.Attributes.Add("style", " position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "font-family: '" + headEle.FontType + "';");
                reportHeader.Controls.Add(newLabel);
                }
                DataTable formTable = getFormData(Session["reportId"].ToString());
                ViewState["formTable_data"] = formTable;
                reportGridView.DataSource = formTable;
                reportGridView.DataBind();
                
            }
        }

        // in the future if more elements in header are added dynamically, write a function that counts the amount of elements that
        // need to be loaded first before calling getHeadEle

        private Dictionary<int,object> getHeadEle(int reportID) {
            Dictionary<int, object> headerEleDictionary = new Dictionary<int, object>();
            try {
                string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;             
                using (SqlConnection con = new SqlConnection(connectionString)) {
                    con.Open();
                    string sql = "SELECT he.value, et.name, he.xPosition, he.yPosition, et.fontType " +
                        "FROM Header_element he, Element_type et WHERE he.eleTypeId = et.eleTypeId AND he.reportID = @reportID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@reportID",reportID);
                    using(SqlDataReader sqlDataReader = cmd.ExecuteReader()) {
                        int i = 0;
                        while (sqlDataReader.Read())
                        {
                            header_element headEle = new header_element(reportID,sqlDataReader["value"].ToString(), sqlDataReader["name"].ToString(), sqlDataReader["xPosition"].ToString(), sqlDataReader["yPosition"].ToString(), sqlDataReader["fontType"].ToString());
                            headerEleDictionary.Add(i, headEle);
                            i++;
                        }
                    }
                }
            }
            catch (SqlException ex) {
                // catch error here
            }
            return headerEleDictionary;
        }

        private DataTable getFormData(string reportID)
        {
            string connStrReportDB = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            string connStrFormDB = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;
            DataTable dt = new DataTable();
            string query = "";
            using (SqlConnection con = new SqlConnection(connStrReportDB)) {
                con.Open();
                string getQuery = "SELECT query FROM Report_body WHERE reportID = @reportID";
                using (SqlCommand cmd = new SqlCommand(getQuery,con)) {
                    cmd.Parameters.AddWithValue("@reportID",reportID);
                    using (SqlDataReader reader = cmd.ExecuteReader()){ 
                        if (reader.Read()) {
                            query = reader["query"].ToString();
                        }
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(connStrFormDB))
            {
                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    con.Open();
                    // if user uncheck filter, remove the condition
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseRetrieveUses.aspx");
        }


        protected void reportGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            reportGridView.DataSource = ViewState["formTable_data"];
            reportGridView.PageIndex = e.NewPageIndex;
            reportGridView.DataBind();
        }
        /* 
* >> label.Attributes.Add("style", "top:10; right:10; position:absolute;"); <<
Things that need to be done
** reportId is in session **
- Get position and values of labels, and initialize them into the form.
- Populate the data in the column.
*/
    }
}