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
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace FYP
{
    /*
     Add a selection to display no duplicate records, and select the distinct column.
     Add sum()/count() function to the report builder
    */
    public class ReportElement {
        public int ReportID { get; set; }
        public string Value { get; set; }
        public string PosX { get; set; }
        public string PosY { get; set; }
        public string EleTypeName { get; set; }
        public string FontType { get; set; }
        public string Width { get; set; }



        public ReportElement(int reportID, string value, string posX, string posY, string eleTypeName, string fontType, string width)
        {
            ReportID = reportID;
            Value = value;
            PosX = posX;
            PosY = posY;
            EleTypeName = eleTypeName;
            FontType = fontType;
            Width = width;
        }

    }

    public partial class DesignReport : System.Web.UI.Page
    {
        private Style primaryStyle = new Style();
        protected string PostBackString;
        protected string query;
        Label lbl_homeCarouselAdd = new Label();
        static StringBuilder strDiv = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("EscapeText")) {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "EscapeText",
                " // save the original function pointer of the .NET __doPostBack function\n" +
                " // in a global variable netPostBack\n" +
                " var netPostBack = __doPostBack;\n" +
                " // replace __doPostBack with your own function\n" +
                " __doPostBack = EscapeHtml;\n" +
                " \n" +
                " function EscapeHtml (eventTarget, eventArgument) \n" +
                " {\n" +
                " // execute your own code before the page is submitted\n" +
                "setHiddenField();" +
                " \n" +
                " // call base functionality\n" +
                " \n" +
                " return netPostBack (eventTarget, eventArgument);\n" +
                " }\n", true);
            }
            if (Page.IsPostBack) {
                //set location of controls
                //save state of controls
                SaveState();
                //rebind gridview
                DataTable formTable = ViewState["formTable_data"] as DataTable;
                reportGridView.DataSource = formTable;
                if (Session["countTitle"] != null)
                {
                    // default option
                    reportGridView.ShowFooter = true;
                    // save footer row here
                    Session["footerName"] = Session["countTitle"].ToString();
                    Session["footerEnabled"] = "true";
                }
                reportGridView.DataBind();
                lblRptTitle.Text = txtRptTitle.Text;
                lblRptDesc.Text = txtRptDesc.Text;
                
            }
            if (!Page.IsPostBack)
            {
                Session.Timeout = 60;
                string tdate = DateTime.Now.ToString("yyyy-MM-dd");
                string txtTitle = Session["rptTitle"].ToString();
                string txtDesc = Session["rptDesc"].ToString();
                string wantDate = Session["wantDate"].ToString();
                string formName = Session["formName"].ToString();
                if (wantDate == "yes")
                {
                    lblDate.Text = tdate;
                }
                else if (wantDate == "no")
                {
                    lblDate.Text = "";
                }
                lblFormName.Text = formName;
                lblRptTitle.Text = txtTitle;
                lblRptDesc.Text = txtDesc;
                txtRptTitle.Text = txtTitle;
                txtRptDesc.Text = txtDesc;
                query = Convert.ToString(Request.QueryString["queryString"]);
                //add check db here if needed
                DataTable formTable = getFormData(query);
                reportGridView.DataSource = formTable;
                if (Session["countTitle"] != null) {
                    reportGridView.ShowFooter = true;
                    // save footer row here
                    Session["footerName"] = Session["countTitle"].ToString();
                    Session["footerEnabled"] =  "true";
                }
                hiddenFormID.Value = Session["formID"].ToString();
                ViewState["formTable_data"] = formTable;
                reportGridView.DataBind();

                //implement a way to dynamically add/assign position for hidden fields based on position
                foreach (FontFamily font in FontFamily.Families)
                {
                    fontFamilyDrpDwnList.Items.Add(font.Name.ToString());
                }
                //set default selected font
                fontFamilyDrpDwnList.SelectedIndex = fontFamilyDrpDwnList.Items.IndexOf(fontFamilyDrpDwnList.Items.FindByText("Times New Roman"));
                //set all to times new roman text
                primaryStyle.Font.Name = fontFamilyDrpDwnList.SelectedItem.Text;
                lblRptTitle.ApplyStyle(primaryStyle);
                lblRptDesc.ApplyStyle(primaryStyle);
                lblDate.ApplyStyle(primaryStyle);
                if (Session["isRedirect"] != null) {
                    if ((Boolean)Session["isRedirect"] == true)
                    {
                        // re initalize hidden fields
                        if (Session["imgPathSession"] != null) { 
                            hiddenImage.Value = Session["hiddenImage"].ToString();
                            hiddenHeight.Value = Session["hiddenHeight"].ToString();
                            hiddenWidth.Value = Session["hiddenWidth"].ToString();
                            chkImg.Checked = true;
                        }

                        hiddenRptTitle.Value = Session["hiddenRptTitle"].ToString();
                        hiddenRptDesc.Value = Session["hiddenRptDesc"].ToString();
                        if (Session["hiddenRptDate"] != null)
                        {
                            hiddenRptDate.Value = Session["hiddenRptDate"].ToString();
                            Session["hiddenRptDate"] = null;
                        }

                        if (Session["linePos"] != null)
                        {
                            hiddenLinePosition.Value = Session["linePos"].ToString();
                            hiddenLineWidth.Value = Session["lineWidth"].ToString();
                            chkHrVis.Checked = true;
                        }
                        LoadState();
                        Session["isRedirect"] = false;
                    }
                }
                reportGridView.ApplyStyle(primaryStyle);

                ViewState["selectedCbList"] = Session["checkedItems"];
                ListItemCollection cbList = Session["cbListItems"] as ListItemCollection;
                if (Session["cbListItems"] != null) {
                    foreach(ListItem li in cbList){
                        ColumnCbList.Items.Add(li);
                    }
                }
            }
        }

        protected void SaveState() {
            // save image path
            if (fileuploadASP.HasFile)
            {
                    System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)FindControl("imgprw");
                    string folderName = "~/Images";
                    string fileName = Path.GetFileName(fileuploadASP.PostedFile.FileName);
                    string fullpath = Path.Combine(folderName, fileName);
                    fileuploadASP.SaveAs(Server.MapPath(fullpath));
                    ViewState["imgPath"] = fullpath;
            }
            if (chkHrVis.Checked) {
                ViewState["hrPos"] = hiddenLinePosition.Value;
                ViewState["hrWidth"] = hiddenLineWidth.Value;
            }
            else if (!chkHrVis.Checked) {
                ViewState["hrPos"] =  null;
                ViewState["hrWidth"] = null;
            }
        }

        protected void LoadState() {
            string[] coords = null;
            if (Session["imgPathSession"] != null && ViewState["imgPath"] == null)
            {
                coords = Regex.Split(hiddenImage.Value, ",");
                ViewState["imgPath"] = Session["imgPathSession"].ToString();
                Session["imgPathSession"] = null;
                coords = null;
            }
            if (chkHrVis.Checked) {
                coords = Regex.Split(hiddenLinePosition.Value, ",");
                hrLine.Attributes.Add("style", "position:absolute; top:" + coords[1] + "px; left:" + coords[0] + "px;" + "width:" + hiddenLineWidth.Value + "px;");
                coords = null;
            }
            if (ViewState["imgPath"] != null)
            {
                System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)FindControl("imgprw");
                img.ImageUrl = (string)ViewState["imgPath"];
                System.Web.UI.WebControls.Panel imgDiv = (System.Web.UI.WebControls.Panel)FindControl("imgFrame");
                coords = Regex.Split(hiddenImage.Value, ",");
                imgDiv.Attributes.Add("style", "position:absolute; top:" + coords[1] + "px; left:" + coords[0] + "px;" + "width:" + hiddenWidth.Value + "px;" + "height:" + hiddenHeight.Value + "px;");
                coords = null;
            }
            //set coords for title
            coords = Regex.Split(hiddenRptTitle.Value, ",");
            lblRptTitle.Attributes.Add("style", " position:absolute; top:" + coords[1] + "px; left:" + coords[0] + "px;");
            coords = null;
            //set coords for desc
            coords = Regex.Split(hiddenRptDesc.Value, ",");
            lblRptDesc.Attributes.Add("style", " position:absolute; top:" + coords[1] + "px; left:" + coords[0] + "px;");
            coords = null;
            if (lblDate.Text != "")
            {
                coords = Regex.Split(hiddenRptDate.Value, ",");
                lblDate.Attributes.Add("style", " position:absolute; top:" + coords[1] + "px; left:" + coords[0] + "px;");
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
                primaryStyle.Font.Name = fontFamilyDrpDwnList.SelectedItem.Text;
                lblRptTitle.ApplyStyle(primaryStyle);
                lblRptDesc.ApplyStyle(primaryStyle);
                lblDate.ApplyStyle(primaryStyle);
                reportGridView.ApplyStyle(primaryStyle);
                LoadState();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string height = hiddenHeight.Value;
            string width = hiddenWidth.Value;
            string pos = hiddenImage.Value;
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
                //test footer
                
                //insert data for report
                parameters.Add("@name", txtRptTitle.Text);
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
                parameters.Add("@description", txtRptDesc.Text);
                parameters.Add("@formId", hiddenFormID.Value);
                string sql = "INSERT INTO Report" + "(name, staffId, status, dateGenerated, description, formId)" +
                    " VALUES(@name, @staffId, @status, @dateGenerated, @description, @formId)";

                    int rowsAffected = InsertUpdate(sql, parameters);

                int reportId = getReportID();
                // init reportElement for title
                parameters.Clear();
                string titlePosition = hiddenRptTitle.Value;
                // solve the problem here, unable to split string.
                string[] coords = Regex.Split(titlePosition, ",");
                ReportElement reportEleTitle = new ReportElement(reportId, txtRptTitle.Text, coords[0], coords[1], "title", lblRptTitle.Font.Name,"");
                parameters.Add("@title", reportEleTitle);
                coords = null;

                    // init reportElement for desc
                    string descPosition = hiddenRptDesc.Value;
                    coords = Regex.Split(descPosition, ",");
                    ReportElement reportEleDesc = new ReportElement(reportId, txtRptDesc.Text, coords[0], coords[1], "desc", lblRptDesc.Font.Name,"");
                    parameters.Add("@desc", reportEleDesc);
                    coords = null;

                    // init reportElement for date
                    if (lblDate.Text != "")
                    {
                        string datePosition = hiddenRptDate.Value;
                        coords = Regex.Split(hiddenRptDate.Value, ",");
                        ReportElement reportEleDate = new ReportElement(reportId, lblDate.Text, coords[0], coords[1], "date", lblDate.Font.Name,"");
                        parameters.Add("@date", reportEleDate);
                        coords = null;
                    }

                //add footer
                if ((string)Session["footerEnabled"] == "true")
                {
                    string footerName = Session["footerName"].ToString();
                    ReportElement footerElement = new ReportElement(reportId, footerName, "", "", "footer", "","");
                    parameters.Add("@footer", footerElement);
                }
                //add line if exists
                if (chkHrVis.Checked)
                {
                    coords = Regex.Split(hiddenLinePosition.Value, ",");
                    ReportElement lineElement = new ReportElement(reportId,"line",coords[0],coords[1],"line","",hiddenLineWidth.Value);
                    parameters.Add("@line", lineElement);
                }

                //add element_type
                sql = "INSERT INTO Element_type" + " (name, fontType) " + "VALUES " + "(@name, @fontType)";
                rowsAffected = InsertUpdate(sql, parameters);


                    parameters.Clear();
                    sql = "INSERT INTO Report_body " + "(reportID, query)" + " VALUES " + "(@reportID, @query)";
                    string query = Convert.ToString(Request.QueryString["queryString"]);
                    Session.Remove("query");
                    parameters.Add("@reportID", reportId);
                    parameters.Add("@query", query);
                    rowsAffected = InsertUpdate(sql, parameters);

                //Add image to db
                if (ViewState["imgPath"] != null) { 
                parameters.Clear();
                coords = Regex.Split(hiddenImage.Value, ",");
                sql = "INSERT INTO Header_image " + "(reportID, imagePath, width, height, xPosition, yPosition)" + " VALUES " + "(@reportID, @imagePath, @width, @height, @xPosition, @yPosition)";
                parameters.Add("@reportID", reportId);
                parameters.Add("@imagePath", ViewState["imgPath"].ToString());
                parameters.Add("@width", hiddenWidth.Value);
                parameters.Add("@height", hiddenHeight.Value);
                parameters.Add("@xPosition", coords[0]);
                parameters.Add("@yPosition", coords[1]);
                rowsAffected = InsertUpdate(sql, parameters);
                coords = null;
                }

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
                catch (SqlException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error saving report.')", true);
                }
            
            
        }
        


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseTemplate1.aspx");
        }

        public static int getReportID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT MAX(reportID) FROM Report";
                SqlCommand cmd = new SqlCommand(sql, con);
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
                        if (key == "@footer") {
                            using (cmd)
                            {
                                cmd.CommandText = "INSERT INTO Footer_element" + "(reportID, value, eleTypeId, xPosition, yPosition)" + " VALUES " + "(@reportID, @value, @eleTypeId, @xPosition, @yPosition)";
                                cmd.Parameters.AddWithValue("@reportID", reportEle.ReportID);
                                cmd.Parameters.AddWithValue("@value", reportEle.Value);
                                cmd.Parameters.AddWithValue("@eleTypeId", eleTypeId);
                                cmd.Parameters.AddWithValue("@xPosition", reportEle.PosX);
                                cmd.Parameters.AddWithValue("@yPosition", reportEle.PosY);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        else if (key == "@line") {
                            using (cmd)
                            {
                                cmd.CommandText = "INSERT INTO Header_element" + "(reportID, value, eleTypeId, xPosition, yPosition, width)" + " VALUES " + "(@reportID, @value, @eleTypeId, @xPosition, @yPosition, @width)";
                                cmd.Parameters.AddWithValue("@reportID", reportEle.ReportID);
                                cmd.Parameters.AddWithValue("@value", reportEle.Value);
                                cmd.Parameters.AddWithValue("@eleTypeId", eleTypeId);
                                cmd.Parameters.AddWithValue("@xPosition", reportEle.PosX);
                                cmd.Parameters.AddWithValue("@yPosition", reportEle.PosY);
                                cmd.Parameters.AddWithValue("@width", reportEle.Width);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        else
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
        
        protected string sumCol(string rowName, string tableName) { // add db name if needed
            string connectionString = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString)) {
                string query = "SELECT SUM(" + rowName + ") FROM " + tableName; // manually add table name
                con.Open();
                SqlCommand cmd = new SqlCommand(query,con);
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read())
                    {
                        return reader.GetValue(0).ToString();
                    }
                }
            }
            return null;
        }
       

        protected void reportGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                System.Data.DataRowView dtview;
                DateTime dt;
                int intCounter;
                // Get the contents of the current row
                // as a DataRowView 
                dtview = (DataRowView)e.Row.DataItem;
                // Loop through the individual values in the
                // DataRowView's ItemArray 
                for (intCounter = 0; intCounter <= dtview.Row.ItemArray.Length - 1; intCounter++)
                {
                    // Check if the current value is
                    // a System.DateTime type 
                    if (dtview.Row.ItemArray[intCounter] is System.DateTime)
                    {
                        // If it is a DateTime, cast it as such
                        // into the variable 
                        dt = (DateTime)dtview.Row.ItemArray[intCounter];
                        // Set the text of the current cell
                        // as the short date representation
                        // of the datetime 
                        e.Row.Cells[intCounter].Text = dt.ToShortDateString();
                    }
                }
            }
            if (Session["countTitle"] != null) {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    string query = Request.QueryString["queryString"];
                    DataTable formTable = getFormData(Convert.ToString(Request.QueryString["queryString"]));
                    //get index of column
                    int count = 0;
                    foreach (DataColumn col in formTable.Columns)
                    {
                        if (col.ColumnName == Session["footerName"].ToString())
                        {
                            break;
                        }
                        else
                            count++;
                    }
                    e.Row.ID = "footerRowId";
                    if (count > 0) { 
                    e.Row.Cells[count-1].Controls.Add(new Literal() { Text = "Total :" });
                    e.Row.Cells[count-1].HorizontalAlign = HorizontalAlign.Right;
                    }
                    if (formTable.Columns[count].DataType.Name.ToString() == "Double")
                    {
                        double total = formTable.AsEnumerable().Sum(row => row.Field<double>(Session["footerName"].ToString()));
                        e.Row.Cells[count].Controls.Add(new Literal() { Text = total.ToString() });
                    }
                    else if (formTable.Columns[count].DataType.Name.ToString() == "Int32" || formTable.Columns[count].DataType.Name.ToString() == "Int64" || formTable.Columns[count].DataType.Name.ToString() == "Int16")
                    {
                        int total = formTable.AsEnumerable().Sum(row => row.Field<int>(Session["footerName"].ToString()));
                        e.Row.Cells[count].Controls.Add(new Literal() { Text = total.ToString() });
                    }
                    else if (formTable.Columns[count].DataType.Name.ToString() == "Decimal")
                    {
                        decimal total = formTable.AsEnumerable().Sum(row => row.Field<decimal>(Session["footerName"].ToString()));
                        e.Row.Cells[count].Controls.Add(new Literal() { Text = total.ToString() });
                    }
                }

            }
        }
        
        // add items to filter dropdownlist if any checkbox item is checked.
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> checkedList = new List<string>();
            if (ViewState["selectedCbList"] != null)
            {
                checkedList = (List<string>)ViewState["selectedCbList"];
            }
            string value = string.Empty;
            string result = Request.Form["__EVENTTARGET"];
            string[] checkedBox = result.Split('$');
            int index = int.Parse(checkedBox[checkedBox.Length - 1]);
            if (ColumnCbList.Items[index].Selected)
            {
                value = ColumnCbList.Items[index].Text;
                checkedList.Add(value);
            }
            else if (!ColumnCbList.Items[index].Selected)
            {
                value = ColumnCbList.Items[index].Text;
                checkedList.Remove(value);
            }
            ViewState["selectedCbList"] = checkedList;

            if (CheckBox3.Checked == true)
            {
                selectCount.Items.Clear();
                ListItem firstele = new ListItem("", "", true);
                int j = 0;
                for (int i = 0; i < ColumnCbList.Items.Count; i++)
                {
                    if (ColumnCbList.Items[i].Selected)
                    {
                        ListItem cbItem = new ListItem(ColumnCbList.Items[i].Text, ColumnCbList.Items[i].Value);
                        DataTable dt = getDBInfo(ColumnCbList.Items[i].Value);
                        string rowType = "";
                        foreach (DataRow row in dt.Rows)
                        {
                            if (j == 0)
                            {
                                Session.Add("nameOfTable", row["nameOfTable"].ToString());
                                j++;
                            }
                            rowType = getColumnType(row["nameOfColumn"].ToString(), row["nameOfTable"].ToString());
                        }
                        if (rowType == "int" || rowType == "double" || rowType == "decimal")
                        {
                            selectCount.Items.Add(cbItem);
                            selectCount.SelectedIndex = 0;
                        }
                    }
                }
                if (selectCount.Items.Count != 0)
                {
                    selectCount.Visible = true;
                    Label5.Visible = true;
                }
                else
                {
                    selectCount.Visible = false;
                    Label5.Visible = false;
                }
            }
            else {
                selectCount.Visible = false;
                Label5.Visible = false;
            }
            
        }
        

        // add dbName to param when needed
        private string getColumnType(string nameOfColumn, string nameOfTable)
        {
            //change this to use with any different form databases.
            string connStr = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = string.Format("SELECT {0} FROM {1}", "[" + nameOfColumn + "]", "[" + nameOfTable + "]");
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    System.Type type = reader.GetFieldType(0);
                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.DateTime:
                            con.Close();
                            return "date";
                        case TypeCode.String:
                            con.Close();
                            return "string";
                        case TypeCode.Int32:
                            con.Close();
                            return "int";
                        case TypeCode.Decimal:
                            con.Close();
                            return "decimal";
                        case TypeCode.Double:
                            con.Close();
                            return "double";
                        default:
                            break;
                    }
                }
            }
            return null;
        }

        // optimize this if got time, change to like hs one
        private DataTable getMappingData(string formId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // think about getting and passing formId if needed
                string query = "SELECT mappingId, nameOfColumn, nameOfTable FROM Mapping WHERE formId = @formId";
                

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@formId", formId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    con.Close();
                    return dt;
                }
                else
                    con.Close();
                return null;
            }
        }


        private DataTable getDBInfo(string mappingId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // think about getting and passing formId if needed
                SqlCommand cmd = new SqlCommand("SELECT nameOfDatabase, nameOfColumn, nameOfTable FROM Mapping WHERE mappingId = @mappingId", con);
                cmd.Parameters.AddWithValue("@mappingId", mappingId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    con.Close();
                    return dt;
                }
                else
                    con.Close();
                return null;
            }
        }

        private string getColAndTable(DataTable colNameDT)
        {
            string tableNames = "";
            string columns = "";
            // add in filters here
            List<string> checkboxSelection = (List<string>)ViewState["selectedCbList"];
            for (int i = 0; i < colNameDT.Rows.Count; i++)
            {
                //syntax dt.Rows[rowindex][columnName/columnIndex]
                if (i == 0)
                {
                    tableNames = colNameDT.Rows[i]["nameOfTable"].ToString();
                    columns = checkboxSelection[0];
                }
                else if (i == colNameDT.Rows.Count - 1)
                {
                    if (!colNameDT.Rows[i]["nameOfTable"].ToString().Equals(colNameDT.Rows[i - 1]["nameOfTable"].ToString()))
                    {
                        tableNames += ", " + colNameDT.Rows[i]["nameOfTable"].ToString() + " ";
                    }
                }
                else
                {
                    if (!colNameDT.Rows[i]["nameOfTable"].ToString().Equals(colNameDT.Rows[i - 1]["nameOfTable"].ToString()))
                    {
                        tableNames += ", " + colNameDT.Rows[i]["nameOfTable"].ToString();
                    }
                }
            }
            Boolean ignoreFirstElement = false;
            foreach (string listItem in checkboxSelection)
            {
                if (ignoreFirstElement == false)
                {
                    ignoreFirstElement = true;
                }
                else
                {
                    columns += ", " + listItem;
                }
            }
            // insert query string here
            string query = "SELECT " + columns + " FROM " + tableNames;
            return query;
        }

        

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (CheckBox3.Checked == false)
            {
                selectCount.Visible = false;
                Label5.Visible = false;
            }
            else
            {
                selectCount.Items.Clear();
                ListItem firstele = new ListItem("", "", true);
                for (int i = 0; i < ColumnCbList.Items.Count; i++)
                {
                    if (ColumnCbList.Items[i].Selected)
                    {
                        ListItem cbItem = new ListItem(ColumnCbList.Items[i].Text, ColumnCbList.Items[i].Value);
                        DataTable dt = getDBInfo(ColumnCbList.Items[i].Value);
                        string rowType = "";
                        foreach (DataRow row in dt.Rows)
                        {
                            rowType = getColumnType(row["nameOfColumn"].ToString(), row["nameOfTable"].ToString());
                        }
                        if (rowType == "int" || rowType == "double" || rowType == "decimal")
                        {
                            selectCount.Items.Add(cbItem);
                            selectCount.SelectedIndex = 0;
                        }
                    }
                }
                if (selectCount.Items.Count != 0)
                {
                    selectCount.Visible = true;
                    Label5.Visible = true;
                }
                else
                {
                    selectCount.Visible = false;
                    Label5.Visible = false;
                }
            }
        }

        private string QueryBuilder()
        {
            //check if filter option is selected.
            //checks if dropdownlist item is selected

            DataTable dt = getMappingData(hiddenFormID.Value);
            string query = getColAndTable(dt);
            return query;
            // return non filtered query here.

        }

        // edit this save button to resubmit data on same page.
        protected void Button1_Click(object sender, EventArgs e)
        {
            string query = QueryBuilder();
            Session["rptTitle"] = lblRptTitle.Text;
            Session["rptDesc"] = lblRptDesc.Text;
            Session["formID"] = hiddenFormID.Value;
            string wantDate = "";
            if (lblDate.Text == "") {
                wantDate = "no";
            }
            else {
                wantDate = "yes";
            }
            Session["wantDate"] = wantDate;
            if (CheckBox3.Checked == true)
            {
                Session["countTitle"] = selectCount.SelectedItem.Text;
            }
            else {
                Session["countTitle"] = null;
            }
            Session["cbListItems"] = ColumnCbList.Items;
            Session["checkedItems"] = (List<string>)ViewState["selectedCbList"];
            if (ViewState["imgPath"] != null)
            {
                Session["imgPathSession"] = (string)ViewState["imgPath"];
                Session["hiddenImage"] = hiddenImage.Value;
                Session["hiddenHeight"] = hiddenHeight.Value;
                Session["hiddenWidth"] = hiddenWidth.Value;
            }

            if (chkHrVis.Checked) {
                Session["linePos"] = hiddenLinePosition.Value;
                Session["lineWidth"] = hiddenLineWidth.Value;
            }
            Session["isRedirect"] = true;

            //set back values for hidden fields
            Session["hiddenRptTitle"] = hiddenRptTitle.Value;
            Session["hiddenRptDesc"] = hiddenRptDesc.Value;
            if (hiddenRptDate.Value != "") { 
                Session["hiddenRptDate"] = hiddenRptDate.Value;
            }

            Response.Redirect("~/DesignReport.aspx?queryString=" + query);
        }

        protected void reportGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            reportGridView.DataSource = ViewState["formTable_data"];
            reportGridView.PageIndex = e.NewPageIndex;
            if (reportGridView.PageCount - 1 == reportGridView.PageIndex)
            {
                reportGridView.ShowFooter = true;
            }
            else
            {
                reportGridView.ShowFooter = false;
            }
            reportGridView.DataBind();
        }
        
        protected void reportGridView_DataBound(object sender, EventArgs e)
        {
            if (Session["footerEnabled"] != null) {
                if (Session["footerEnabled"].ToString() == "true") {
                    reportGridView.FooterRow.Visible = this.reportGridView.PageIndex == this.reportGridView.PageCount - 1;
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DataTable formTable = getFormData(Request.QueryString["queryString"]);
            reportGridView.DataSource = formTable;
            reportGridView.PageIndex = reportGridView.PageIndex + 1;
            reportGridView.DataBind();
        }
    }
}