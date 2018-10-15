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
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace FYP
{
    public class ReportElement {
        public int ReportID { get; set; }
        public string Value { get; set; }
        public string PosX { get; set; }
        public string PosY { get; set; }
        public string EleTypeName { get; set; }
        public string FontType { get; set; }
        


        public ReportElement(int reportID, string value, string posX, string posY, string eleTypeName, string fontType)
        {
            ReportID = reportID;
            Value = value;
            PosX = posX;
            PosY = posY;
            EleTypeName = eleTypeName;
            FontType = fontType;
        }

    }

    public partial class DesignReport : System.Web.UI.Page
    {
        private Style primaryStyle = new Style();
        protected string PostBackString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) {
                //if (Request["__EVENTARGUMENT"] == "saveOnClick")
                //{
                //   // BtnSave_Click();
                //}
            }
           
            if (!Page.IsPostBack)
            {
                PostBackString = Page.ClientScript.GetPostBackEventReference(this, "saveOnClick");
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
                    lblDate.Text = "";
                }

                lblRptTitle.Text = txtTitle;
                lblRptDesc.Text = txtDesc;

                txtRptTitle.Text = txtTitle;
                txtRptDesc.Text = txtDesc;

                //take variables to report
                string query = Session["query"].ToString();
                //add check db here if needed
                DataTable formTable = getFormData(query);
                reportGridView.DataSource = formTable;
                reportGridView.DataBind();
                //implement a way to dynamically add/assign position for hidden fields based on position

                foreach (FontFamily font in FontFamily.Families)
                {
                    fontFamilyDrpDwnList.Items.Add(font.Name.ToString());
                }
                
                Session.Remove("rptTitle");
                Session.Remove("rptDesc");
                Session.Remove("wantDate");
            }
        }
        
        private DataTable getFormData(string query)
        {
            string connStrReportDB = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStrReportDB))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                // if user uncheck filter, remove the condition
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
        protected void ChangeFont(object sender, EventArgs e)
        {
                primaryStyle.Font.Name =
                fontFamilyDrpDwnList.SelectedItem.Text;
                lblRptTitle.ApplyStyle(primaryStyle);
                lblRptDesc.ApplyStyle(primaryStyle);
                lblDate.ApplyStyle(primaryStyle);
            
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            /*
             Variables to get
             Report
             - StaffID, date, description, title,
             - Identify type of element, and create eleTypeId
             - Header_element, store all elements in header + positions. (seperate the position (X,Y))
             - Footer_element, ?????
             - Report_body, store query
             - Element_type, add name, fonttype
             */
            try {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                //insert data for report
                parameters.Add("@name", lblRptTitle.Text);
                parameters.Add("@staffId", Session["userId"].ToString());
                parameters.Add("@status", 1);
                if (hiddenRptDate.Value == "")
                {
                    parameters.Add("@dateGenerated", lblDate.Text);
                }
                else
                {
                    parameters.Add("@dateGenerated", DateTime.Now.ToString("yyyy-MM-dd"));
                }
                parameters.Add("@description", lblRptDesc.Text);
                string sql = "INSERT INTO Report" + "(name, staffId, status, dateGenerated, description)" +
                    " VALUES(@name, @staffId, @status, @dateGenerated, @description)";

                int rowsAffected = InsertUpdate(sql, parameters);

                int reportId = getReportID(parameters["@staffId"].ToString(), parameters["@name"].ToString());
                // init reportElement for title
                parameters.Clear();
                string titlePosition = hiddenRptTitle.Value;
                // solve the problem here, unable to split string.
                string[] coords = Regex.Split(titlePosition, ",");
                ReportElement reportEleTitle = new ReportElement(reportId, lblRptTitle.Text, coords[0], coords[1], "label", lblRptTitle.Font.Name);
                parameters.Add("@title", reportEleTitle);
                coords = null;

                // init reportElement for desc
                string descPosition = hiddenRptDesc.Value;
                coords = Regex.Split(descPosition, ",");
                ReportElement reportEleDesc = new ReportElement(reportId, lblRptDesc.Text, coords[0], coords[1], "label", lblRptDesc.Font.Name);
                parameters.Add("@desc", reportEleDesc);
                coords = null;

                // init reportElement for date
                if (lblDate.Text != "")
                {
                    string datePosition = hiddenRptDate.Value;
                    coords = Regex.Split(hiddenRptDate.Value, ",");
                    ReportElement reportEleDate = new ReportElement(reportId, lblDate.Text, coords[0], coords[1], "label", lblDate.Font.Name);
                    parameters.Add("@date", reportEleDate);
                    coords = null;
                }

                //add element_type
                sql = "INSERT INTO Element_type" + " (name, fontType) " + "VALUES " + "(@name, @fontType)";
                rowsAffected = InsertUpdate(sql, parameters);


                parameters.Clear();
                sql = "INSERT INTO Report_body " + "(reportID, query)" + " VALUES " + "(@reportID, @query)";
                string query = Session["query"].ToString();
                Session.Remove("query");
                parameters.Add("@reportID", reportId);
                parameters.Add("@query", query);
                rowsAffected = InsertUpdate(sql, parameters);

                //Add permissions
                parameters.Clear();
                sql = "INSERT INTO report_right " + "(reportId, staffId, rights)" + "VALUES " + "(@reportId, @staffId, @rights)";
                parameters.Add("@reportId",reportId);
                parameters.Add("@staffId", Session["userId"].ToString());
                parameters.Add("@rights", "R,E");
                rowsAffected = InsertUpdate(sql,parameters);

                //add footer code if needed.
                Response.Write("<script>alert('" + "Report saved successfully." + "')</script>");
                Response.Redirect("Homepage.aspx");
            }
            catch (SqlException ex) {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error saving report.')", true);
            }
        }
        
        protected void BtnClear_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            
        }

        public static int getReportID(string parameter, string parameter2)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT reportID FROM Report WHERE staffId = @staffId AND name = @name";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@staffId",parameter);
                cmd.Parameters.AddWithValue("@name", parameter2);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    return reader.GetInt32(0);
                }
                
            }
            return 0;
        }

        public static int InsertUpdate(string sql, Dictionary<string, object> parameters)
        {
            int rows = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                Boolean isReportEle = false;
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                int count = 0;
                foreach (string key in parameters.Keys)
                {
                    if (parameters[key] is string || parameters[key] is int) {
                        cmd.Parameters.AddWithValue(key, parameters[key]);
                    }
                    else if (parameters[key] is ReportElement) {
                        isReportEle = true;
                        count++;
                        ReportElement reportEle = (ReportElement)parameters[key];
                        using (cmd) {
                            cmd.CommandText = "INSERT INTO Element_type" + " (name, fontType) " + "VALUES " + "(@name, @fontType)";
                            cmd.Parameters.AddWithValue("@name", reportEle.EleTypeName);
                            cmd.Parameters.AddWithValue("@fontType", reportEle.FontType);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        int eleTypeId = 0;
                        
                            cmd.CommandText = "SELECT MAX(eleTypeId) FROM Element_type";
                        using (SqlDataReader reader = cmd.ExecuteReader()) { 
                            if (reader.Read())
                            {
                                eleTypeId = reader.GetInt32(0);
                                cmd.Parameters.Clear();
                            }
                            else
                            {
                                //error handling here
                            }
                        }
                        using (cmd) { 
                            cmd.CommandText = "INSERT INTO Header_element" + "(reportID, value, eleTypeId, xPosition, yPosition)" + " VALUES " + "(@reportID, @value, @eleTypeId, @xPosition, @yPosition)";
                            cmd.Parameters.AddWithValue("@reportID", reportEle.ReportID);
                            cmd.Parameters.AddWithValue("@value", reportEle.Value);
                            cmd.Parameters.AddWithValue("@eleTypeId", eleTypeId);
                            cmd.Parameters.AddWithValue("@xPosition", reportEle.PosX);
                            cmd.Parameters.AddWithValue("@yPosition", reportEle.PosY);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }
                }
                if (isReportEle == false)
                {
                    rows = cmd.ExecuteNonQuery();
                }
            }
            return rows;
        }
    }
}


//private HtmlInputHidden createHiddenField(Label label, int hiddenFieldID) {
//    HtmlInputHidden hf = new HtmlInputHidden();
//    hf.ID = hiddenFieldID.ToString();
//    hf.Attributes["class"] = "hiddenFieldClass";
//    return hf;
//}

//private int InitHiddenFields() {
//    int objCount = 0;
//    foreach (Control c in reportHeader.Controls) {
//        //add additional types as they get added.
//        if (c.GetType() == typeof(Label)) {
//            Label tempLabel = (Label)c;
//            HtmlInputHidden hf = createHiddenField(tempLabel,objCount);
//            hiddenPanel.Controls.Add(hf);
//            objCount++;
//        }
//    }
//    return objCount;
//}

//private int countElements()
//{
//    int objCount = 0;
//    foreach (Control c in reportHeader.Controls)
//    {
//        //add additional types as they get added.
//        if (c.GetType() == typeof(Label))
//        {
//            objCount++;
//        }
//    }
//    return objCount;
//}