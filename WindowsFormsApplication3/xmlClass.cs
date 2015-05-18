using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication3
{
    public class xmlClass
    {
			public string openFile(string path)
			{
				return "Clase xml- metodo openFILE";

			}

			public void readXML(string path)
			{
				List<Factura> Invoice = new List<Factura>();
				List<Articulo> Detail = new List<Articulo>();

				Factura factura = new Factura();
				XmlTextReader reader = new XmlTextReader(path);
				//string serie= "";
				//string folio = "";
				string serie = string.Empty;
				string folio = string.Empty;
				string version = string.Empty;
				string FechaTimbrado = string.Empty;
				string noCertificadoSAT = string.Empty;
				string UUID = string.Empty;
				while (reader.Read())
				{
						switch (reader.NodeType)
						{
							case XmlNodeType.Element: // The node is an element.
								Console.Write("<" + reader.Name);
								Console.WriteLine(">");
								Console.WriteLine(reader.AttributeCount);
								if (reader.Name == "cfdi:Comprobante")
								{
										if (null != reader.GetAttribute("serie"))
										{
											serie = reader.GetAttribute("serie");
											//Console.Write(reader.GetAttribute("serie"));
										}
										if (null != reader.GetAttribute("folio"))
										{
											folio= reader.GetAttribute("folio").ToString();
											Console.Write(reader.GetAttribute("folio").ToString());
										}
										if (null != reader.GetAttribute("fecha"))
										{
											factura.FechaEmision = reader.GetAttribute("fecha");
										}
										if (null != reader.GetAttribute("subTotal"))
										{
											factura.SubTotal = reader.GetAttribute("subTotal");
										}
										if (null != reader.GetAttribute("descuento"))
										{
											factura.Descuento = reader.GetAttribute("descuento");
										}
										if (null != reader.GetAttribute("total"))
										{
											factura.Total = reader.GetAttribute("total");
										}
										if (null != reader.GetAttribute("sello"))
										{
											factura.SelloDigitalEmisor = reader.GetAttribute("sello");
										}
										if (null != reader.GetAttribute("noCertificado"))
										{
											factura.SerieCSD = reader.GetAttribute("noCertificado");
										}
										if (null != reader.GetAttribute("LugarExpedicion"))
										{
											factura.LugarExpedicion = reader.GetAttribute("LugarExpedicion");
										}
								}
								if (reader.Name == "cfdi:Emisor")
								{
										if (null != reader.GetAttribute("rfc"))
										{
											factura.RFCEmisor = reader.GetAttribute("rfc");
										}
										if (null != reader.GetAttribute("nombre"))
										{
											factura.NombreEmisor = reader.GetAttribute("nombre");
										}
								}
								if (reader.Name == "cfdi:DomicilioFiscal")
								{
										if (reader.HasAttributes)
										{
                                
											for (int i = 0; i < reader.AttributeCount; i++)
											{
												//Console.WriteLine("  {0}", reader[i]);
												factura.DireccionEmisor = factura.DireccionEmisor + " " + reader[i];
											}
											// Move the reader back to the element node.
											reader.MoveToElement();
										}
								}
								if (reader.Name == "cfdi:Receptor")
								{
										if (null != reader.GetAttribute("rfc"))
										{
											factura.RFCReceptor = reader.GetAttribute("rfc");
										}
										if (null != reader.GetAttribute("nombre"))
										{
											factura.RFCReceptor = reader.GetAttribute("nombre");
										}     
								}
								if (reader.Name == "cfdi:Domicilio")
								{
										if (reader.HasAttributes)
										{

											for (int i = 0; i < reader.AttributeCount; i++)
											{
												//Console.WriteLine("  {0}", reader[i]);
												factura.DireccionReceptor = factura.DireccionReceptor + " " + reader[i];
											}
											// Move the reader back to the element node.
											reader.MoveToElement();
										}
								}
								if (reader.Name == "cfdi:Concepto")
								{
										Articulo articulo = new Articulo();
										if (null != reader.GetAttribute("cantidad"))
										{
											articulo.Cantidad = reader.GetAttribute("cantidad");
										}
										if (null != reader.GetAttribute("unidad"))
										{
											articulo.Unidad = reader.GetAttribute("unidad");
										}
										if (null != reader.GetAttribute("descripcion"))
										{
											articulo.Concepto = reader.GetAttribute("descripcion");
										}
										if (null != reader.GetAttribute("valorUnitario"))
										{
											articulo.PrecioUnitario = reader.GetAttribute("valorUnitario");
										}
										if (null != reader.GetAttribute("importe"))
										{
											articulo.Importe = reader.GetAttribute("importe");
										}

										factura.Articulos.Add(articulo);
										articulo = null;
								}
								if (reader.Name == "tfd:TimbreFiscalDigital")
								{
										if (null != reader.GetAttribute("selloCFD"))
										{
											factura.SelloDigitalEmisor = reader.GetAttribute("selloCFD");
										}
										if (null != reader.GetAttribute("selloSAT"))
										{
											factura.SelloDigitalSAT = reader.GetAttribute("selloSAT");
										}
										if (null != reader.GetAttribute("version"))
										{
											version = reader.GetAttribute("version");
										}
										if (null != reader.GetAttribute("FechaTimbrado"))
										{
											FechaTimbrado = reader.GetAttribute("FechaTimbrado");
										}
										if (null != reader.GetAttribute("noCertificadoSAT"))
										{
											noCertificadoSAT = reader.GetAttribute("noCertificadoSAT");
										}
										if (null != reader.GetAttribute("UUID"))
										{
											UUID = reader.GetAttribute("UUID");
											Console.WriteLine(reader.GetAttribute("UUID"));
										}
								}
                      
                           
								break;
							/* case XmlNodeType.Text: //Display the text in each element.
								Console.WriteLine(reader.Value);
								break;
							case XmlNodeType.EndElement: //Display the end of the element.
								Console.Write("</" + reader.Name);
								Console.WriteLine(">");
								break;*/
						}
				}
				factura.SerieFolio = serie + folio;
				factura.CadenaOriginalSAT = "||" + version + "|" + UUID +"|" + FechaTimbrado+"|"+factura.SelloDigitalEmisor + "|"+noCertificadoSAT+"||";
            
				// Console.ReadLine();

			}

			public ReporteExel getExcelValues(string path)
			{
				ReporteExel reporte = new ReporteExel();
				XmlTextReader reader = new XmlTextReader(path);
				decimal iva = 0;

				while (reader.Read())
				{
						switch (reader.NodeType)
						{
							case XmlNodeType.Element:
								if (reader.Name == "cfdi:Comprobante")
								{
										if (null != reader.GetAttribute("tipoDeComprobante"))
										{
											reporte.Efecto = reader.GetAttribute("tipoDeComprobante");
										}
										if (null != reader.GetAttribute("subTotal"))
										{
											string valor = reader.GetAttribute("subTotal");
											reporte.SubTotal = Convert.ToDecimal(valor);
										}
										if (null != reader.GetAttribute("fecha"))
										{
											reporte.FechaEmision = reader.GetAttribute("fecha");
										}
										if (null != reader.GetAttribute("total"))
										{
											string valor = reader.GetAttribute("total");
											//Console.WriteLine("Valor----->"+Convert.ToDouble(valor));
                               
												//int total=0;
											//total = Convert.ToInt32(valor);//Int32.Parse(reader.GetAttribute("total").ToString());

											//Int32.TryParse(reader.GetAttribute("total"), out total);
											reporte.Total = Convert.ToDecimal(valor);
										}
 
								}
								if (reader.Name == "cfdi:Emisor")
								{
										if (null != reader.GetAttribute("rfc"))
										{
											reporte.RFCEmisor = reader.GetAttribute("rfc");
										}
										if (null != reader.GetAttribute("nombre"))
										{
											reporte.NombreEmisor = reader.GetAttribute("nombre");
										}
								}
								if (reader.Name == "cfdi:Receptor")
								{
										if (null != reader.GetAttribute("rfc"))
										{
											reporte.RFCReceptor = reader.GetAttribute("rfc");
										}
										if (null != reader.GetAttribute("nombre"))
										{
											reporte.NombreReceptor = reader.GetAttribute("nombre");
										}
								}

								if (reader.Name == "cfdi:Traslado" && null != reader.GetAttribute("impuesto"))
								{
										Traslado traslado = new Traslado();

										if (reader.GetAttribute("impuesto") == "IVA" || reader.GetAttribute("impuesto") == "Iva" || reader.GetAttribute("impuesto") == "iva")
										{
											string valor = reader.GetAttribute("importe");
											iva += Convert.ToDecimal(valor);

										}
                           
								}
								if (reader.Name == "tfd:TimbreFiscalDigital")
								{
										if (null != reader.GetAttribute("UUID"))
										{
                               
											reporte.FolioFiscal = reader.GetAttribute("UUID");
										}
 
								}
								break;
						}
				}
				reporte.IVA = iva;
				return reporte;
			}
			public  Factura getXMLValues(string path){
				List<Factura> Invoice = new List<Factura>();
				List<Articulo> Detail = new List<Articulo>();

				Factura factura = new Factura();
				XmlTextReader reader = new XmlTextReader(path);
				//string serie= "";
				//string folio = "";
				string serie = string.Empty;
				string folio = string.Empty;
				string version = string.Empty;
				string FechaTimbrado = string.Empty;
				string noCertificadoSAT = string.Empty;
				string UUID = string.Empty;
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
							case XmlNodeType.Element: // The node is an element.
								Console.Write("<" + reader.Name);
								Console.WriteLine(">");
								Console.WriteLine(reader.AttributeCount);
								if (reader.Name == "cfdi:Comprobante")
								{
									if (null != reader.GetAttribute("serie"))
									{
											serie = reader.GetAttribute("serie");
											//Console.Write(reader.GetAttribute("serie"));
									}
									if (null != reader.GetAttribute("folio"))
									{
											folio = reader.GetAttribute("folio").ToString();
											Console.Write(reader.GetAttribute("folio").ToString());
									}
									if (null != reader.GetAttribute("fecha"))
									{
											factura.FechaEmision = reader.GetAttribute("fecha");
									}
									if (null != reader.GetAttribute("subTotal"))
									{
											factura.SubTotal = reader.GetAttribute("subTotal");
									}
									if (null != reader.GetAttribute("descuento"))
									{
											factura.Descuento = reader.GetAttribute("descuento");
									}
									if (null != reader.GetAttribute("total"))
									{
											factura.Total = reader.GetAttribute("total");
									}
									if (null != reader.GetAttribute("sello"))
									{
											factura.SelloDigitalEmisor = reader.GetAttribute("sello");
									}
									if (null != reader.GetAttribute("noCertificado"))
									{
											factura.SerieCSD = reader.GetAttribute("noCertificado");
									}
									if (null != reader.GetAttribute("LugarExpedicion"))
									{
											factura.LugarExpedicion = reader.GetAttribute("LugarExpedicion");
									}
								}
								if (reader.Name == "cfdi:Emisor")
								{
									if (null != reader.GetAttribute("rfc"))
									{
											factura.RFCEmisor = reader.GetAttribute("rfc");
									}
									if (null != reader.GetAttribute("nombre"))
									{
											factura.NombreEmisor = reader.GetAttribute("nombre");
									}
								}
								if (reader.Name == "cfdi:DomicilioFiscal")
								{
									if (reader.HasAttributes)
									{

											for (int i = 0; i < reader.AttributeCount; i++)
											{
												//Console.WriteLine("  {0}", reader[i]);
												factura.DireccionEmisor = factura.DireccionEmisor + " " + reader[i];
											}
											// Move the reader back to the element node.
											reader.MoveToElement();
									}
								}
								if (reader.Name == "cfdi:Receptor")
								{
									if (null != reader.GetAttribute("rfc"))
									{
											factura.RFCReceptor = reader.GetAttribute("rfc");
									}
									if (null != reader.GetAttribute("nombre"))
									{
											factura.NombreReceptor = reader.GetAttribute("nombre");
									}
								}
								if (reader.Name == "cfdi:Domicilio")
								{
									if (reader.HasAttributes)
									{

											for (int i = 0; i < reader.AttributeCount; i++)
											{
												//Console.WriteLine("  {0}", reader[i]);
												factura.DireccionReceptor = factura.DireccionReceptor + " " + reader[i];
											}
											// Move the reader back to the element node.
											reader.MoveToElement();
									}
								}
								if (reader.Name == "cfdi:Concepto")
								{
									Articulo articulo = new Articulo();
									if (null != reader.GetAttribute("cantidad"))
									{
											articulo.Cantidad = reader.GetAttribute("cantidad");
									}
									if (null != reader.GetAttribute("unidad"))
									{
											articulo.Unidad = reader.GetAttribute("unidad");
									}
									if (null != reader.GetAttribute("descripcion"))
									{
											articulo.Concepto = reader.GetAttribute("descripcion");
									}
									if (null != reader.GetAttribute("valorUnitario"))
									{
											articulo.PrecioUnitario = reader.GetAttribute("valorUnitario");
									}
									if (null != reader.GetAttribute("importe"))
									{
											articulo.Importe = reader.GetAttribute("importe");
									}

									factura.Articulos.Add(articulo);
									articulo = null;
								}
								if (reader.Name == "cfdi:Traslado") 
								{
									Traslado traslado = new Traslado();
									if (null != reader.GetAttribute("importe"))
									{
											traslado.Importe = reader.GetAttribute("importe");
									}
									if (null != reader.GetAttribute("impuesto"))
									{
											traslado.Impuesto = reader.GetAttribute("impuesto");
									}
									if (null != reader.GetAttribute("tasa"))
									{
											traslado.Tasa = reader.GetAttribute("tasa");
									}
									factura.Traslados.Add(traslado);
									traslado = null;


								}
								if (reader.Name == "cfdi:Retencion") {
									Traslado traslado = new Traslado();
									if (null != reader.GetAttribute("importe"))
									{
											traslado.Importe = reader.GetAttribute("importe");
									}
									if (null != reader.GetAttribute("impuesto"))
									{
											traslado.Impuesto = reader.GetAttribute("impuesto");
									}
									factura.Traslados.Add(traslado);
									traslado = null;

								}
								if (reader.Name == "tfd:TimbreFiscalDigital")
								{
									if (null != reader.GetAttribute("selloCFD"))
									{
											factura.SelloDigitalEmisor = reader.GetAttribute("selloCFD");
									}
									if (null != reader.GetAttribute("selloSAT"))
									{
											factura.SelloDigitalSAT = reader.GetAttribute("selloSAT");
									}
									if (null != reader.GetAttribute("version"))
									{
											version = reader.GetAttribute("version");
									}
									if (null != reader.GetAttribute("FechaTimbrado"))
									{
											FechaTimbrado = reader.GetAttribute("FechaTimbrado");
									}
									if (null != reader.GetAttribute("noCertificadoSAT"))
									{
											noCertificadoSAT = reader.GetAttribute("noCertificadoSAT");
									}
									if (null != reader.GetAttribute("UUID"))
									{
											UUID = reader.GetAttribute("UUID");
											factura.FolioFiscal = reader.GetAttribute("UUID");
									}
								}


								break;
							/* case XmlNodeType.Text: //Display the text in each element.
								Console.WriteLine(reader.Value);
								break;
							case XmlNodeType.EndElement: //Display the end of the element.
								Console.Write("</" + reader.Name);
								Console.WriteLine(">");
								break;*/
					}
				}
				factura.SerieFolio = serie + folio;
				factura.CadenaOriginalSAT = "||" + version + "|" + UUID + "|" + FechaTimbrado + "|" + factura.SelloDigitalEmisor + "|" + noCertificadoSAT + "||";

				// Console.ReadLine();

				return factura;  
          
       
			}
			
			public EgresoItem Factura2Egreso(Factura factura){
				EgresoItem egreso =  new EgresoItem();

				DateTime Date = DateTime.Now;
				string sqlDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

				egreso.Fecha = sqlDate;
				egreso.FechaDePago = sqlDate;
				egreso.FechaReg = sqlDate;
				string descripcion = "";
				foreach (Articulo art in factura.Articulos)
				{
					if (descripcion.Length < 100)
					{
						descripcion +=  art.Concepto + "/";
					}
				}
				egreso.Descripcion = descripcion;
				egreso.Clasificacion = "BIENES";
            egreso.Receptor = factura.RFCReceptor;
            egreso.Emisor = factura.RFCEmisor;
				try
				{
					egreso.Descuento = decimal.Parse(factura.Descuento);
				}
				catch
				{

					egreso.Descuento = 0.0M;
				}
				try
				{
					egreso.IVA = decimal.Parse(factura.IVA);
				}
				catch 
				{

					egreso.IVA = 0.0M;//decimal.Parse(factura.IVA);
				}
				try
				{
					egreso.Total = decimal.Parse(factura.Total);
				}
				catch 
				{
					egreso.Total = 0.0M;
					
				}
				//egreso.Descuento = decimal.Parse(factura.Descuento);
				//egreso.IVA = decimal.Parse(factura.IVA);
				//egreso.Total = decimal.Parse(factura.IVA);
				egreso.Factura = factura.SerieFolio;
				//egreso.ImporteSinDescuento = 

				return egreso;
				

			}

			public IngresoItem Factura2Ingreso(Factura factura)
			{
				IngresoItem ingreso = new IngresoItem();
				DateTime Date = DateTime.Now;
				string sqlDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

				ingreso.Fecha = sqlDate;
				ingreso.FechaDeCobro = sqlDate;
				ingreso.FechaReg = sqlDate;
				ingreso.Factura = factura.SerieFolio;
				ingreso.Importe = decimal.Parse(factura.SubTotal);
				ingreso.IVA = decimal.Parse(factura.IVA);
				ingreso.Total = decimal.Parse(factura.Total);

				return ingreso;


			}

		 
    }
}
