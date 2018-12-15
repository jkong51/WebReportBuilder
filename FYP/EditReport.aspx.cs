using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            public string Width { get; set; }

            public header_element(int reportID, string val, string eleType, string xPos, string yPos, string fontType, string width)
            {
                ReportID = reportID;
                Value = val;
                EleType = eleType;
                XPos = xPos;
                YPos = yPos;
                FontType = fontType;
                Width = width;
            }
        }

        private Style primaryStyle = new Style();
        protected static string reportId;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            if (!ClientScript.IsClientScriptBlockRegistered("EscapeText"))
            {
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
                //Rebind gridview
                SaveState();
                DataTable formTable = ViewState["formTable_data"] as DataTable;
                reportGridView.DataSource = formTable;
                if (Session["countTitle"] != null)
                {
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
                foreach (FontFamily font in FontFamily.Families)
                {
                    fontFamilyDrpDwnList.Items.Add(font.Name.ToString());
                }
                Dictionary<int, object> headerEleDictionary = getHeadEle(System.Convert.ToInt32(Session["reportId"].ToString()));
                foreach (int key in headerEleDictionary.Keys)
                {
                    //Label newLabel = new Label();
                    header_element headEle = (header_element)headerEleDictionary[key];
                    //set title
                    if (headEle.EleType == "title")
                    {
                        //label is title
                        lblRptTitle.Text = headEle.Value;
                        txtRptTitle.Text = headEle.Value;
                        hiddenRptTitle.Value = headEle.XPos + "," + headEle.YPos;
                        lblRptTitle.Font.Name = headEle.FontType;
                        lblRptTitle.Attributes.Add("style", " position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;");
                        reportGridView.Font.Name = headEle.FontType;
                        fontFamilyDrpDwnList.SelectedIndex = fontFamilyDrpDwnList.Items.IndexOf(fontFamilyDrpDwnList.Items.FindByText(headEle.FontType));
                    }
                    else if (headEle.EleType == "desc")
                    {
                        //lbl1 is desc. lbl2 is date(if have)
                        lblRptDesc.Text = headEle.Value;
                        txtRptDesc.Text = headEle.Value;
                        hiddenRptDesc.Value = headEle.XPos + "," + headEle.YPos;
                        lblRptDesc.Font.Name = headEle.FontType;
                        lblRptDesc.Attributes.Add("style", " position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;");
                    }
                    else if (headEle.EleType == "date") {
                        lblDate.Text = headEle.Value;
                        hiddenRptDate.Value = headEle.XPos + "," + headEle.YPos;
                        lblDate.Font.Name = headEle.FontType;
                        lblDate.Attributes.Add("style", " position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;");
                    }
                    else if (headEle.EleType == "line")
                    {
                        chkHrVis.Checked = true;
                        //hrLine.Attributes.Add("style", "position:absolute; top:" + coords[1] + "px; left:" + coords[0] + "px;" + "width:" + hiddenLineWidth.Value + "px;");
                        hrLine.Attributes.Add("style", "position:absolute; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "width:" + headEle.Width + "px;");
                        hiddenLineWidth.Value = headEle.Width;
                        hiddenLinePosition.Value = headEle.XPos + "," + headEle.YPos;
                    }
                }
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        //SELECT fe.[value], fe.[eleTypeId] FROM Footer_element fe
                        string sql = "SELECT imagePath, width, height, xPosition, yPosition FROM Header_image where reportID = @reportID";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        using (cmd)
                        {
                            cmd.Parameters.AddWithValue("@reportID", Session["reportId"].ToString());
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                chkImg.Checked = true;
                                imgprw.ImageUrl = reader["imagePath"].ToString();
                                imgFrame.Attributes.Add("style", "position:absolute; top:" + reader["yPosition"].ToString() + "px; left:" + reader["xPosition"].ToString() + "px;" + "width:" + reader["width"].ToString() + "px;" + "height:" + reader["height"].ToString() + "px;");
                                hiddenImage.Value = reader["xPosition"].ToString() + "," + reader["yPosition"].ToString();
                                hiddenHeight.Value = reader["height"].ToString();
                                hiddenWidth.Value = reader["width"].ToString();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                }
                reportId = Session["reportId"].ToString();
                hiddenFormID.Value = getFormID(Convert.ToInt32(reportId));
                List<string> checkedList = new List<string>();
                if (Session["checkedItems"] != null)
                {
                    ListItemCollection cbList = Session["cbListItems"] as ListItemCollection;
                    foreach (ListItem li in cbList)
                    {
                        ColumnCbList.Items.Add(li);
                    }
                    ViewState["selectedCbList"] = (List<string>)Session["checkedItems"];
                }
                else {
                    checkedList = initCheckBoxList(hiddenFormID.Value, reportId);
                    ViewState["selectedCbList"] = checkedList;
                }

                if (String.IsNullOrEmpty(Convert.ToString(Request.QueryString["queryString"])))
                {
                    sqlQuery.Value = getQuery(Session["reportId"].ToString());
                }
                else {
                    sqlQuery.Value = Convert.ToString(Request.QueryString["queryString"]);
                }
                string query = sqlQuery.Value;
                DataTable formTable = getFormData(query);
                //check if footer exists for this
                if (Session["countTitle"] == null && Session["footerEnabled"] == null)
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
                else if(Session["countTitle"] != null)
                {
                    reportGridView.ShowFooter = true;
                    // save footer row here
                    Session["footerName"] = Session["countTitle"].ToString();
                    Session["footerEnabled"] = "true";
                }
                if (Session["isRedirect"] != null)
                {
                    if ((Boolean)Session["isRedirect"] == true)
                    {
                        // re initalize hidden fields
                        if (Session["imgPathSession"] != null)
                        {
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
                reportGridView.DataSource = formTable;
                ViewState["formTable_data"] = formTable;
                reportGridView.DataBind();
            }
        }

        protected void SaveState()
        {
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
            if (chkHrVis.Checked)
            {
                ViewState["hrPos"] = hiddenLinePosition.Value;
                ViewState["hrWidth"] = hiddenLineWidth.Value;
            }
            else if (!chkHrVis.Checked)
            {
                ViewState["hrPos"] = null;
                ViewState["hrWidth"] = null;
            }
        }

        protected void LoadState()
        {
            string[] coords = null;
            if (Session["imgPathSession"] != null && ViewState["imgPath"] == null)
            {
                coords = Regex.Split(hiddenImage.Value, ",");
                ViewState["imgPath"] = Session["imgPathSession"].ToString();
                Session["imgPathSession"] = null;
                coords = null;
            }
            if (chkHrVis.Checked)
            {
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

        private Dictionary<int, object> getHeadEle(int reportID)
        {
            Dictionary<int, object> headerEleDictionary = new Dictionary<int, object>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT he.value, et.name, he.xPosition, he.yPosition, et.fontType, he.width " +
                        "FROM Header_element he, Element_type et WHERE he.eleTypeId = et.eleTypeId AND he.reportID = @reportID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@reportID", reportID);
                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (sqlDataReader.Read())
                        {
                            header_element headEle = new header_element(reportID, sqlDataReader["value"].ToString(), sqlDataReader["name"].ToString(), sqlDataReader["xPosition"].ToString(), sqlDataReader["yPosition"].ToString(), sqlDataReader["fontType"].ToString(), sqlDataReader["width"].ToString());
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
            primaryStyle.Font.Name = fontFamilyDrpDwnList.SelectedItem.Text;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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

            //check if session footerEnabled == true
            if (Session["countTitle"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    string query = "";
                    if (String.IsNullOrEmpty(Convert.ToString(Request.QueryString["queryString"]))) {
                        query = sqlQuery.Value;
                    }
                    else {
                        query = Convert.ToString(Request.QueryString["queryString"]);
                    }
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
                    if (count > 0) { 
                    e.Row.Cells[count - 1].Controls.Add(new Literal() { Text = "Total :" });
                    e.Row.Cells[count - 1].HorizontalAlign = HorizontalAlign.Right;
                    }
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
            if (CheckBox3.Checked == false)
            {
                selectCount.Visible = false;
                Label5.Visible = false;
            }
            else
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
        }

        private string QueryBuilder()
        {
            //check if filter option is selected.
            //checks if dropdownlist item is selected
             DataTable dt = getMappingData(hiddenFormID.Value);
             string query = getColAndTable(dt);
             return query;
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
            else {
                Session["countTitle"] = null;
                Session["footerEnabled"] = "false";
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

            if (chkHrVis.Checked)
            {
                Session["linePos"] = hiddenLinePosition.Value;
                Session["lineWidth"] = hiddenLineWidth.Value;
            }
            Session["isRedirect"] = true;

            //set back values for hidden fields
            Session["hiddenRptTitle"] = hiddenRptTitle.Value;
            Session["hiddenRptDesc"] = hiddenRptDesc.Value;
            if (hiddenRptDate.Value != "")
            {
                Session["hiddenRptDate"] = hiddenRptDate.Value;
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
            sql = "UPDATE Header_element " + "SET value = @value, xPosition = @xPos, yPosition = @yPos, width = @width " + "WHERE headerID = @headerID";
            rowsAffected = getHeaderEle(sql, headerIdList);

            if ((string)Session["footerEnabled"] == "true")
            {
                string footerName = Session["footerName"].ToString();
                ReportElement footerElement = new ReportElement(Convert.ToInt32(reportId), footerName, "", "", "footer", fontFamilyDrpDwnList.SelectedItem.Text,"");
                updateFooterEle(footerElement);
            }

            //update image if new one is uploaded
            if (fileuploadASP.HasFile) {
                sql = "UPDATE Header_image " + "SET imagePath = @imagePath, width = @width, height = @height, xPosition = @xPosition. yPosition = @yPosition " + "WHERE reportID = @reportID";
                //x = coords[0] y = coords[1]
                string[] coords = Regex.Split(hiddenImage.Value, ",");
                rowsAffected = updateImage(sql, reportId, coords);
            }
            Response.Write("<script>alert('" + "Report saved successfully." + "')</script>");
            Response.Redirect("Homepage.aspx");
        }

        protected int updateImage(string sql,string reportID,string[] coords)
        {
            int rows;
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@imagePath", ViewState["imgPath"].ToString());
                cmd.Parameters.AddWithValue("@width", hiddenWidth.Value);
                cmd.Parameters.AddWithValue("@height", hiddenHeight.Value);
                cmd.Parameters.AddWithValue("@xPosition", coords[0]);
                cmd.Parameters.AddWithValue("@yPosition", coords[1]);
                cmd.Parameters.AddWithValue("@reportID", reportID);
                rows = cmd.ExecuteNonQuery();
            }
            return rows;
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
                    cmd.Parameters.AddWithValue("@width",reportEle.Width);
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

        protected void updateFooterEle(ReportElement reportEle)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE Footer_element SET value = @value WHERE reportID = @reportID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@value", reportEle.Value);
                    cmd.Parameters.AddWithValue("@reportID", reportEle.ReportID);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
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
            // title of report
            string[] coords = Regex.Split(hiddenRptTitle.Value, ",");
            ReportElement reportEleTitle = new ReportElement(Convert.ToInt32(reportId), txtRptTitle.Text, coords[0], coords[1], "title", fontFamilyDrpDwnList.SelectedItem.Text,"");
            parameters.Add(reportEleTitle);
            coords = null;
            //desc of report
            coords = Regex.Split(hiddenRptDesc.Value, ",");
            ReportElement reportEleDesc = new ReportElement(Convert.ToInt32(reportId), txtRptDesc.Text, coords[0], coords[1], "desc", fontFamilyDrpDwnList.SelectedItem.Text,"");
            parameters.Add(reportEleDesc);
            //date of report
            if (lblDate.Text != "")
            {
                coords = null;
                coords = Regex.Split(hiddenRptDate.Value, ",");
                ReportElement reportEleDate = new ReportElement(Convert.ToInt32(reportId), lblDate.Text, coords[0], coords[1], "date", fontFamilyDrpDwnList.SelectedItem.Text,"");
                parameters.Add(reportEleDate);
            }
            if (chkHrVis.Checked) {
                coords = null;
                coords = Regex.Split(hiddenRptDate.Value, ",");
                ReportElement reportEleLine = new ReportElement(Convert.ToInt32(reportId), "", coords[0], coords[1], "line", "", hiddenLineWidth.Value);
                parameters.Add(reportEleLine);
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

        protected List<string> initCheckBoxList(string formId, string reportId) {
            List<string> newQueryStringList = new List<string>();
            DataTable dt = getMappingData(formId);
            ColumnCbList.DataValueField = "mappingId";
            ColumnCbList.DataTextField = "nameOfColumn";
            ColumnCbList.DataSource = dt;
            ColumnCbList.DataBind();
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
                        //checkedList.Add(CheckBoxList1.Items[i].Text);
                        List<string> oldQueryStringList = qry.Split(' ').ToList();
                        // remove all the other elements besides the column names
                        for (int i = 0; i < oldQueryStringList.Count;i++)
                        {
                            if (oldQueryStringList[i] == "SELECT")
                            {
                                oldQueryStringList.RemoveAt(i);
                                i--;
                            }
                            else if (oldQueryStringList[i] == "FROM")
                            {
                                //remove all elements after FROM
                                oldQueryStringList.RemoveRange(i, oldQueryStringList.Count-i);
                                break;
                            }
                            else {
                                // get rid of the comma
                                string oldString = oldQueryStringList[i];
                                string newString = oldString.Trim(',');
                                newQueryStringList.Add(newString);
                            }
                        }
                        //SELECT dateSubmitted, itemName, quantity  FROM Computer_Parts_Survey
                        for (int i = 0; i < ColumnCbList.Items.Count;i++) {
                            if (Regex.IsMatch(qry, ColumnCbList.Items[i].Text))
                            {
                                ColumnCbList.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
            return newQueryStringList;
        }

        protected void reportGridView_DataBound(object sender, EventArgs e)
        {
            if (Session["footerEnabled"] != null)
            {
                if (Session["footerEnabled"].ToString() == "true")
                {
                    reportGridView.FooterRow.Visible = this.reportGridView.PageIndex == this.reportGridView.PageCount - 1;
                }
            }
        }
    }
}