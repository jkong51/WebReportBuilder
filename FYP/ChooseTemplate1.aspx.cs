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
            Session.Add("query",query);
            Session.Add("wantDate", tDate);
            //check if show footer is checked.
            if (CheckBox3.Checked == true) {
                Session.Add("countTitle",selectCount.SelectedItem.Text);
            }
            // in the future, if more elements are added remember to generate a hidden field for each element intialized.
            Response.Redirect("~/DesignReport.aspx");
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
            foreach (DataRow row in dt.Rows) {
                rowType = getColumnType(row["nameOfColumn"].ToString(),row["nameOfTable"].ToString());
            }
            conditionDDL.Items.Clear();
            switch (rowType) {
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
                    //    if (j == 0) {
                    //        Session.Add("nameOfTable", row["nameOfTable"].ToString());
                    //        j++;
                    //    }
                        rowType = getColumnType(row["nameOfColumn"].ToString(), row["nameOfTable"].ToString());
                    }
                    if (rowType == "int" || rowType == "double" || rowType == "decimal") {
                        selectCount.Items.Add(cbItem);
                        selectCount.SelectedIndex = 0;
                    }
                }
            }
            if (CheckBoxList1.SelectedIndex == -1) {
                filterTablePlaceHolder.Visible = false;
            }
        }

        //query builder
        private string QueryBuilder() {
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
                    //foreach (string listItem in checkboxSelection)
                    //{
                    //    if (listItem == colNameDT.Rows[i]["nameOfColumn"].ToString())
                    //    {
                            
                    //    }
                    //}

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
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked == true) {
                selectCount.Visible = true;
            }
            else {
                selectCount.Visible = false;
            }
        }

        protected void selectFilter_CheckedChanged(object sender, EventArgs e)
        {

        }
    }


}
