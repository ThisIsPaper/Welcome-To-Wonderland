
namespace Wonderland.Web.Dashboards
{
    using System;

    public partial class ExportFinancials : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.exportFinancialsButton.Click += this.ExportFinancialsButton_Click;
        }

        private void ExportFinancialsButton_Click(object sender, EventArgs e)
        {
            this.Response.Clear();
            this.Response.AddHeader("content-disposition", "attachment; filename=financials.csv");
            this.Response.ContentType = "text/csv";
            this.Response.Write("a, b, c");
            this.Response.End();
        }
    }
}