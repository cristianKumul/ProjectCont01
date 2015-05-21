using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace WindowsFormsApplication3
{
   public partial class CapturaSISPrevia : Form
   {
		
		public string UserID {get;set;}
		public dynamic datosCaptura { get; set; }
		public string tipoCaptura { get; set; }
		public List<bool> statusFacturas = new List<bool>();

      static string connStr = ConfigurationManager.ConnectionStrings["MyConn1"].ToString();
      static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn1"].ConnectionString);
      public CapturaSISPrevia()
      {
         InitializeComponent();
			
      }

      private void CapturaSISPrevia_Load(object sender, EventArgs e)
      {

         dataGridView1.AutoGenerateColumns = true;

         DataSet ds = new DataSet();

         DataTable dt = new DataTable("Captura");
         dt.Columns.Add(new DataColumn("Select", typeof(System.Boolean)));
         dt.Columns.Add(new DataColumn("Emisor", typeof(string)));
         dt.Columns.Add(new DataColumn("Receptor", typeof(string)));
         dt.Columns.Add(new DataColumn("Factura", typeof(string)));
         dt.Columns.Add(new DataColumn("Descripcion", typeof(string)));
         dt.Columns.Add(new DataColumn("Importe", typeof(decimal)));
         dt.Columns.Add(new DataColumn("Descuento", typeof(decimal)));
         dt.Columns.Add(new DataColumn("IVA", typeof(decimal)));
         dt.Columns.Add(new DataColumn("Total", typeof(decimal)));
			dt.Columns.Add(new DataColumn("File", typeof(string)));
         dt.Columns.Add(new DataColumn("BaseDatos", typeof(string)));
         dt.Columns.Add(new DataColumn("ID", typeof(int)));


			

         /*
			DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
			chk.HeaderText = "Aprobación";
			chk.Name = "chk";
         chk.SortMode = DataGridViewColumnSortMode.Automatic;
			dataGridView1.Columns.Add(chk);
         */
			DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
			btn.HeaderText = "Abrir PDF";
			btn.Name = "VerPDF";
			btn.Text = "Abrir PDF";
			
			btn.UseColumnTextForButtonValue = true;
			dataGridView1.Columns.Add(btn);


         DataRow dr = dt.NewRow();


			for (int i = 0; i < datosCaptura.Count; i++)
			{
            try
            {  string connStr = ConfigurationManager.ConnectionStrings["MyConn1"].ToString();
               SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn1"].ConnectionString);
               conn.Open();
               using (SqlConnection con = conn)
               {

                  string ingreso ="SELECT cl.Clave,cld.Clave FROM [" + conn.Database + "].[dbo].[Cat_Clientes] AS cl JOIN [" + con.Database + "].[dbo].[Cat_ClientesDe] as cld on cld.[RFC] = '" + datosCaptura[i].Receptor + "' WHERE cl.RFC = '" + datosCaptura[i].Emisor + "';";
                  string egreso = "SELECT cl.Clave,prov.Clave FROM [" + conn.Database + "].[dbo].[Cat_Clientes] AS cl JOIN [" + conn.Database + "].[dbo].[Cat_Proveedores] as prov on prov.[RFC] = '" + datosCaptura[i].Emisor + "' WHERE cl.RFC = '" + datosCaptura[i].Receptor + "';";
                  string query ="";

                  if (tipoCaptura == "Egresos")
                  {
                     query = egreso;
                  }
                  if (tipoCaptura == "Ingresos")
                  {
                    query = ingreso;
                  }
                  using (SqlCommand command = new SqlCommand(query, con))
                  {
                     using (SqlDataReader reader = command.ExecuteReader())
                     {

                        if (reader.Read())
                        {
                           datosCaptura[i].EnBase = true;

                           //MessageBox.Show(reader[0].ToString() + " , " + reader[1].ToString());
                           if (tipoCaptura == "Egresos")
                           {
                              datosCaptura[i].IdEmpresa = Convert.ToInt32(reader[0].ToString());//reader.GetInt32(0);//(int)reader[0];
                              datosCaptura[i].IdProveedor =Convert.ToInt32( reader[1].ToString());//reader.GetInt32(1); //(int)reader[1];
                              int foo1 = datosCaptura[i].IdEmpresa;
                              int foo2 = datosCaptura[i].IdProveedor;
                           }
                           if (tipoCaptura == "Ingresos")
                           {

                              datosCaptura[i].IdEmpresa = Convert.ToInt32(reader[0].ToString());
                              datosCaptura[i].IdCliente = Convert.ToInt32(reader[1].ToString());
                              int foo1 = datosCaptura[i].IdEmpresa;
                              int foo2 = datosCaptura[i].IdCliente;

                           }

                        }
                        else
                        {
                           datosCaptura[i].EnBase = false;
                        }

                     }

                  }

               }
               conn.Close();
            }
            catch(Exception error )
            {

               var st = new StackTrace(error,true);
               var frame = st.GetFrame(0);
               var line = frame.GetFileLineNumber();



               MessageBox.Show( error.Message.ToString());
            }
				dr = dt.NewRow();
				dr["Factura"] = datosCaptura[i].Factura;
            
				dr["Descripcion"] = datosCaptura[i].Descripcion;
            try
            {
               dr["Descuento"] = datosCaptura[i].Descuento;
               
            }
            catch
            {
               dr["Descuento"] = 0.0M;
              
            }
            dr["IVA"] = datosCaptura[i].IVA;
				dr["Total"] = datosCaptura[i].Total;
				dr["File"] = datosCaptura[i].Archivo;
            dr["ID"] = i;
            dr["Emisor"] = datosCaptura[i].Emisor;
            dr["Receptor"] = datosCaptura[i].Receptor;
            dr["Select"] = datosCaptura[i].EnBase ? true : false;
            dr["BaseDatos"] = datosCaptura[i].EnBase ? "OK" : "No encontrado";
				dt.Rows.Add(dr);

			}

		
			ds.Tables.Add(dt);
			
			
         dataGridView1.DataSource = ds.Tables[0];

			this.dataGridView1.Columns[10].Visible = false;
         this.dataGridView1.Columns[12].Visible = false;
         //dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
         for (int i = 1; i < 8 ; i++)
         {
           
            dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
         }
        // dataGridView1.DataMember = "Prueba de tabla";
         dataGridView1.AllowUserToAddRows = false;
         dataGridView1.AllowUserToDeleteRows = false;
         dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

         
       
      }

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == dataGridView1.Columns["verPDF"].Index && e.RowIndex >= 0)
			{
				

				MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());
            dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
				MessageBox.Show(e.RowIndex.ToString());
				string path = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
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

      private void cancelarbtn_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      private void button1_Click(object sender, EventArgs e)
      {
         
         

         for (int i = 0; i < this.dataGridView1.RowCount; i++ )
         {

            int id = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[12].Value.ToString());
            //MessageBox.Show(this.dataGridView1.Rows[i].Cells[1].Value.ToString());
            if (this.dataGridView1.Rows[i].Cells[1].Value.ToString() == "True")
            {
               //int id = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[12].Value.ToString());
               //MessageBox.Show(id.ToString());

               datosCaptura[id].SeleccionadoPorUsuario = true;
            }
            else
            {
               //int id = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[12].Value.ToString());
               datosCaptura[Convert.ToInt32(this.dataGridView1.Rows[i].Cells[12].Value.ToString())].SeleccionadoPorUsuario = false;
            }
             
         }

         IEnumerable<dynamic> datos = datosCaptura;

         IEnumerable<dynamic> consulta = datos.Where(s => s.EnBase == true && s.SeleccionadoPorUsuario == true);
         //IEnumerable<dynamic> result = consulta.Where(s => s.SeleccionadoPorUsuario == true);
         
         MessageBox.Show(consulta.Count().ToString());


         string queryInsert = "";
         string commandEgresos = "INSERT INTO [@database].[dbo].[tblEgresos] ([IdEmpresa],[IdSucursal] ,[Fecha] ,[Factura],[IdProveedor]"+
           ",[Descripcion] ,[ImporteSinDescuento],[Descuento],[Importe],[PorIVA],[IVA],[Total],[FechaDePago],[Estatus],[IdUsuarioReg]"+
           ",[FechaReg],[DeducibleISR],[DeducibleIETU],[EsInversion],[AplicaParaDIOT]) VALUES (@Empresa,0,@Fecha,@Factura,@ClientProveedor,@Descripcion,@ImporteSnDesc,@Descuento,@Importe,16.0,@IVA,@FechaPago,'PAGADA',@IdUsuario,@FechaReg,1,1,0,1);";

         string commandIngresos = "INSERT INTO [@database].[dbo].[tblIngresos] ([IdEmpresa],[IdCliente],[IdSucursal] ,[Fecha],[Factura] ,[Importe],"+
           "[PorIVA],[IVA],[Total],[FechaDeCobro] ,[Estatus],[IdUsuarioReg],[FechaReg],[AcumulableISR]"+
           ",[AcumulableIETU],[PorRetISR],[RetISR],[PorRetIVA],[RetIVA]) VALUES (@Empresa,@ClientProveedor,0,@Fecha,@Factura,@Importe,16.0,@IVA,@Total,@FechaCobro,'COBRADA',@IdUsuario,@FechaReg,1,1,10.0,0.00,66.67,0.00);";

         


         string connStr = ConfigurationManager.ConnectionStrings["MyConn1"].ToString();
         SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn1"].ConnectionString);
         conn.Open();
         using (SqlConnection con = conn)
         {
            queryInsert = tipoCaptura == "Egresos" ? commandEgresos : commandIngresos;
            using(SqlCommand query = new SqlCommand(queryInsert))
            {
               query.Connection = con;
               bool bit = tipoCaptura == "Egresos" ? true : false;
               if (bit)
               {
                  query.Parameters.Add("@Empresa", SqlDbType.Int, 4).Value = consulta.ElementAt(0).IdEmpresa;
                  query.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = consulta.ElementAt(0).Fecha;
                  query.Parameters.Add("@Factura", SqlDbType.VarChar,80).Value = consulta.ElementAt(0).Factura;


               }
            }
         }
      
         

         
         
      }
   }
}
