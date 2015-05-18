using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication3
{
    public class Articulo
    {
        public string Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Concepto { get; set; }
        public string PrecioUnitario { get; set; }
        public string Importe { get; set; }

       
    }
    public class Traslado
    {
        public string Impuesto { get; set; }
        public string Tasa { get; set; }
        public string Importe { get; set; }
    }
    public class Factura
    {
        public string FolioFiscal { get; set; }
        public string SerieCSD { get; set; }
        public string FechaEmision { get; set; }
        public string SerieFolio { get; set; }
        public string RFCEmisor { get; set; }
        public string NombreEmisor { get; set; }
        public string DireccionEmisor { get; set; }
        public string RFCReceptor { get; set; }
        public string NombreReceptor { get; set; }
        public string DireccionReceptor { get; set; }
        public string SubTotal { get; set; }
        public string IVA { get; set; }
        public string Total { get; set; }
        public string Descuento { get; set; }
        public string SelloDigitalEmisor { get; set; }
        public string SelloDigitalSAT { get; set; }
        public string SelloCertificadoSAT { get; set; }
        public string LugarExpedicion { get; set; }
        public string CadenaOriginalSAT { get; set; }
        public List<Articulo> Articulos = new List<Articulo>();
        public List<Traslado> Traslados = new List<Traslado>();

    }
    public class ReporteExel
    {
        public string FolioFiscal { get; set; }
        public string RFCEmisor { get; set; }
        public string NombreEmisor { get; set; }
        public string RFCReceptor { get; set; }
        public string NombreReceptor { get; set; }
        public string Efecto { get; set; }
        public string FechaEmision { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public decimal IVA { get; set; }

    }
	 public class EgresoItem
	 {
		 public int IdEmpresa { get; set; }
		 public int IdSucursal { get; set; }
		 public string Poliza { get; set; }
		 public string Fecha { get; set; }
		 public string Factura { get; set; }
		 public int IdProveedor { get; set; }
		 public string Descripcion { get; set; }
		 public string Clasificacion { get; set; }
		 public decimal ImporteSinDescuento { get; set; }
		 public decimal IEPS { get; set; }
		 public decimal Descuento { get; set; }
		 public decimal Bienes { get; set; }
		 public float PorIVA { get; set; }
		 public decimal IVA { get; set; }
		 public decimal Total { get; set; }
		 public string FechaDePago { get; set; }
		 public string IdUsuarioReg { get; set; }
		 public string FechaReg { get; set; }
		 public string Archivo { get; set; }

	 }
		public class IngresoItem
	 {
		 public int IdEmpresa { get; set; }
		 public int IdCliente { get; set; }
		 public int IdSucursal { get; set; }
		 public string Fecha { get; set; }
		 public string Factura { get; set; }
		 public decimal Importe { get; set; }
		 public float PorIVA { get; set; }
		 public decimal IVA { get; set; }
		 public decimal Total { get; set; }
		 public string FechaDeCobro { get; set; }
		 public string IdUsuarioReg { get; set; }
		 public string FechaReg { get; set; }
		 public float PorRetIVA = 66.7F;
		 public string Archivo { get; set; }

	 }
    public class ListExcel
    {
        List<ReporteExel> Data = new List<ReporteExel>();
    }
    
}
