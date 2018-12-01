﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{

    public partial class EditReport : System.Web.UI.Page
    {
        private class header_element
        {
            public int ReportID { get; set; }
            public string Value { get; set; }
            public string EleType { get; set; }
            public string XPos { get; set; }
            public string YPos { get; set; }
            public string FontType { get; set; }

            public header_element(int reportID, string val, string eleType, string xPos, string yPos, string fontType)
            {
                ReportID = reportID;
                Value = val;
                EleType = eleType;
                XPos = xPos;
                YPos = yPos;
                FontType = fontType;
            }
        }

        private Style primaryStyle = new Style();
        protected static string reportId;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            if (!Page.IsPostBack)
            {
                Dictionary<int, object> headerEleDictionary = getHeadEle(System.Convert.ToInt32(Session["reportId"].ToString()));
                foreach (int key in headerEleDictionary.Keys)
                {
                    //Label newLabel = new Label();
                    header_element headEle = (header_element)headerEleDictionary[key];
                    //set title
                    if (key == 0)
                    {
                        //label is title
                        lblRptTitle.Text = headEle.Value;
                        txtRptTitle.Text = headEle.Value;
                        hiddenRptTitle.Value = headEle.XPos + "," + headEle.YPos;
                        lblRptTitle.Attributes.Add("style", " position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "font-family: '" + headEle.FontType + "';");
                    }
                    else if (key == 1)
                    {
                        //lbl1 is desc. lbl2 is date(if have)
                        lblRptDesc.Text = headEle.Value;
                        txtRptDesc.Text = headEle.Value;
                        hiddenRptDesc.Value = headEle.XPos + "," + headEle.YPos;
                        lblRptDesc.Attributes.Add("style", " position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "font-family: '" + headEle.FontType + "';");
                    }
                    else if (key == 2) {
                        lblDate.Text = headEle.Value;
                        hiddenRptDate.Value = headEle.XPos + "," + headEle.YPos;
                        lblDate.Attributes.Add("style", " position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "font-family: '" + headEle.FontType + "';");
                    }
                    //newLabel.Attributes.Add("style", " position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "font-family: '" + headEle.FontType + "';");
                    //reportHeader.Controls.Add(newLabel);
                }
                reportId = Session["reportId"].ToString();
                hiddenFormID.Value = getFormID(Convert.ToInt32(reportId));
                initCheckBoxList(hiddenFormID.Value,reportId);
                if (String.IsNullOrEmpty(Convert.ToString(Request.QueryString["queryString"])))
                {
                    sqlQuery.Value = getQuery(Session["reportId"].ToString());

                }
                else {
                    sqlQuery.Value = Convert.ToString(Request.QueryString["queryString"]);
                }
                string query = sqlQuery.Value;
                DataTable formTable = getFormData(query);
                ViewState["formTable_data"] = formTable;
                reportGridView.DataSource = formTable;
                //check if footer exists for this
                if (Session["countTitle"] == null)
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            //SELECT fe.[value], fe.[eleTypeId] FROM Footer_element fe
                            string sql = "SELECT value FROM Footer_element where reportID = @reportID";
                            SqlCommand cmd = new SqlCommand(sql, con);
                            using (cmd) {
                                cmd.Parameters.AddWithValue("@reportID", reportId);
                                SqlDataReader reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    Session["countTitle"] = reader["value"].ToString();
                                }
                            }
                        }
                    }
                    catch (SqlException ex) {

                    }
                }
                reportGridView.DataBind();

                foreach (FontFamily font in FontFamily.Families)
                {
                    fontFamilyDrpDwnList.Items.Add(font.Name.ToString());
                }


            }
        }

        private Dictionary<int, object> getHeadEle(int reportID)
        {
            Dictionary<int, object> headerEleDictionary = new Dictionary<int, object>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT he.value, et.name, he.xPosition, he.yPosition, et.fontType " +
                        "FROM Header_element he, Element_type et WHERE he.eleTypeId = et.eleTypeId AND he.reportID = @reportID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@reportID", reportID);
                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (sqlDataReader.Read())
                        {
                            header_element headEle = new header_element(reportID, sqlDataReader["value"].ToString(), sqlDataReader["name"].ToString(), sqlDataReader["xPosition"].ToString(), sqlDataReader["yPosition"].ToString(), sqlDataReader["fontType"].ToString());
                            headerEleDictionary.Add(i, headEle);
                            i++;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // catch error here
            }
            return headerEleDictionary;
        }
        

        protected void ChangeFont(object sender, EventArgs e)
        {
            //fix change font
            primaryStyle.Font.Name = fontFamilyDrpDwnList.SelectedItem.Text;
            //foreach (Control c in updatePanel1.Controls) {
            //    if (c.ID == "lbl0") {
            //        ((Label)c).ApplyStyle(primaryStyle);
            //    }
            //    else if (c.ID == "lbl1") {
            //        ((Label)c).ApplyStyle(primaryStyle);
            //    }
            //    else if (c.ID == "lbl2") {
            //        ((Label)c).ApplyStyle(primaryStyle);
            //    }
            //}
            lblRptTitle.ApplyStyle(primaryStyle);
            lblRptDesc.ApplyStyle(primaryStyle);
            lblDate.ApplyStyle(primaryStyle);
            reportGridView.ApplyStyle(primaryStyle);
        }

        private string getQuery(string reportID)
        {
            string connStrReportDB = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            string connStrFormDB = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;
            DataTable dt = new DataTable();
            string qry = "";
            using (SqlConnection con = new SqlConnection(connStrReportDB))
            {
                con.Open();
                string getQuery = "SELECT query FROM Report_body WHERE reportID = @reportID";
                using (SqlCommand cmd = new SqlCommand(getQuery, con))
                {
                    cmd.Parameters.AddWithValue("@reportID", reportID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            qry = reader["query"].ToString();
                        }
                    }
                }
            }
            return qry;
        }

        protected void reportGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //check if session footerEnabled == true
            if (Session["countTitle"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    string query = sqlQuery.Value;
                    DataTable formTable = getFormData(query);
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
                    e.Row.Cells[count - 1].Controls.Add(new Literal() { Text = "Total :" });
                    e.Row.Cells[count - 1].HorizontalAlign = HorizontalAlign.Right;
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

        private string QueryBuilder()
        {
            //check if filter option is selected.
            //checks if dropdownlist item is selected

             DataTable dt = getMappingData(hiddenFormID.Value);
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

        // edit this save button to resubmit data on same page.
        protected void Button1_Click(object sender, EventArgs e)
        {
            string qry = QueryBuilder();
            Session["rptTitle"] = lblRptTitle.Text;
            Session["rptDesc"] = lblRptDesc.Text;
            string wantDate = "";
            if (lblDate.Text == "")
            {
                wantDate = "no";
            }
            else
            {
                wantDate = "yes";
            }
            Session["wantDate"] = wantDate;
            if (CheckBox3.Checked == true)
            {
                Session["countTitle"] = selectCount.SelectedItem.Text;
            }
            Response.Redirect("~/EditReport.aspx?queryString=" + qry);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            /*
             Variables to get
             Report
             - reportID, date, description, title
             - Identify type of element, and create eleTypeId
             - Header_element, store all elements in header + positions. (seperate the position (X,Y))
             - Report_body, store query
             - Element_type, add name, fonttype
             */
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //get data for report
            parameters.Add("@name", txtRptTitle.Text);
            parameters.Add("@description", txtRptDesc.Text);
            parameters.Add("@reportId",reportId);
            string sql = "UPDATE Report " + "SET name = @name, description = @description WHERE reportId = @reportId";
            int rowsAffected = updateRecord(sql,parameters);
            parameters.Clear();
            string query = sqlQuery.Value;
            parameters.Add("@query",query);
            parameters.Add("@reportID",Convert.ToInt32(reportId));
            sql = "UPDATE Report_body " + "SET query = @query WHERE reportID = @reportID";
            rowsAffected = updateRecord(sql,parameters);

            List<int> headerIdList = getHeaderEleID(reportId);
            sql = "UPDATE Header_element " + "SET value = @value, xPosition = @xPos, yPosition = @yPos " + "WHERE headerID = @headerID";
            rowsAffected = getHeaderEle(sql, headerIdList);
            Response.Write("<script>alert('" + "Report saved successfully." + "')</script>");
           
            Response.Redirect("Homepage.aspx");
        }

        protected int updateRecord(string sql, Dictionary<string, object> parameters) {
            int rows = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                foreach (string key in parameters.Keys)
                {
                   cmd.Parameters.AddWithValue(key, parameters[key]);
                }
                    rows = cmd.ExecuteNonQuery();
            }
            return rows;
        }

        protected int updateHeaderEle(string sql, int headerId, ReportElement reportEle) {
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                int rowsreturned = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@value",reportEle.Value);
                    cmd.Parameters.AddWithValue("@xPos",reportEle.PosX);
                    cmd.Parameters.AddWithValue("@yPos",reportEle.PosY);
                    cmd.Parameters.AddWithValue("@headerID",headerId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                sql = "UPDATE Element_type SET fontType = @fontType WHERE eleTypeId = @eleTypeId";
                cmd.CommandText = sql;
                int eleTypeId = getEleTypeId(reportEle,headerId);
                using (cmd) {
                    cmd.Parameters.AddWithValue("@fontType",reportEle.FontType);
                    cmd.Parameters.AddWithValue("@eleTypeId",eleTypeId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    rowsreturned++;
                }
                return rowsreturned;
            }
        }

        protected int getEleTypeId(ReportElement reportEle,int headerId) {
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT eleTypeId FROM Header_element WHERE headerID = @headerId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@headerId",headerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        return Convert.ToInt32(reader.GetValue(0).ToString());
                    }
                }
            }
            return 0;
        }

        protected int getHeaderEle(string sql, List<int> headerIdList) {
            int rows = 0;
            
            List<ReportElement> parameters = new List<ReportElement>();
            string titlePosition = hiddenRptTitle.Value;
            // title of report
            string[] coords = Regex.Split(titlePosition, ",");
            ReportElement reportEleTitle = new ReportElement(Convert.ToInt32(reportId), txtRptTitle.Text, coords[0], coords[1], "label", lblRptTitle.Font.Name);
            parameters.Add(reportEleTitle);
            coords = null;
            //desc of report
            string descPosition = hiddenRptDesc.Value;
            coords = Regex.Split(descPosition, ",");
            ReportElement reportEleDesc = new ReportElement(Convert.ToInt32(reportId), txtRptDesc.Text, coords[0], coords[1], "label", lblRptDesc.Font.Name);
            parameters.Add(reportEleDesc);
            //date of report
            if (lblDate.Text != "")
            {
                coords = null;
                string datePosition = hiddenRptDate.Value;
                coords = Regex.Split(hiddenRptDate.Value, ",");
                ReportElement reportEleDate = new ReportElement(Convert.ToInt32(reportId), lblDate.Text, coords[0], coords[1], "label", lblDate.Font.Name);
                parameters.Add(reportEleDate);
            }
            int count = 0;
            int test = 0;
            foreach (int li in headerIdList) {
                test = updateHeaderEle(sql, li, parameters[count]);
                count++;
            }

            return rows;
        }

        protected List<int> getHeaderEleID(string rID) {
            List<int> headerIdList = new List<int>();
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT headerID FROM Header_element WHERE reportID = @reportID";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@reportID", rID);
                using (cmd) {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        headerIdList.Add((int)reader["headerID"]);
                    }
                }
            }
                return headerIdList;
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseReportUpdate.aspx");
        }

        protected string getFormID(int reportId) {
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT formId FROM Report WHERE reportID = @reportID";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@reportID", reportId);
                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        return reader["formId"].ToString();
                    }
                }
            }
            return null;
        }

        protected void initCheckBoxList(string formId, string reportId) {
            DataTable dt = getMappingData(formId);
            CheckBoxList1.DataValueField = "mappingId";
            CheckBoxList1.DataTextField = "nameOfColumn";
            CheckBoxList1.DataSource = dt;
            CheckBoxList1.DataBind();
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT query FROM Report_body WHERE reportID = @reportID";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@reportID", reportId);
                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string qry = reader["query"].ToString();
                        //SELECT dateSubmitted, itemName, quantity  FROM Computer_Parts_Survey
                        for (int i = 0; i < CheckBoxList1.Items.Count;i++) {
                            if (Regex.IsMatch(qry, CheckBoxList1.Items[i].Text))
                            {
                                CheckBoxList1.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }
    }
}