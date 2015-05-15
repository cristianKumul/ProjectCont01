using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
   public partial class CapturaSISPrevia : Form
   {
      public CapturaSISPrevia()
      {
         InitializeComponent();
      }

      private void CapturaSISPrevia_Load(object sender, EventArgs e)
      {

         dataGridView1.AutoGenerateColumns = true;

         DataSet ds = new DataSet();

         DataTable dt = new DataTable("Captura");
         dt.Columns.Add(new DataColumn("Factura", typeof(string)));
         dt.Columns.Add(new DataColumn("Descripción", typeof(string)));
         dt.Columns.Add(new DataColumn("Importe", typeof(string)));
         dt.Columns.Add(new DataColumn("Descuento", typeof(string)));
         dt.Columns.Add(new DataColumn("PorIVA", typeof(string)));
         dt.Columns.Add(new DataColumn("Total", typeof(string)));



         DataRow dr = dt.NewRow();
         dr["Factura"] = "1174";
        
         dr["Descripción"] = "CARPETA ASFALTICA";
         dr["Importe"] = "13800.00";
         dr["PorIVA"] = "16";
         dr["Total"] = "15318.00";
         dt.Rows.Add(dr);
         ds.Tables.Add(dt);


         dataGridView1.DataSource = ds.Tables[0];
         dataGridView1.DataMember = "Prueba de tabla";

         dataGridView1.Rows[0].ReadOnly = false;
         dataGridView1.Rows[1].ReadOnly = true;
         dataGridView1.Rows[2].ReadOnly = true;

         
       
      }
   }
}
