using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class BlankTemplateOption : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Visible = true;
            CheckBoxList1.Visible = true;
            string connStr = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
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