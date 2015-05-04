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
     partial class FacturaRpt : Form
    {
        //
        //Cree dos listas una para el Encabezado y otra para el detalle
        //
         public List<Factura> Invoice = new List<Factura>();
         public List<Articulo> Detail = new List<Articulo>();
         public List<Traslado> Traslate = new List<Traslado>();
        //
        //Cree las propiedades publicas Titulo y Empresa
        //
        public string Titulo { get; set; }
        public string Empresa { get; set; }

        public FacturaRpt()
        {
            InitializeComponent();
        }

        public void FacturaRpt_Load(object sender, EventArgs e)
        {
            //Limpiemos el DataSource del informe
            reportViewer1.LocalReport.DataSources.Clear();
            //
       
            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Encabezado", Invoice));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DetalleArticulo", Detail));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Traslados",Traslate));
            //
            //Enviemos la lista de parametros
            //
            
            //Hagamos un refresh al reportViewer
            //
            reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

    

       
    }
}
