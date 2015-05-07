using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net; //webclient
//using HtmlAgilityPack;
using System.IO;
using Microsoft.Reporting.WinForms;








namespace WindowsFormsApplication3
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private SHDocVw.WebBrowser_V1 Web_V1;
        public void InlinePopups(WebBrowser browser)
        {
            
            // hooks to force new windows to open in the current instance
            Web_V1 = (SHDocVw.WebBrowser_V1)browser.ActiveXInstance;
            Web_V1.NewWindow += new SHDocVw.DWebBrowserEvents_NewWindowEventHandler((string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed) =>
            {
                Processed = true; // stop event from being processed

                // open in the existing window

                browser.Navigate(URL);
            });
        }

       
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://portalcfdi.facturaelectronica.sat.gob.mx/");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Refresh();

        }
       // public SHDocVw.WebBrowser_V1 Web_V1; 
        private void Main_Load(object sender, EventArgs e)
        {
           // Web_V1 = (SHDocVw.WebBrowser_V1)this.webBrowser1.ActiveXInstance;
           // Web_V1.NewWindow += new SHDocVw.DWebBrowserEvents_NewWindowEventHandler(Web_V1_NewWindow);

            webBrowser1.Navigate("https://portalcfdi.facturaelectronica.sat.gob.mx/");
           // InlinePopups(webBrowser1);
        }

        
       
        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            String[] splitCFDI;
            int numGuion;
            String rfc,ConsultaActual;
            String ruta = "";

            List<String> lista = new List<String>();

            HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("img");
            WebClient foo = new WebClient();
            string url = "https://portalcfdi.facturaelectronica.sat.gob.mx/";

            WebRequest request = WebRequest.Create(url);
            
            CookieContainer cookies = new CookieContainer();
            var cookie = FullWebBrowserCookie.GetCookieInternal(webBrowser1.Url, false);

            foo.Headers.Add("Cookie:" + cookie);
            foo.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            
            foreach (HtmlElement div in webBrowser1.Document.GetElementsByTagName("div"))
            {
                if (div.GetAttribute("id") == "encabezadoPortal")
                {
                    foreach (HtmlElement span in div.GetElementsByTagName("span"))
                    {
                        if (span.GetAttribute("id") == "ctl00_LblRfcAutenticado")
                        {
                            sb.Append(span.InnerText);
                        }
                    }
                }

                if (div.GetAttribute("id") == "DivPaginas")
                {
                    
                    foreach (HtmlElement span in div.GetElementsByTagName("span"))
                    {

                        String textSpan = span.InnerText;
                        if (textSpan != null)
                        {
                            splitCFDI = textSpan.Split(new Char[] { '-' });
                            numGuion = splitCFDI.Length;
                        }
                        else
                        {
                            numGuion = 0;
                        }
                        if (numGuion == 5)
                        {   

                            lista.Add(span.InnerText);
                            
                        }
                    }
                }
            }
            ///

            String[] cfdi = lista.ToArray();

            int tamlist = cfdi.Length;
            toolStripProgressBar1.Maximum = tamlist;

            rfc = sb.ToString();

           

           String[] NomFactura = cfdi.ToArray();

           ConsultaActual = webBrowser1.Url.ToString();
           String[] splitConsulta = ConsultaActual.Split(new Char[] { '/' });

           int consultaNum = splitConsulta.Length;

         
            
            if (rfc != "")
            {
                String[] split = rfc.Split(new Char[] { ' ' });

                int p = split.Length;
                
                //MessageBox.Show(p.ToString());
                //MessageBox.Show(split[2]);
                if (splitConsulta[3] == "ConsultaEmisor.aspx")
                {
                    ruta = "C://SATMasivo/" + split[2] + "/FacturasEmitidas/";
                }
                else if (splitConsulta[3] == "ConsultaReceptor.aspx")
                {
                    ruta = "C://SATMasivo/" + split[2] + "/FacturasRecibidas/";
                }
                else
                {
                    ruta = "C://SATMasivo/"+ split[2];
                }

                
                if (!Directory.Exists(ruta))
                {
                   
                    Directory.CreateDirectory(ruta);
                }
                

            }

            String rutaDestino = ruta;

            int n = 0;
           
            foreach (HtmlElement link in links)
            {
                //link.InvokeMember("Click");
                //MessageBox.Show("a");

                String nameStr = link.GetAttribute("name");

               

                if(nameStr == "BtnDescarga"){


                    //int iStartPos = link.OuterHtml.IndexOf("onclick=\"") + ("onclick=\"").Length;
                    //int iEndPos = link.OuterHtml.IndexOf("\">", iStartPos);
                    int iStartPos = link.OuterHtml.IndexOf("return AccionCfdi('") + ("return AccionCfdi('").Length;

                    int iEndPos = link.OuterHtml.IndexOf("\'", iStartPos);
                                        
                    //MessageBox.Show("XML " + n);
                    
                    //link.Focus();
                    //SendKeys.Send("{ENTER}");
                   // link.InvokeMember("Click", null);
                    //string foo;
                    //foo = link.ToString();
                    
                   // MessageBox.Show(link.InnerText);
                   
                    Application.DoEvents();
                    String folioName = link.OuterHtml.Substring(iStartPos,iEndPos-iStartPos);
                    //Uri path = new Uri(url + folioName);
                    //foo.DownloadFileAsync(path, @ruta);
                    String filepath = @"" + ruta +"/"+ NomFactura[n] + ".xml";
                   foo.DownloadFile(url + folioName, filepath);

                   n += 1;
                   toolStripProgressBar1.Value += 1;

                  
                }

               
          


            }
            MessageBox.Show("Se descargaron " + n + " Facturas en "+ruta);
            toolStripProgressBar1.Value = 0;

           
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML Files|*.xml";
            openFileDialog1.Title = "Seleccionar archivo XML";
            string file = "";
            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                file = openFileDialog1.FileName;
                textBox1.Text = file;
                //MessageBox.Show(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
           // openFileDialog1.Filter = "XML Files|*.xml";
           // openFileDialog1.Title = "Seleccionar archivo XML";
            string path = "";
            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.SelectedPath;
                textBox2.Text = path;
                //MessageBox.Show(openFileDialog1.SelectedPath);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private static List<Articulo> FillDgv()
        {
            //
            //Cree una lista generica de la entidad EArticulo
            //
            List<Articulo> listaArticulos = new List<Articulo>();

            //
            //Instancie la clase EArticulo para agregar datos a la lista
            //
         

            Articulo item1 = new Articulo
            {
                //Establezca valores a cada una de las propiedades
                Cantidad = "1",
                Unidad = "7",
                Concepto = "Descripción del artículo 1",
                PrecioUnitario = "12.0",
                Importe = "88.1"
            };
            listaArticulos.Add(item1);

            Articulo item2 = new Articulo
            {
                //Establezca valores a cada una de las propiedades
                Cantidad = "1",
                Unidad = "7",
                Concepto = "Descripción del artículo 1",
                PrecioUnitario = "12.0",
                Importe = "88.1"
            };
            listaArticulos.Add(item2);

            return listaArticulos;
        }

        public void button3_Click(object sender, EventArgs e)
        {
            //
            //Hacemos una instancia de la clase EFactura para
            //llenarla con los valores contenidos en los controles del Formulario
            Factura invoice = new Factura();
            invoice.FolioFiscal = "23123123123123123";
            invoice.NombreEmisor = "nombre emisor";
            invoice.RFCEmisor = "12123lh123";
            invoice.DireccionEmisor = "Resaskjdhaksdh  aksdh akjshd kajsd akjshd ";
            invoice.FechaEmision = "1201-12-11";
            invoice.SubTotal = "10.0";
            invoice.IVA = "10.0";
            invoice.Total = "10.0";

            //Recorremos los Rows existentes actualmente en el control DataGridView
            //para asignar los datos a las propiedades
         
              Articulo article = new Articulo();
                //
                //Vamos tomando los valores de las celdas del row que estamos 
                //recorriendo actualmente y asignamos su valor a la propiedad de la clase intanciada
                //
              article.Cantidad = "1";
            article.Unidad = "pza";
            article.Concepto = "asassadsd";
            article.PrecioUnitario = "1.00";
            article.Importe = "100.00";
           

                //
                //Vamos agregando el Item a la lista del detalle
                //
            invoice.Articulos.Add(article);
            
                //
                //Vamos agregando el Item a la lista del detalle
                //
                //invoice.Detail.Add(article);
            

            //
            //Creamos una instancia del Formulario que contiene nuestro
            //ReportViewer
            //
            FacturaRpt frm = new FacturaRpt();
            //
            //Usamos las propiedades publicas del formulario, aqui es donde enviamos el valor
            //que se mostrara en los parametros creados en el LocalReport, para este ejemplo
            //estamos Seteando los valores directamente pero usted puede usar algun control
            //
           
            //
            //Recuerde que invoice es una Lista Generica declarada en el FacturaRtp, es una lista
            //porque el origen de datos del LocalReport unicamente permite ser enlazado a objetos que 
            //implementen IEnumerable.
            //
            //Usamos el metod Add porque Invoice es una lista e invoice es una entidad simple
            xmlClass xmlTool = new xmlClass();
            Factura factura = xmlTool.getXMLValues(textBox1.Text);
 
            
            frm.Invoice.Add(factura);
            frm.Detail = factura.Articulos;
            frm.Traslate = factura.Traslados;
            //
            //Enviamos el detalle de la Factura, como Detail es una lista e invoide.Details tambien
            //es un lista del tipo EArticulo bastara con igualarla
            //
           
            frm.Show();
            
            //List<Factura> Invoice = new List<Factura>();
            //List<Articulo> Detail = new List<Articulo>();
            //ReportDataSource encabezado = new ReportDataSource("Encabezado", Invoice);
            //ReportDataSource detalle = new ReportDataSource("DetalleArticulo", Invoice);
            /*
            // Variables
            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Report1.rdlc"; //"YourReportHere.rdlc";
            viewer.LocalReport.DataSources.Add(encabezado); // Add datasource here
            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            using (FileStream fs = new FileStream("output.pdf", FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
              */
            /*
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Report1.rdlc"; //"YourReportHere.rdlc";
            viewer.LocalReport.DataSources.Add(encabezado); // Add datasource here
            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;
            // just gets the Report title... make your own method
            //ReportViewer needs a specific type (not always the same as the extension)
            string exportType = "PDF";
            //string reportsTitle = "reports/Report1.rdlc";
            filetype = exportType == "PDF" ? "PDF" : exportType;
            byte[] bytes = viewer.ServerReport.Render("PDF", null,out mimeType, out encoding, out extension, out streamIds, out warnings);
          
            FileStream fs = new FileStream("output.pdf", FileMode.OpenOrCreate);

            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            xmlClass foo = new xmlClass();
            foo.readXML(textBox1.Text);*/


        }

        private void button4_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(textBox2.Text);
            FileInfo[] files = di.GetFiles("*.xml");
            Console.WriteLine(files.Count());
            progressBar1.Maximum = files.Count();

            foreach (FileInfo filetmp in files)
            {
                string file = filetmp.ToString();
                xmlClass xmlTool = new xmlClass();
                Factura factura = xmlTool.getXMLValues(filetmp.FullName);

                List<Factura> datoFactura = new List<Factura>();

                datoFactura.Add(factura);
                Warning[] warnings;
                string[] streams;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = "Report1.rdlc";



                viewer.LocalReport.DataSources.Add(new ReportDataSource("Encabezado", datoFactura));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DetalleArticulo", datoFactura[0].Articulos));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("Traslados",datoFactura[0].Traslados));
                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streams, out warnings);

               // string fileoutput = textBox2.Text + @"\" ;

                string ruta = textBox2.Text + "/PDF/";
                if (!Directory.Exists(ruta))
                {

                    Directory.CreateDirectory(ruta);
                }

                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                FileStream fs = new FileStream(ruta+Path.GetFileNameWithoutExtension(filetmp.Name)+".pdf", FileMode.OpenOrCreate);

                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                progressBar1.Value += 1;

            
            }
            string ruta2 = textBox2.Text + "/PDF/";
            MessageBox.Show("Se crearon " + progressBar1.Maximum + " PDFs en " + ruta2);
            progressBar1.Value = 0;
            
            //Recorremos los Rows existentes actualmente en el control DataGridView
            //para asignar los datos a las propiedades






            
           /*
            //-----------------
            // Variables
            Warning[] warnings;
            string[] streams;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Report1.rdlc";
            


            viewer.LocalReport.DataSources.Add(new ReportDataSource("Encabezado", Invoice));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DetalleArticulo", Invoice[0].Articulos));

            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streams, out warnings);


            
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            FileStream fs = new FileStream("C:\\output.pdf", FileMode.OpenOrCreate);

            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
         
            //xmlClass foo = new xmlClass();
            //foo.readXML(textBox1.Text);
           
         
*/            

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            // openFileDialog1.Filter = "XML Files|*.xml";
            // openFileDialog1.Title = "Seleccionar archivo XML";
            string path = "";
            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.SelectedPath;
                textBox3.Text = path;
                //MessageBox.Show(openFileDialog1.SelectedPath);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(textBox3.Text);
            FileInfo[] files = di.GetFiles("*.xml");
            Excelrpt datos = new Excelrpt();
            progressBar1.Maximum = files.Count();


            foreach (FileInfo filetmp in files)
            {
                xmlClass xmlTool = new xmlClass();
                ReporteExel factura = xmlTool.getExcelValues(filetmp.FullName);
                datos.dataExcel.Add(factura);
                progressBar1.Value += 1;
                
            }
            progressBar1.Value = 0;
            datos.Show();
        }

        private void toolStripProgressBar1_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
           ConfDataBase form = new ConfDataBase();

           form.Show();

        }

        private void button9_Click(object sender, EventArgs e)
        {
           FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
           // openFileDialog1.Filter = "XML Files|*.xml";
           // openFileDialog1.Title = "Seleccionar archivo XML";
           string path = "";
           // Show the Dialog.
           // If the user clicked OK in the dialog and
           // a .CUR file was selected, open it.
           if (openFileDialog1.ShowDialog() == DialogResult.OK)
           {
              path = openFileDialog1.SelectedPath;
              pathIngresostxt.Text = path;
              //textBox2.Text = path;
              //MessageBox.Show(openFileDialog1.SelectedPath);
           }
        }

        private void button11_Click(object sender, EventArgs e)
        {
           FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
           // openFileDialog1.Filter = "XML Files|*.xml";
           // openFileDialog1.Title = "Seleccionar archivo XML";
           string path = "";
           // Show the Dialog.
           // If the user clicked OK in the dialog and
           // a .CUR file was selected, open it.
           if (openFileDialog1.ShowDialog() == DialogResult.OK)
           {
              
              path = openFileDialog1.SelectedPath;
              pathEgresostxt.Text = path;
              
              //textBox2.Text = path;
              //MessageBox.Show(openFileDialog1.SelectedPath);
           }
        }

       

      


       

       
    }
}
