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
    /*
     Fix footer
     Make footer savable.
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
        protected string query;
            protected void Page_Load(object sender, EventArgs e)
            {   
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
                if (String.IsNullOrEmpty(Convert.ToString(Request.QueryString["queryString"]))) {
                    query = Session["query"].ToString();
                }
                else
                {
                    query = Convert.ToString(Request.QueryString["queryString"]);
                }
                //add check db here if needed
                DataTable formTable = getFormData(query);
                ViewState["formTable_data"] = formTable;
                reportGridView.DataSource = formTable;
                if (Session["countTitle"] != null) {
                    reportGridView.ShowFooter = true;
                }
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
                reportGridView.ApplyStyle(primaryStyle);


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

                try
                {
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
                    parameters.Add("@reportId", reportId);
                    parameters.Add("@staffId", Session["userId"].ToString());
                    parameters.Add("@rights", "R,E");
                    rowsAffected = InsertUpdate(sql, parameters);

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
            if (Session["countTitle"] != null) {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    DataTable formTable = getFormData(Session["query"].ToString());
                    //get index of column
                    int count = 0;
                    foreach (DataColumn col in formTable.Columns)
                    {
                        if (col.ColumnName == Session["countTitle"].ToString())
                        {
                            break;
                        }
                        else
                            count++;
                    }
                    e.Row.Cells[count-1].Controls.Add(new Literal() { Text = "Total :" });
                    e.Row.Cells[count-1].HorizontalAlign = HorizontalAlign.Right;
                    if (formTable.Columns[count].DataType.Name.ToString() == "Double")
                    {
                        double total = formTable.AsEnumerable().Sum(row => row.Field<double>(Session["countTitle"].ToString()));
                        e.Row.Cells[count].Controls.Add(new Literal() { Text = total.ToString() });
                    }
                    else if (formTable.Columns[count].DataType.Name.ToString() == "Int32" || formTable.Columns[count].DataType.Name.ToString() == "Int64" || formTable.Columns[count].DataType.Name.ToString() == "Int16")
                    {
                        int total = formTable.AsEnumerable().Sum(row => row.Field<int>(Session["countTitle"].ToString()));
                        e.Row.Cells[count].Controls.Add(new Literal() { Text = total.ToString() });
                    }
                    else if (formTable.Columns[count].DataType.Name.ToString() == "Decimal")
                    {
                        Decimal total = formTable.AsEnumerable().Sum(row => row.Field<Decimal>(Session["countTitle"].ToString()));
                        e.Row.Cells[count].Controls.Add(new Literal() { Text = total.ToString() });
                    }

                }

            }
        }

        // display check box list items when choose form ddl is selected
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Visible = true;
            CheckBoxList1.Visible = true;
            DataTable dt = getMappingData(DropDownList1.SelectedValue);
            CheckBoxList1.DataValueField = "mappingId";
            CheckBoxList1.DataTextField = "nameOfColumn";
            CheckBoxList1.DataSource = dt;
            CheckBoxList1.DataBind();
        }

        // display filter conditions upon selection of the column name in filter function
        protected void SelectedItemDDL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // optimize this
            DataTable dt = getDBInfo(selectedItemDDL1.SelectedValue);

            string rowType = "";
            // returns only a single record, so change DT to something else later.
            foreach (DataRow row in dt.Rows)
            {
                rowType = getColumnType(row["nameOfColumn"].ToString(), row["nameOfTable"].ToString());
            }
            conditionDDL.Items.Clear();
            switch (rowType)
            {
                case "string":
                    conditionDDL.Items.Insert(0, new ListItem("equals", "="));
                    conditionDDL.Items.Insert(1, new ListItem("does not equal", "<>"));
                    break;
                case "date":
                    conditionDDL.Items.Insert(0, new ListItem("equals", "="));
                    conditionDDL.Items.Insert(1, new ListItem("less than", "<"));
                    conditionDDL.Items.Insert(2, new ListItem("more than", ">"));
                    conditionDDL.Items.Insert(3, new ListItem("more or equal than", ">="));
                    conditionDDL.Items.Insert(4, new ListItem("less or equal than", "<="));
                    break;
                case "int":
                    conditionDDL.Items.Insert(0, new ListItem("equals", "="));
                    conditionDDL.Items.Insert(1, new ListItem("less than", "<"));
                    conditionDDL.Items.Insert(2, new ListItem("more than", ">"));
                    conditionDDL.Items.Insert(3, new ListItem("more or equal than", ">="));
                    conditionDDL.Items.Insert(4, new ListItem("less or equal than", "<="));

                    break;
                case "decimal":
                    conditionDDL.Items.Insert(0, new ListItem("equals", "="));
                    conditionDDL.Items.Insert(1, new ListItem("less than", "<"));
                    conditionDDL.Items.Insert(2, new ListItem("more than", ">"));
                    conditionDDL.Items.Insert(3, new ListItem("more or equal than", ">="));
                    conditionDDL.Items.Insert(4, new ListItem("less or equal than", "<="));
                    break;
                case "double":
                    conditionDDL.Items.Insert(0, new ListItem("equals", "="));
                    conditionDDL.Items.Insert(1, new ListItem("less than", "<"));
                    conditionDDL.Items.Insert(2, new ListItem("more than", ">"));
                    conditionDDL.Items.Insert(3, new ListItem("more or equal than", ">="));
                    conditionDDL.Items.Insert(4, new ListItem("less or equal than", "<="));
                    break;
                default:
                    break;
            }
        }

        // add items to filter dropdownlist if any checkbox item is checked.
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("tableName");
            selectedItemDDL1.Items.Clear();
            selectCount.Items.Clear();
            ListItem firstele = new ListItem("", "", true);
            int j = 0;
            selectedItemDDL1.Items.Add(firstele);
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    ListItem cbItem = new ListItem(CheckBoxList1.Items[i].Text, CheckBoxList1.Items[i].Value);
                    selectedItemDDL1.Items.Add(cbItem);

                    DataTable dt = getDBInfo(CheckBoxList1.Items[i].Value);
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
            if (CheckBoxList1.SelectedIndex == -1)
            {
                filterTablePlaceHolder.Visible = false;
            }
        }

        //query builder
        private string QueryBuilder()
        {
            //check if filter option is selected.
            //checks if dropdownlist item is selected

            DataTable dt = getMappingData(DropDownList1.SelectedValue);
            string query = getColAndTable(dt);


            if (selectedItemDDL1.SelectedIndex > -1 && conditionDDL.SelectedIndex > -1)
            {
                string filteredColName = selectedItemDDL1.SelectedItem.Text;
                string condition = conditionDDL.SelectedValue;
                query += " WHERE " + selectedItemDDL1.SelectedItem.Text + " " + conditionDDL.SelectedValue + " " + filterBox1.Text;
                return query;
            }
            else
                return query;
            // return non filtered query here.

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
            List<string> checkboxSelection = new List<string>();
            foreach (ListItem listItem in CheckBoxList1.Items)
            {
                if (listItem.Selected)
                {
                    checkboxSelection.Add(listItem.Text);
                }
            }
            for (int i = 0; i < colNameDT.Rows.Count; i++)
            {
                //syntax dt.Rows[rowindex][columnName/columnIndex]
                if (i == 0)
                {
                    tableNames = colNameDT.Rows[i]["nameOfTable"].ToString();
                    columns = checkboxSelection[0];
                    checkboxSelection.RemoveAt(0);

                }
                else if (i == colNameDT.Rows.Count - 1)
                {
                    if (!colNameDT.Rows[i]["nameOfTable"].ToString().Equals(colNameDT.Rows[i - 1]["nameOfTable"].ToString()))
                    {
                        tableNames += ", " + colNameDT.Rows[i]["nameOfTable"].ToString() + " ";
                    }

                    foreach (string listItem in checkboxSelection)
                    {
                        if (listItem == colNameDT.Rows[i]["nameOfColumn"].ToString())
                        {
                            columns += ", " + colNameDT.Rows[i]["nameOfColumn"].ToString() + " ";
                        }
                    }

                }
                else
                {
                    if (!colNameDT.Rows[i]["nameOfTable"].ToString().Equals(colNameDT.Rows[i - 1]["nameOfTable"].ToString()))
                    {
                        tableNames += ", " + colNameDT.Rows[i]["nameOfTable"].ToString();
                    }

                    foreach (string listItem in checkboxSelection)
                    {
                        if (listItem == colNameDT.Rows[i]["nameOfColumn"].ToString())
                        {
                            columns += ", " + colNameDT.Rows[i]["nameOfColumn"].ToString();
                        }
                    }
                }
            }
            // insert query string here
            string query = "SELECT " + columns + " FROM " + tableNames;
            return query;
        }

        protected void AddFilterBtn_Click(object sender, EventArgs e)
        {
            filterTablePlaceHolder.Visible = true;
            //addFilter.Enabled = false;
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked == true)
            {
                selectCount.Visible = true;
            }
            else
            {
                selectCount.Visible = false;
            }
        }

        // edit this save button to resubmit data on same page.
        protected void Button1_Click(object sender, EventArgs e)
        {
            string query = QueryBuilder();
            Session["rptTitle"] = lblRptTitle.Text;
            Session["rptDesc"] = lblRptDesc.Text;
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
    }
}