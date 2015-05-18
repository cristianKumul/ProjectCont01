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
		
		public string UserID {get;set;}
		public dynamic datosCaptura { get; set; }
		public string tipoCaptura { get; set; }
		public List<bool> statusFacturas = new List<bool>();

		
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
         dt.Columns.Add(new DataColumn("Importe", typeof(decimal)));
         dt.Columns.Add(new DataColumn("Descuento", typeof(decimal)));
         dt.Columns.Add(new DataColumn("IVA", typeof(decimal)));
         dt.Columns.Add(new DataColumn("Total", typeof(decimal)));
			dt.Columns.Add(new DataColumn("File", typeof(string)));


			


			DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
			chk.HeaderText = "Aprobación";
			chk.Name = "chk";
			dataGridView1.Columns.Add(chk);

			DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
			btn.HeaderText = "Abrir PDF";
			btn.Name = "VerPDF";
			btn.Text = "Abrir PDF";
			
			btn.UseColumnTextForButtonValue = true;
			dataGridView1.Columns.Add(btn);


         DataRow dr = dt.NewRow();


			for (int i = 0; i < datosCaptura.Count; i++)
			{
				dr = dt.NewRow();
				dr["Factura"] = datosCaptura[i].Factura;
				dr["Descripción"] = datosCaptura[i].Descripcion;
				dr["Descuento"] = datosCaptura[i].Descuento;
				dr["IVA"] = datosCaptura[i].IVA;
				dr["Total"] = datosCaptura[i].Total;
				dr["File"] = datosCaptura[i].Archivo;
				dt.Rows.Add(dr);

			}

			/*
			dr["Descripción"] = "CARPETA ASFALTICA";
         dr["Importe"] = "13800.00";
         dr["IVA"] = "16";
         dr["Total"] = "15318.00";
         dt.Rows.Add(dr);

			

			dr = dt.NewRow();
			dr["Factura"] = "1174";

			dr["Descripción"] = "CARPETA ASFALTICA";
			dr["Importe"] = "13800.00";
			dr["IVA"] = "16";
			dr["Total"] = "15318.00";
			dt.Rows.Add(dr);
			  */
			ds.Tables.Add(dt);
			
			
         dataGridView1.DataSource = ds.Tables[0];

			this.dataGridView1.Columns[8].Visible = false;
        // dataGridView1.DataMember = "Prueba de tabla";


         
       
      }

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == dataGridView1.Columns["verPDF"].Index && e.RowIndex >= 0)
			{
				
				MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
				MessageBox.Show(e.RowIndex.ToString());
				string path = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
				Factura invoice = new Factura();
			
				FacturaRpt frm = new FacturaRpt();
				xmlClass xmlTool = new xmlClass();
				Factura factura = xmlTool.getXMLValues(path);


				frm.Invoice.Add(factura);
				frm.Detail = factura.Articulos;
				frm.Traslate = factura.Traslados;
				//
				//Enviamos el detalle de la Factura, como Detail es una lista e invoide.Details tambien
				//es un lista del tipo EArticulo bastara con igualarla
				//

				frm.Show();
			}
		}
   }
}
