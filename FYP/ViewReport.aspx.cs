using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;


namespace FYP
{
    public partial class ViewReport : System.Web.UI.Page
    {
        private class header_element {
            public int ReportID { get; set; }
            public string Value { get; set; }
            public string EleType { get; set; }
            public string XPos { get; set; }
            public string YPos { get; set; }
            public string FontType { get; set; }

            public header_element(int reportID, string val, string eleType, string xPos, string yPos, string fontType) {
                ReportID = reportID;
                Value = val;
                EleType = eleType;
                XPos = xPos;
                YPos = yPos;
                FontType = fontType;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { 
            Dictionary<int, object> headerEleDictionary = getHeadEle(System.Convert.ToInt32(Session["reportId"].ToString()));
                int i = 0;
                foreach (int key in headerEleDictionary.Keys) {
                header_element headEle = (header_element)headerEleDictionary[key];
                    if (key == 0) {
                        lblTitle.CssClass = "reportHeader1";
                        lblTitle.Attributes.Add("style", " position:absolute;margin-left:-148px;margin-top:10px; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "font-family: '" + headEle.FontType + "';");
                        lblTitle.Text = headEle.Value;
                        reportGridView.Font.Name = headEle.FontType;
                        lblTitle.Font.Name = headEle.FontType;
                    }
                    else if (key == 1) {
                        lblDesc.CssClass = "reportHeader2";
                        lblDesc.Attributes.Add("style", " position:absolute;margin-left:-148px;margin-top:10px; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "font-family: '" + headEle.FontType + "';");
                        lblDesc.Text = headEle.Value;
                        lblDesc.Font.Name = headEle.FontType;
                    }
                    else if (key == 2) {
                        lblDate.CssClass = "reportHeader2";
                        lblDate.Font.Name = headEle.FontType;
                        lblDate.Attributes.Add("style", " position:absolute;margin-left:-148px;margin-top:10px; top:" + headEle.YPos + "px; left:" + headEle.XPos + "px;" + "font-family: '" + headEle.FontType + "';");
                        lblDate.Text = headEle.Value;
                    }
                }
                DataTable formTable = getFormData(Session["reportId"].ToString());
                ViewState["formTable_data"] = formTable;
                reportGridView.DataSource = formTable;
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        //SELECT fe.[value], fe.[eleTypeId] FROM Footer_element fe
                        string sql = "SELECT value FROM Footer_element where reportID = @reportID";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        using (cmd)
                        {
                            cmd.Parameters.AddWithValue("@reportID", Session["reportId"].ToString());
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                Session["countTitle"] = reader["value"].ToString();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                }
                reportGridView.DataBind();
            }
        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    //base.VerifyRenderingInServerForm(control);
        //}

        // in the future if more elements in header are added dynamically, write a function that counts the amount of elements that
        // need to be loaded first before calling getHeadEle

        private Dictionary<int,object> getHeadEle(int reportID) {
            Dictionary<int, object> headerEleDictionary = new Dictionary<int, object>();
            try {
                string connectionString = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;             
                using (SqlConnection con = new SqlConnection(connectionString)) {
                    con.Open();
                    string sql = "SELECT he.value, et.name, he.xPosition, he.yPosition, et.fontType " +
                        "FROM Header_element he, Element_type et WHERE he.eleTypeId = et.eleTypeId AND he.reportID = @reportID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@reportID",reportID);
                    using(SqlDataReader sqlDataReader = cmd.ExecuteReader()) {
                        int i = 0;
                        while (sqlDataReader.Read())
                        {
                            header_element headEle = new header_element(reportID,sqlDataReader["value"].ToString(), sqlDataReader["name"].ToString(), sqlDataReader["xPosition"].ToString(), sqlDataReader["yPosition"].ToString(), sqlDataReader["fontType"].ToString());
                            headerEleDictionary.Add(i, headEle);
                            i++;
                        }
                    }
                }
            }
            catch (SqlException ex) {
                // catch error here
            }
            return headerEleDictionary;
        }

        private DataTable getFormData(string reportID)
        {
            string connStrReportDB = ConfigurationManager.ConnectionStrings["FormNameConnectionString"].ConnectionString;
            string connStrFormDB = ConfigurationManager.ConnectionStrings["FormDBConnection"].ConnectionString;
            DataTable dt = new DataTable();
            string query = "";
            using (SqlConnection con = new SqlConnection(connStrReportDB)) {
                con.Open();
                string getQuery = "SELECT query FROM Report_body WHERE reportID = @reportID";
                using (SqlCommand cmd = new SqlCommand(getQuery,con)) {
                    cmd.Parameters.AddWithValue("@reportID",reportID);
                    using (SqlDataReader reader = cmd.ExecuteReader()){ 
                        if (reader.Read()) {
                            query = reader["query"].ToString();
                            Session["query"] = query;
                        }
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(connStrFormDB))
            {
                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    con.Open();
                    // if user uncheck filter, remove the condition
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseRetrieveUses.aspx");
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

        protected void reportGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Session["countTitle"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    DataTable formTable = getFormData(Session["reportID"].ToString());
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
                    e.Row.ID = "footerRowId";
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

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    StringWriter stringWriter = new StringWriter();
        //    HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        //    form1.RenderControl(htmlTextWriter);
        //    StringReader stringReader = new StringReader(stringWriter.ToString());
        //    Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //    HTMLWorker htmlparser = new HTMLWorker(Doc);
        //    PdfWriter.GetInstance(Doc, Response.OutputStream);
        //    Doc.Open();
        //    htmlparser.Parse(stringReader);
        //    Doc.Close();
        //    Response.Write(Doc);
        //    Response.End();
        //}
        //public override void VerifyRenderingInServerForm(Control control) { }


        //protected void btnExportPDF_Click(object sender, EventArgs e)
        //{
        //    ////Response.ContentType = "application/pdf";
        //    ////Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        //    ////Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    ////StringWriter sw = new StringWriter();
        //    ////HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    //////reportGridView.AllowPaging = false;
        //    ////printPDF.DataBind();
        //    ////printPDF.RenderControl(hw);
        //    ////StringReader sr = new StringReader(sw.ToString());
        //    ////Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //    ////HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    ////PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    ////pdfDoc.Open();
        //    ////htmlparser.Parse(sr);
        //    ////pdfDoc.Close();
        //    ////Response.Write(pdfDoc);
        //    ////Response.End();

        //    //DGVPrinter printer = new DGVPrinter();
        //    //printer.Title = "Inventory Report";
        //    //printer.SubTitle = "subTitle" + String.Format(DateTime.Now.ToString("yyyy"));
        //    //printer.TitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
        //    //printer.PageNumbers = true;
        //    //printer.PageNumberInHeader = false;
        //    //printer.PorportionalColumns = true;
        //    //printer.HeaderCellAlignment = StringAlignment.Near;
        //    //printer.Footer = "footer";
        //    //printer.FooterSpacing = 15;
        //    //printer.PrintDataGridView(reportGridView);
        //}

        //protected void ExportPdf_Click(object sender, EventArgs e)
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    printPDF.Page.RenderControl(hw);
        //    //containment-wrapper.Page.RenderControl(hw);
        //    StringReader sr = new StringReader(sw.ToString());
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    Response.End();
        //}
        /* 
         * 
* >> label.Attributes.Add("style", "top:10; right:10; position:absolute;"); <<
Things that need to be done
** reportId is in session **
- Get position and values of labels, and initialize them into the form.
- Populate the data in the column.
*/
    }
}