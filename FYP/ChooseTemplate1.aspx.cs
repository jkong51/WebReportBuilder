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

            Session["SelectedTables"] = checkboxSelection;
            Session.Add("wantDate", tDate);
            Response.Redirect("~/DesignReport.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Visible = true;
            CheckBoxList1.Visible = true;
            string connStr = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // think about getting and passing formId if needed
                SqlCommand cmd = new SqlCommand("SELECT mappingId, colName FROM Mapping WHERE formId = @formId", con);
                cmd.Parameters.AddWithValue("@formId", DropDownList1.SelectedValue);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    CheckBoxList1.DataValueField = "mappingId";
                    CheckBoxList1.DataTextField = "colName";
                    CheckBoxList1.DataSource = reader;
                    CheckBoxList1.DataBind();
                }
            }
        }

    }
}
