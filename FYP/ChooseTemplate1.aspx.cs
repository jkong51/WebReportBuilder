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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String rptTitle = txtRptTitle.Text;
            String rptDesc = txtRptDesc.Text;            
            Session.Add("rptTitle", rptTitle);
            Session.Add("rptDesc", rptDesc);
            String tDate = "yes";
            if (chkDate.Checked)
            {
                tDate = "yes";
            }
            else
            {
                tDate = "no";
            }

            List<string> checkboxSelection = new List<string>();
            foreach (ListItem listItem in CheckBoxList1.Items) {
                if (listItem.Selected) {
                    checkboxSelection.Add(listItem.Value);
                }
            }
            // add error message if first option is selected.
            Session["SelectedTables"] = checkboxSelection;
            Session.Add("wantDate", tDate);
            Response.Redirect("~/DesignReport.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Visible = true;
            CheckBoxList1.Visible = true;
            DataTable dt = getMappingData(DropDownList1.SelectedValue);
            CheckBoxList1.DataValueField = "mappingId";
            CheckBoxList1.DataTextField = "colName";
            CheckBoxList1.DataSource = dt;
            CheckBoxList1.DataBind();
        }

        protected void SelectedItemDDL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // optimize this
            DataTable dt = getDBInfo(selectedItemDDL1.SelectedValue);
            string rowType = "";
            // returns only a single record, so change DT to something else later.
            foreach (DataRow row in dt.Rows) {
                rowType = getColumnType(row["colName"].ToString(),row["tableName"].ToString());
            }
            switch (rowType) {
                case "string":
                    DropDownList conditionStringDDL = new DropDownList();
                    conditionStringDDL.Items.Insert(0, new ListItem("equals", "="));
                    conditionStringDDL.Items.Insert(1, new ListItem("does not equal", "<>"));
                    filterPlaceholder.Controls.Add(conditionStringDDL);
                    break;
                case "date":
                    DropDownList dateStringDDL = new DropDownList();
                    dateStringDDL.Items.Insert(0, new ListItem("equals", "="));
                    dateStringDDL.Items.Insert(1, new ListItem("less than", "<"));
                    dateStringDDL.Items.Insert(2, new ListItem("more than", ">"));
                    dateStringDDL.Items.Insert(3, new ListItem("more or equal than", ">="));
                    dateStringDDL.Items.Insert(4, new ListItem("less or equal than", "<="));
                    filterPlaceholder.Controls.Add(dateStringDDL);
                    break;
                case "int":
                    DropDownList intStringDDL = new DropDownList();
                    intStringDDL.Items.Insert(0, new ListItem("equals", "="));
                    intStringDDL.Items.Insert(1, new ListItem("less than", "<"));
                    intStringDDL.Items.Insert(2, new ListItem("more than", ">"));
                    intStringDDL.Items.Insert(3, new ListItem("more or equal than", ">="));
                    intStringDDL.Items.Insert(4, new ListItem("less or equal than", "<="));
                    filterPlaceholder.Controls.Add(intStringDDL);
                    break;
                case "decimal":
                    DropDownList decimalStringDDL = new DropDownList();
                    decimalStringDDL.Items.Insert(0, new ListItem("equals", "="));
                    decimalStringDDL.Items.Insert(1, new ListItem("less than", "<"));
                    decimalStringDDL.Items.Insert(2, new ListItem("more than", ">"));
                    decimalStringDDL.Items.Insert(3, new ListItem("more or equal than", ">="));
                    decimalStringDDL.Items.Insert(4, new ListItem("less or equal than", "<="));
                    filterPlaceholder.Controls.Add(decimalStringDDL);
                    break;
                case "double":
                    DropDownList doubleStringDDL = new DropDownList();
                    doubleStringDDL.Items.Insert(0, new ListItem("equals", "="));
                    doubleStringDDL.Items.Insert(1, new ListItem("less than", "<"));
                    doubleStringDDL.Items.Insert(2, new ListItem("more than", ">"));
                    doubleStringDDL.Items.Insert(3, new ListItem("more or equal than", ">="));
                    doubleStringDDL.Items.Insert(4, new ListItem("less or equal than", "<="));
                    filterPlaceholder.Controls.Add(doubleStringDDL);
                    break;
                default:
                    break;
            }

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItemDDL1.Items.Clear();
            ListItem firstele = new ListItem("", "", true);
            selectedItemDDL1.Items.Add(firstele);
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    ListItem cbItem = new ListItem(CheckBoxList1.Items[i].Text, CheckBoxList1.Items[i].Value);
                    selectedItemDDL1.Items.Add(cbItem);
                }
            }
            if (CheckBoxList1.SelectedIndex == -1) {
                filterTablePlaceHolder.Visible = false;
            }
        }

        // add dbName to param when needed
        private string getColumnType(string colName , string tableName)
        {
            //change this to use with any different form databases.
            
            string connStr = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = string.Format("SELECT {0} FROM {1}", "[" + colName + "]", "[" + tableName + "]" );
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
                SqlCommand cmd = new SqlCommand("SELECT mappingId, colName, tableName FROM Mapping WHERE formId = @formId", con);
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
                SqlCommand cmd = new SqlCommand("SELECT dbName, colName, tableName FROM Mapping WHERE mappingId = @mappingId", con);
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

        protected void EnableFilterBtn_Click(object sender, EventArgs e)
        {

        }

        protected void AddFilterBtn_Click(object sender, EventArgs e)
        {
            filterTablePlaceHolder.Visible = true;
        }
    }
}
