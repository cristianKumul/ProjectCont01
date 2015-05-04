using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication3
{
    public partial class Excelrpt : Form
    {
       public List<ReporteExel> dataExcel = new List<ReporteExel>();

        public Excelrpt()
        {
            InitializeComponent();
        }

        private void Excelrpt_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ExcelData", dataExcel));
            this.reportViewer1.RefreshReport();
        }
    }
}
