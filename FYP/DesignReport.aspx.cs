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

namespace FYP
{
    public partial class DesignReport : System.Web.UI.Page
    {
        private Style primaryStyle = new Style();
        protected string PostBackString;
        protected void Page_Load(object sender, EventArgs e)
        {
            PostBackString = Page.ClientScript.GetPostBackEventReference(this,"getPos");
            if (Page.IsPostBack) {
                BtnSave_Click();
                lblRptTitle.Text = txtRptTitle.Text;
                lblRptDesc.Text = txtRptDesc.Text;
            }
            if (!Page.IsPostBack)
            {

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

        protected void BtnSave_Click()
        {
            // get all controls
            string query = Session["query"].ToString();
            string titlePosition = hiddenRptTitle.Value;
            string descPosition = hiddenRptDesc.Value;

            if (hiddenRptDate.Value == "") { 
            string datePosition = hiddenRptDate.Value;
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

        protected void BtnClear_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseTemplate1.aspx");
        }
    }
}