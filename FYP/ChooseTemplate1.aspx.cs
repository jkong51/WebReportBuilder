using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FYP
{
    public partial class ChooseTemplate1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            String formName = DropDownList1.SelectedItem.Text;
            Session.Add("formName", formName);
            String rptTitle = txtRptTitle.Text;
            String rptDesc = txtRptDesc.Text;            
            Session.Add("rptTitle", rptTitle);
            Session.Add("rptDesc", rptDesc);
            String tDate = "";
            if (chkDate.Checked)
            {
                tDate = "yes";
            }
            else
            {
                tDate = "no";
            }
            // build sql query here.
            string query = QueryBuilder();
            // add error message if first option is selected.
            Session.Add("wantDate", tDate);

            // add the checkbox list items to session
            Session.Add("cbListItems", ColumnCbList.Items);
            // add the ordered checked items to session
            Session.Add("checkedItems", (List<string>)ViewState["selectedCbList"]);
            Session.Add("formID", DropDownList1.SelectedValue);
            //check if show footer is checked.
            if (countChkBox.Checked == true) {
                Session.Add("countTitle",selectCount.SelectedItem.Text);
            }
            // in the future, if more elements are added remember to generate a hidden field for each element intialized.
            Response.Redirect("~/DesignReport.aspx?queryString=" + query);
        }

        // display check box list items when choose form ddl is selected
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Visible = true;
            ColumnCbList.Visible = true;
            DataTable dt = getMappingData(DropDownList1.SelectedValue);
            ColumnCbList.DataValueField = "mappingId";
            ColumnCbList.DataTextField = "nameOfColumn";
            ColumnCbList.DataSource = dt;
            ColumnCbList.DataBind();
            ViewState["selectedCbList"] = null;
        }
        

        // add items to filter dropdownlist if any checkbox item is checked.
        protected void ColumnCbList_SelectedIndexChanged(object sender, EventArgs e)
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
            else if(!ColumnCbList.Items[index].Selected)
            {
                value = ColumnCbList.Items[index].Text;
                checkedList.Remove(value);
            }
            ViewState["selectedCbList"] = checkedList;

            if (countChkBox.Checked == true)
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
                else {
                    selectCount.Visible = false;
                    Label5.Visible = false;
                }
            }
            else
            {
                selectCount.Visible = false;
                Label5.Visible = false;
            }
        }

        //query builder
        private string QueryBuilder() {
            //check if filter option is selected.
            //checks if dropdownlist item is selected
            DataTable dt = getMappingData(DropDownList1.SelectedValue);
            string query = getColAndTable(dt);
            return query;
            
        }


        // add dbName to param when needed
        private string getColumnType(string nameOfColumn , string nameOfTable)
        {
            //change this to use with any different form databases.
            string connStr = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = string.Format("SELECT {0} FROM {1}", "[" + nameOfColumn + "]", "[" + nameOfTable + "]" );
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    System.Type type = reader.GetFieldType(0);
                    switch (Type.GetTypeCode(type)) {
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
        private DataTable getMappingData(string formId) {
            string connStr = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // think about getting and passing formId if needed
                string query = "SELECT mappingId, nameOfColumn, nameOfTable FROM Mapping WHERE formId = @formId";
               
                
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@formId", formId);
                //if (itemSelected == true) {
                //    cmd.Parameters.AddWithValue("@formId2", formId);
                //}
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
                    //checkboxSelection.RemoveAt(0);
                    }
                    // will only enter this segment if it is the last item on checkBoxSelection
                    else if (i == colNameDT.Rows.Count - 1)
                    {
                        if (!colNameDT.Rows[i]["nameOfTable"].ToString().Equals(colNameDT.Rows[i - 1]["nameOfTable"].ToString()))
                        {
                            tableNames += ", " + colNameDT.Rows[i]["nameOfTable"].ToString() + " ";
                        }
                    }
                    //enter this segment only if it isn't the first or last item in checkBoxSelection
                    else
                    {
                        //in case there are multiple tables required. (future improvement)
                        if (!colNameDT.Rows[i]["nameOfTable"].ToString().Equals(colNameDT.Rows[i - 1]["nameOfTable"].ToString()))
                        {
                            tableNames += ", " + colNameDT.Rows[i]["nameOfTable"].ToString();
                        }
                    }
            }
            // ignore first element as it has been added already
            Boolean ignoreFirstElement = false ;
            foreach (string listItem in checkboxSelection)
            {
                if (ignoreFirstElement == false)
                {
                    ignoreFirstElement = true;
                }
                else {
                    columns += ", " + listItem;
                }
            }
            // insert query string here
            string query = "SELECT " + columns + " FROM " + tableNames;
            return query;
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (countChkBox.Checked == false) {
                selectCount.Visible = false;
                Label5.Visible = false;
            }
            else {
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
    }
}
