using ABI.Windows.AI.MachineLearning;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.CajaViewModels;
using MultiMarcasAPP.ViewModels.CotizacionViewModels;
using MultiMarcasAPP.ViewModels.GastosViewModels;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using Spire.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GastoViewModel = MultiMarcasAPP.ViewModels.GastosViewModels.GastoViewModel;

namespace MultiMarcasAPP.Services.PDF
{
    public class PDFMaker
    {
        private static NumberFormatInfo nfi = new CultureInfo("es-GT", false).NumberFormat;

        public static void CreatePdf(string destino)
        {
            FileInfo file = new FileInfo(destino);
            file.Directory.Create();


            WriterProperties writerProperties = new WriterProperties();
            PdfWriter writer = new PdfWriter(destino);
            PdfDocument pdf = new PdfDocument(writer);
            Document documento = new Document(pdf);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            Table tablaCabezera = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3 })).UseAllAvailableWidth().SetPadding(0);
            Image Logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\logo.png"));
            Logo.SetHeight(100);

            tablaCabezera.AddCell(new Cell(4, 1).Add(Logo).SetBorder(Border.NO_BORDER));
            tablaCabezera.AddCell(new Cell().Add(new Paragraph("MULTIMARCAS RMD").SetFont(font).SetFontSize(17).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
            tablaCabezera.AddCell(new Cell().Add(new Paragraph("Telefonos: 45137111   58392363").SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetPadding(0).SetMargin(0);
            tablaCabezera.AddCell(new Cell().Add(new Paragraph("Direccion: 3ra Calle 1-20 zona 4 San Pedro Sac. San Marcos").SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
            tablaCabezera.AddCell(new Cell().Add(new Paragraph("Cotizacion").SetFont(font).SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);


            documento.Add(tablaCabezera);
            documento.Close();
        }

        public static Task<bool> CreatePDFCotizacion(List<ProductoCotizacionViewModel> productos, string nombre)
        {
            return (Task.Factory.StartNew(() =>
            {
                if (!Directory.Exists("C:/MultiMarcasApp"))
                {
                    Directory.CreateDirectory("C:/MultiMarcasApp");
                }
                if (!Directory.Exists("C:/MultiMarcasApp/Cotizacion"))
                {
                    Directory.CreateDirectory("C:/MultiMarcasApp/Cotizacion");
                }

                FileInfo file = new FileInfo($"C:/MultiMarcasApp/Cotizacion/Cotizacion.pdf");
                file.Directory.Create();

                WriterProperties writerProperties = new WriterProperties();
                PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/Cotizacion/Cotizacion.pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document documento = new Document(pdf);

                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                Table tablaCabezera = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3 })).UseAllAvailableWidth().SetPadding(0);
                Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\logo.png"));
                logo.SetHeight(100);

                tablaCabezera.AddCell(new Cell(4, 1).Add(logo).SetBorder(Border.NO_BORDER));
                tablaCabezera.AddCell(new Cell().Add(new Paragraph("MULTIMARCAS RMD").SetFont(font).SetFontSize(17).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                tablaCabezera.AddCell(new Cell().Add(new Paragraph("Telefonos: 45137111   58392363").SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetPadding(0).SetMargin(0);
                tablaCabezera.AddCell(new Cell().Add(new Paragraph("Direccion: 3ra Calle 1-20 zona 4 San Pedro Sac. San Marcos").SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                tablaCabezera.AddCell(new Cell().Add(new Paragraph("Cotizacion").SetFont(font).SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);


                Paragraph Fecha = new Paragraph("Fecha: " + DateTime.Now.ToString("d", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0);
                Paragraph Nombre = new Paragraph("Nombre: " + nombre).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(10).SetPadding(0);


                Table tablaProductos = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 9, 2, 2 })).UseAllAvailableWidth().SetPadding(0);
                tablaProductos.AddCell(new Cell().Add(new Paragraph("Cantidad").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph("Codigo").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph("Descripcion").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph("Precio").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph("Total").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                decimal total = 0;
                foreach (var item in productos)
                {
                    tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Cantidad.ToString()).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Producto.Codigo).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Producto.Nombre).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph(item.PrecioVenta.ToString("c", nfi)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Total.ToString("c", nfi)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));
                    total += item.Total;
                }
                tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + Services.Conversores.NumeroALetrasConversor.NumeroALetras(total)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
                tablaProductos.AddCell(new Cell(1, 2).Add(new Paragraph("TOTAL: " + total.ToString("c", nfi)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));

                Paragraph Footer = new Paragraph("Los precios en la cotizacion pueden variar segun existencias sin previo aviso. ").SetFont(font).SetFontSize(13).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0);


                documento.Add(tablaCabezera);
                documento.Add(Fecha);
                documento.Add(Nombre);
                documento.Add(tablaProductos);
                documento.Add(Footer);
                documento.Close();

                return true;
            }));
        }

        public static Task<bool> CreatePDFVenta(List<ProductoVentaViewModel> productos, Venta venta)
        {
            try
            {
                return (Task.Factory.StartNew(() =>
                {
                    if (!Directory.Exists("C:/MultiMarcasApp"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp");
                    }
                    if (!Directory.Exists("C:/MultiMarcasApp/Venta"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp/Venta");
                    }

                    FileInfo file = new FileInfo($"C:/MultiMarcasApp/Venta/Venta.pdf");
                    file.Directory.Create();

                    WriterProperties writerProperties = new WriterProperties();
                    PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/Venta/Venta.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document documento = new Document(pdf);

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    Table tablaCabezera = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3 })).UseAllAvailableWidth().SetPadding(0);
                    Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\logo.png"));
                    logo.SetHeight(100);

                    tablaCabezera.AddCell(new Cell(4, 1).Add(logo).SetBorder(Border.NO_BORDER));
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("MULTIMARCAS RMD").SetFont(font).SetFontSize(19).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("Telefonos: 45137111   58392363").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("Direccion: 3ra Calle 1-20 zona 4 San Pedro Sac. San Marcos").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("Venta #" + venta.Id).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);


                    Paragraph Fecha = new Paragraph("Fecha: " + venta.Fecha).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0);
                    Paragraph Nombre = new Paragraph("Nombre: " + venta.Nombre).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(10).SetPadding(0);


                    Table tablaProductos = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 9, 2, 2 })).UseAllAvailableWidth().SetPadding(0);
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Cantidad").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Codigo").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Descripcion").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Precio").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Total").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    decimal total = 0;
                    foreach (var item in productos)
                    {
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Cantidad.ToString()).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Producto.Codigo).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Producto.Nombre).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.PrecioVenta.ToString("c", nfi)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Total.ToString("c", nfi)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));
                        total += item.Total;
                    }
                    tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + Services.Conversores.NumeroALetrasConversor.NumeroALetras(total)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
                    tablaProductos.AddCell(new Cell(1, 2).Add(new Paragraph("TOTAL: " + total.ToString("c", nfi)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));



                    documento.Add(tablaCabezera);
                    documento.Add(Fecha);
                    documento.Add(Nombre);
                    documento.Add(tablaProductos);
                    documento.Close();
                    return true;
                }));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return new Task<bool>( () => true) ;
            }
            
        }

        public static Task<bool> CreateReporteVentas(List<ReporteVenta> reporteVenta, decimal SaldoInicial, decimal SaldoFinal)
        {
            try
            {
                return (Task.Factory.StartNew(() =>
                {
                    if (!Directory.Exists("C:/MultiMarcasApp"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp");
                    }
                    if (!Directory.Exists("C:/MultiMarcasApp/ReportesVentas"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp/ReportesVentas");
                    }

                    FileInfo file = new FileInfo($"C:/MultiMarcasApp/ReportesVentas/ReporteVentas{reporteVenta.First().Fecha.Month.ToString(nfi)}{reporteVenta.First().Fecha.Year}.pdf");
                    file.Directory.Create();

                    WriterProperties writerProperties = new WriterProperties();
                    PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/ReportesVentas/ReporteVentas{reporteVenta.First().Fecha.Month.ToString(nfi)}{reporteVenta.First().Fecha.Year}.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document documento = new Document(pdf);

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    Table tablaCabezera = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 })).UseAllAvailableWidth().SetPadding(0);
                    Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\Imagen1.png"));
                    logo.SetHeight(150);

                    tablaCabezera.AddCell(new Cell(5, 1).Add(logo).SetBorder(Border.NO_BORDER));
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("MULTIMARCAS RMD").SetFont(bold).SetFontSize(20).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("3A. CALLE 1-20 ZONA 4 SAN PEDRO SACATEPEQUEZ SAN MARCOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("REPORTE DE VENTAS").SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("ROBERTO NEHEMIAS FUENTES OROZCO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph($"CORRESPONDIENTE AL MES " +
                        $"{reporteVenta.First().Fecha.Month.ToString(nfi)} DE {reporteVenta.First().Fecha.Year}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);

                    documento.Add(tablaCabezera);

                    Table TablaVienen = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 })).UseAllAvailableWidth().SetPadding(0);
                    TablaVienen.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaVienen.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaVienen.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaVienen.AddCell(new Cell().Add(new Paragraph("VIENEN").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaVienen.AddCell(new Cell().Add(new Paragraph($"{SaldoInicial.ToString("c",nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(TablaVienen);
                    documento.Add(new Paragraph());

                    Table TablaTitulosReportes = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1, 1})).UseAllAvailableWidth().SetPadding(0);
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("FECHA").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("VENTAS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("GASTOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("DEPOSITOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("PAGO PROV").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("SALDO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(TablaTitulosReportes);
                    documento.Add(new Paragraph());
                    DateTime fecha = reporteVenta.First().Fecha;
                    bool espacio = false;

                    foreach (var item in reporteVenta)
                    {
                        if (item.Fecha != fecha)
                        {
                            espacio = true;
                        }

                        if (espacio)
                        {
                            documento.Add(new Paragraph());
                        }

                        espacio = false;

                        Table TablaReportes = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1,1 })).UseAllAvailableWidth().SetPadding(0);
                        if (item is ReporteVentaVentas)
                        {
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Fecha.ToString("d",nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Monto.ToString("c",nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Saldo.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        }
                        else if(item is ReporteVentaGasto)
                        {
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Fecha.ToString("d", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Monto.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Saldo.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        }
                        else if(item is ReporteVentaDeposito)
                        {
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Fecha.ToString("d", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Monto.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Saldo.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        }
                        else if(item is ReporteVentasPagos)
                        {
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Fecha.ToString("d", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Monto.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Saldo.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        }

                        fecha = item.Fecha;
                        documento.Add(TablaReportes);
                    }

                    Table TablaSaldoFinal = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 })).UseAllAvailableWidth().SetPadding(0);
                    TablaSaldoFinal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaSaldoFinal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaSaldoFinal.AddCell(new Cell(1,2).Add(new Paragraph("SALDO EN CAJA").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaSaldoFinal.AddCell(new Cell().Add(new Paragraph($"{SaldoFinal.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(new Paragraph());
                    documento.Add(TablaSaldoFinal);

                    documento.Close();
                    return true;
                }));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return new Task<bool>(() => true);
            }
        }

        public static Task<bool> CreateReporteGastos(List<GastoViewModel> Gastos)
        {
            try
            {
                return (Task.Factory.StartNew(() =>
                {
                    if (!Directory.Exists("C:/MultiMarcasApp"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp");
                    }
                    if (!Directory.Exists("C:/MultiMarcasApp/ReporteGastos"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp/ReporteGastos");
                    }

                    FileInfo file = new FileInfo($"C:/MultiMarcasApp/ReporteGastos/ReporteGastos{Gastos.First().Pago.Fecha.Month.ToString(nfi)}{Gastos.First().Pago.Fecha.Year}.pdf");
                    file.Directory.Create();

                    WriterProperties writerProperties = new WriterProperties();
                    PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/ReporteGastos/ReporteGastos{Gastos.First().Pago.Fecha.Month.ToString(nfi)}{Gastos.First().Pago.Fecha.Year}.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document documento = new Document(pdf);

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    Table tablaCabezera = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 })).UseAllAvailableWidth().SetPadding(0);
                    Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\Imagen1.png"));
                    logo.SetHeight(150);

                    tablaCabezera.AddCell(new Cell(5, 1).Add(logo).SetBorder(Border.NO_BORDER));
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("MULTIMARCAS RMD").SetFont(bold).SetFontSize(20).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("3A. CALLE 1-20 ZONA 4 SAN PEDRO SACATEPEQUEZ SAN MARCOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("REPORTE DE GASTOS").SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("ROBERTO NEHEMIAS FUENTES OROZCO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph($"CORRESPONDIENTE AL MES {Gastos.First().Pago.Fecha.Month.ToString(nfi)} DE {Gastos.First().Pago.Fecha.Year}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);

                    documento.Add(tablaCabezera);

                    Table TablaTitulosReportes = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 4})).UseAllAvailableWidth().SetPadding(0);
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("FECHA").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("MONTO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("DESCRIPCION").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(TablaTitulosReportes);
                    documento.Add(new Paragraph());

                    decimal Total = 0;

                    foreach (var item in Gastos)
                    {
                        Table TablaReportes = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 4})).UseAllAvailableWidth().SetPadding(0);

                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Pago.Fecha.ToString("d", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Pago.Monto.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"Pago {item.TipoGasto.Nombre} No. Doc. {item.Pago.NoDoc}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        Total += item.Pago.Monto;

                        documento.Add(TablaReportes);
                    }

                    Table TablaTotal = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 })).UseAllAvailableWidth().SetPadding(0);
                    TablaTotal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaTotal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaTotal.AddCell(new Cell(1, 2).Add(new Paragraph("TOTAL GASTOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTotal.AddCell(new Cell().Add(new Paragraph($"{Total.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(new Paragraph());
                    documento.Add(TablaTotal);

                    documento.Close();
                    return true;
                }));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return new Task<bool>(() => true);
            }
        }
        public static Task<bool> CreateReportePagosProveedores(List<PagoProveedor> pagos)
        {
            try
            {
                return (Task.Factory.StartNew(() =>
                {
                    if (!Directory.Exists("C:/MultiMarcasApp"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp");
                    }
                    if (!Directory.Exists("C:/MultiMarcasApp/ReportePagosProveedores"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp/ReportePagosProveedores");
                    }

                    FileInfo file = new FileInfo($"C:/MultiMarcasApp/ReportePagosProveedores/ReportePagosProveedores{pagos.First().Fecha.Month.ToString(nfi)}{pagos.First().Fecha.Year}.pdf");
                    file.Directory.Create();

                    WriterProperties writerProperties = new WriterProperties();
                    PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/ReportePagosProveedores/ReportePagosProveedores{pagos.First().Fecha.Month.ToString(nfi)}{pagos.First().Fecha.Year}.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document documento = new Document(pdf, PageSize.LEGAL.Rotate());
                    
                    

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    Table tablaCabezera = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 })).UseAllAvailableWidth().SetPadding(0);
                    Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\Imagen1.png"));
                    logo.SetHeight(120);

                    tablaCabezera.AddCell(new Cell(5, 1).Add(logo).SetBorder(Border.NO_BORDER));
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("MULTIMARCAS RMD").SetFont(bold).SetFontSize(20).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("3A. CALLE 1-20 ZONA 4 SAN PEDRO SACATEPEQUEZ SAN MARCOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("REPORTE DE GASTOS").SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("ROBERTO NEHEMIAS FUENTES OROZCO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph($"CORRESPONDIENTE AL MES {pagos.First().Fecha.Month.ToString(nfi)} DE {pagos.First().Fecha.Year}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);

                    documento.Add(tablaCabezera);

                    Table TablaTitulosReportes = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 1.5f, 1.5f, 1, 1 })).UseAllAvailableWidth().SetPadding(0);
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("FECHA").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("PROVEEDOR").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("NO FACTURA").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("NO DOC PAGO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("TIPO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("MONTO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(TablaTitulosReportes);
                    documento.Add(new Paragraph());

                    decimal Total = 0;

                    foreach (var item in pagos)
                    {
                        Table TablaReportes = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 1.5f, 1.5f, 1, 1 })).UseAllAvailableWidth().SetPadding(0);

                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Fecha.ToString("d", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.proveedor.RazonSocial}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.NoDocFactura}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.NoDocPago}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.TipoPago}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Monto.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        Total += item.Monto;

                        documento.Add(TablaReportes);
                    }

                    Table TablaTotal = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 })).UseAllAvailableWidth().SetPadding(0);
                    TablaTotal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaTotal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaTotal.AddCell(new Cell(1, 2).Add(new Paragraph("TOTAL PAGOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTotal.AddCell(new Cell().Add(new Paragraph($"{Total.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(new Paragraph());
                    documento.Add(TablaTotal);

                    documento.Close();
                    return true;
                }));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return new Task<bool>(() => true);
            }
        }
        public static Task<bool> CreateReporteInventario(List<ReporteInventarioProducto> productos, DateTime fecha)
        {
            try
            {
                return (Task.Factory.StartNew(() =>
                {
                    if (!Directory.Exists("C:/MultiMarcasApp"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp");
                    }
                    if (!Directory.Exists("C:/MultiMarcasApp/ReportesInventarios"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp/ReportesInventarios");
                    }

                    FileInfo file = new FileInfo($"C:/MultiMarcasApp/ReportesInventarios/ReporteInventario{fecha.ToShortDateString().Replace("/","-")}.pdf");
                    file.Directory.Create();

                    WriterProperties writerProperties = new WriterProperties();
                    PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/ReportesInventarios/ReporteInventario{fecha.ToShortDateString().Replace("/", "-")}.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document documento = new Document(pdf);

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    Table tablaCabezera = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 })).UseAllAvailableWidth().SetPadding(0);
                    Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\Imagen1.png"));
                    logo.SetHeight(150);

                    tablaCabezera.AddCell(new Cell(5, 1).Add(logo).SetBorder(Border.NO_BORDER));
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("MULTIMARCAS RMD").SetFont(bold).SetFontSize(20).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("3A. CALLE 1-20 ZONA 4 SAN PEDRO SACATEPEQUEZ SAN MARCOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("REPORTE DE INVENTARIO").SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("ROBERTO NEHEMIAS FUENTES OROZCO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph($"INVENTARIO AL {fecha.ToShortDateString()}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);

                    documento.Add(tablaCabezera);

                    Table TablaTitulosReportes = new Table(UnitValue.CreatePercentArray(new float[] { 6, 1.75f, 2, 2})).UseAllAvailableWidth().SetPadding(0);
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("DESCRIPCION").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("INVENTARIO FINAL").SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("COSTO UNITARIO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("COSTO TOTAL").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(TablaTitulosReportes);
                    documento.Add(new Paragraph());

                    decimal Total = 0;

                    foreach (var item in productos)
                    {
                        if(item.Stock > 0)
                        {
                            Table TablaReportes = new Table(UnitValue.CreatePercentArray(new float[] { 6, 1.75f, 2, 2 })).UseAllAvailableWidth().SetPadding(0);

                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Descripcion}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Stock}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.PrecioCompra.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));
                            TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Total.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));
                            Total += item.Total;

                            documento.Add(TablaReportes);
                        }
                    }

                    Table TablaTotal = new Table(UnitValue.CreatePercentArray(new float[] { 6, 1.75f, 2, 2 })).UseAllAvailableWidth().SetPadding(0);
                    TablaTotal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaTotal.AddCell(new Cell(1,2).Add(new Paragraph("TOTAL").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTotal.AddCell(new Cell().Add(new Paragraph($"{Total.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));

                    TablaTotal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaTotal.AddCell(new Cell(1,2).Add(new Paragraph("TOTAL SIN IVA").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTotal.AddCell(new Cell().Add(new Paragraph($"{(Total/(decimal)(1.12)).ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));

                    documento.Add(new Paragraph());
                    documento.Add(TablaTotal);

                    documento.Close();
                    return true;
                }));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return new Task<bool>(() => true);
            }
        }

        public static Task<bool> CreateReporteDepositos(List<DepositoViewModel> Depositos)
        {
            try
            {
                return (Task.Factory.StartNew(() =>
                {
                    if (!Directory.Exists("C:/MultiMarcasApp"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp");
                    }
                    if (!Directory.Exists("C:/MultiMarcasApp/ReporteDepositos"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp/ReporteDepositos");
                    }

                    FileInfo file = new FileInfo($"C:/MultiMarcasApp/ReporteDepositos/ReporteDepositos{Depositos.First().Fecha.Month.ToString(nfi)}{Depositos.First().Fecha.Year}.pdf");
                    file.Directory.Create();

                    WriterProperties writerProperties = new WriterProperties();
                    PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/ReporteDepositos/ReporteDepositos{Depositos.First().Fecha.Month.ToString(nfi)}{Depositos.First().Fecha.Year}.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document documento = new Document(pdf);

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    Table tablaCabezera = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 })).UseAllAvailableWidth().SetPadding(0);
                    Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\Imagen1.png"));
                    logo.SetHeight(150);

                    tablaCabezera.AddCell(new Cell(5, 1).Add(logo).SetBorder(Border.NO_BORDER));
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("MULTIMARCAS RMD").SetFont(bold).SetFontSize(20).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("3A. CALLE 1-20 ZONA 4 SAN PEDRO SACATEPEQUEZ SAN MARCOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("REPORTE DE DEPOSITOS").SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph("ROBERTO NEHEMIAS FUENTES OROZCO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);
                    tablaCabezera.AddCell(new Cell().Add(new Paragraph($"CORRESPONDIENTE AL MES {Depositos.First().Fecha.Month.ToString(nfi)} DE {Depositos.First().Fecha.Year}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetMargin(0).SetPadding(0)).SetPadding(0).SetMargin(0);

                    documento.Add(tablaCabezera);

                    Table TablaTitulosReportes = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 4 })).UseAllAvailableWidth().SetPadding(0);
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("FECHA").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("MONTO").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTitulosReportes.AddCell(new Cell().Add(new Paragraph("DESCRIPCION").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(TablaTitulosReportes);
                    documento.Add(new Paragraph());

                    decimal Total = 0;

                    foreach (var item in Depositos)
                    {
                        Table TablaReportes = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 4 })).UseAllAvailableWidth().SetPadding(0);

                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Fecha.ToString("d", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"{item.Monto.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                        TablaReportes.AddCell(new Cell().Add(new Paragraph($"Depositos a MULTIMARCAS RMD numero de Boleta {item.NoDoc} " ).SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
                        Total += item.Monto;

                        documento.Add(TablaReportes);
                    }

                    Table TablaTotal = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 })).UseAllAvailableWidth().SetPadding(0);
                    TablaTotal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaTotal.AddCell(new Cell().Add(new Paragraph("").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    TablaTotal.AddCell(new Cell(1, 2).Add(new Paragraph("TOTAL DEPOSITOS").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));
                    TablaTotal.AddCell(new Cell().Add(new Paragraph($"{Total.ToString("c", nfi)}").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)));

                    documento.Add(new Paragraph());
                    documento.Add(TablaTotal);

                    documento.Close();
                    return true;
                }));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return new Task<bool>(() => true);
            }
        }
    
        public static Task<bool> CreatePDFVenta8mm(List<ProductoVentaViewModel> productos, Venta venta)
        {
            try
            {
                return (Task.Factory.StartNew(() =>
                {
                    float heigth = GetHeightOfPDFVenta8mm(productos, venta);
                    if (!Directory.Exists("C:/MultiMarcasApp"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp");
                    }
                    if (!Directory.Exists("C:/MultiMarcasApp/Venta"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp/Venta");
                    }

                    FileInfo file = new FileInfo($"C:/MultiMarcasApp/Venta/Venta.pdf");
                    file.Directory.Create();

                    WriterProperties writerProperties = new WriterProperties();
                    PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/Venta/Venta.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document documento = new Document(pdf, new PageSize(220, heigth));
                    documento.SetMargins(0, 0, 0, 0);
                    

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    

                    Table tablaPadre = new Table(1).UseAllAvailableWidth().SetMargin(0).SetPadding(0);
                    tablaPadre.SetWidth(220);

                    Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\logo.png"));
                    logo.SetWidth(160);
                    tablaPadre.AddCell(new Cell().Add(logo.SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetBorder(Border.NO_BORDER));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph("Direccion: 3ra Calle 1-20 zona 4 San Pedro Sac. San Marcos")).SetBorder(Border.NO_BORDER).SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph("Telefonos: 45137111  58392363").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph($"Fecha Venta {venta.Fecha.ToString("d", nfi)}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph($"Venta #{venta.Id}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph($"Nombre: {venta.Nombre}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));


                    Table tablaProductos = new Table(3).UseAllAvailableWidth().SetPadding(0);
                    tablaProductos.SetWidth(220);
                    tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("Producto").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(0.5f)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Cantidad").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Precio").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("SubTotal").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));


                    decimal total = 0;
                    foreach (var item in productos)
                    {
                        tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph(item.Producto.Nombre).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(0.5f)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Cantidad.ToString()).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.PrecioVenta.ToString("c", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Total.ToString("c", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));

                        total += item.Total;
                    }
                    tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + Services.Conversores.NumeroALetrasConversor.NumeroALetras(total)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + total.ToString("c", nfi)).SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));

                    documento.Add(tablaPadre);
                    documento.Add(tablaProductos);

                    documento.Close();

                    return true;
                }));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return new Task<bool>(() => true);
            }
        }

        public static float GetHeightOfPDFVenta8mm(List<ProductoVentaViewModel> productos, Venta venta)
        {
            if (!Directory.Exists("C:/MultiMarcasApp"))
            {
                Directory.CreateDirectory("C:/MultiMarcasApp");
            }
            if (!Directory.Exists("C:/MultiMarcasApp/Venta"))
            {
                Directory.CreateDirectory("C:/MultiMarcasApp/Venta");
            }

            FileInfo file = new FileInfo($"C:/MultiMarcasApp/Venta/Venta.pdf");
            file.Directory.Create();

            WriterProperties writerProperties = new WriterProperties();
            PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/Venta/Venta.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document documento = new Document(pdf);
            documento.SetMargins(0, 0, 0, 0);


            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);


            Table tablaPadre = new Table(1).UseAllAvailableWidth().SetMargin(0).SetPadding(0);
            tablaPadre.SetWidth(220);

            Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\logo.png"));
            logo.SetWidth(160);
            tablaPadre.AddCell(new Cell().Add(logo.SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetBorder(Border.NO_BORDER));
            tablaPadre.AddCell(new Cell().Add(new Paragraph("Direccion: 3ra Calle 1-20 zona 4 San Pedro Sac. San Marcos")).SetBorder(Border.NO_BORDER).SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0));
            tablaPadre.AddCell(new Cell().Add(new Paragraph("Telefonos: 45137111  58392363").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
            tablaPadre.AddCell(new Cell().Add(new Paragraph($"Fecha Venta {DateTime.Now.ToString("d", nfi)}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
            tablaPadre.AddCell(new Cell().Add(new Paragraph($"Venta #{venta.Id}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
            tablaPadre.AddCell(new Cell().Add(new Paragraph($"Nombre: {venta.Nombre}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));


            Table tablaProductos = new Table(3).UseAllAvailableWidth().SetPadding(0);
            tablaProductos.SetWidth(220);
            tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("Producto").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(0.5f)));
            tablaProductos.AddCell(new Cell().Add(new Paragraph("Cantidad").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
            tablaProductos.AddCell(new Cell().Add(new Paragraph("Precio").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
            tablaProductos.AddCell(new Cell().Add(new Paragraph("SubTotal").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));


            decimal total = 0;
            foreach (var item in productos)
            {
                tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph(item.Producto.Nombre).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(0.5f)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Cantidad.ToString()).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph(item.PrecioVenta.ToString("c", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Total.ToString("c", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));

                total += item.Total;
            }
            tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + Services.Conversores.NumeroALetrasConversor.NumeroALetras(total)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)));
            tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + total.ToString("c", nfi)).SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)));

            documento.Add(tablaPadre);
            documento.Add(tablaProductos);


            IRenderer tableRenderer = tablaPadre.CreateRendererSubTree().SetParent(documento.GetRenderer());
            LayoutResult tableLayoutResult = tableRenderer.Layout(new LayoutContext(new LayoutArea(0, new Rectangle(220, 2000))));
            IRenderer tableRenderer2 = tablaProductos.CreateRendererSubTree().SetParent(documento.GetRenderer());
            LayoutResult tableLayoutResult2 = tableRenderer2.Layout(new LayoutContext(new LayoutArea(0, new Rectangle(220, 2000))));

            float heigth = tableLayoutResult.GetOccupiedArea().GetBBox().GetHeight();
            heigth += tableLayoutResult2.GetOccupiedArea().GetBBox().GetHeight();

            documento.Close();

            return heigth;
            
        }

        public static Task<bool> CreatePDFCoticacion80mm(List<ProductoVentaViewModel> productos, string nombre)
        {
            try
            {
                return (Task.Factory.StartNew(() =>
                {
                    float heigth = GetHeightOfPDFCotizacion80mm(productos, nombre);
                    if (!Directory.Exists("C:/MultiMarcasApp"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp");
                    }
                    if (!Directory.Exists("C:/MultiMarcasApp/Cotizacion"))
                    {
                        Directory.CreateDirectory("C:/MultiMarcasApp/Cotizacion");
                    }

                    FileInfo file = new FileInfo($"C:/MultiMarcasApp/Cotizacion/Cotizacion.pdf");
                    file.Directory.Create();

                    WriterProperties writerProperties = new WriterProperties();
                    PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/Venta/Cotizacion.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document documento = new Document(pdf, new PageSize(220, heigth));
                    documento.SetMargins(0, 0, 0, 0);


                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);


                    Table tablaPadre = new Table(1).UseAllAvailableWidth().SetMargin(0).SetPadding(0);
                    tablaPadre.SetWidth(220);

                    Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\logo.png"));
                    logo.SetWidth(160);
                    tablaPadre.AddCell(new Cell().Add(logo.SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetBorder(Border.NO_BORDER));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph("Direccion: 3ra Calle 1-20 zona 4 San Pedro Sac. San Marcos")).SetBorder(Border.NO_BORDER).SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph("Telefonos: 45137111  58392363").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph($"Fecha COTIZACION {DateTime.Now.ToString("d", nfi)}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    tablaPadre.AddCell(new Cell().Add(new Paragraph($"Nombre: {nombre}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));


                    Table tablaProductos = new Table(3).UseAllAvailableWidth().SetPadding(0);
                    tablaProductos.SetWidth(220);
                    tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("Producto").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(0.5f)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Cantidad").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("Precio").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                    tablaProductos.AddCell(new Cell().Add(new Paragraph("SubTotal").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));


                    decimal total = 0;
                    foreach (var item in productos)
                    {
                        tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph(item.Producto.Nombre).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(0.5f)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Cantidad.ToString()).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.PrecioVenta.ToString("c", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                        tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Total.ToString("c", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));

                        total += item.Total;
                    }
                    tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + Services.Conversores.NumeroALetrasConversor.NumeroALetras(total)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
                    tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + total.ToString("c", nfi)).SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));

                    documento.Add(tablaPadre);
                    documento.Add(tablaProductos);

                    documento.Close();

                    return true;
                }));
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return new Task<bool>(() => true);
            }
        }

        public static float GetHeightOfPDFCotizacion80mm(List<ProductoVentaViewModel> productos, string nombre)
        {
            if (!Directory.Exists("C:/MultiMarcasApp"))
            {
                Directory.CreateDirectory("C:/MultiMarcasApp");
            }
            if (!Directory.Exists("C:/MultiMarcasApp/Cotizacion"))
            {
                Directory.CreateDirectory("C:/MultiMarcasApp/Cotizacion");
            }

            FileInfo file = new FileInfo($"C:/MultiMarcasApp/Cotizacion/Cotizacion.pdf");
            file.Directory.Create();

            WriterProperties writerProperties = new WriterProperties();
            PdfWriter writer = new PdfWriter($"C:/MultiMarcasApp/Venta/Cotizacion.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document documento = new Document(pdf);
            documento.SetMargins(0, 0, 0, 0);


            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);


            Table tablaPadre = new Table(1).UseAllAvailableWidth().SetMargin(0).SetPadding(0);
            tablaPadre.SetWidth(220);

            Image logo = new Image(ImageDataFactory.Create("C:\\MultiMarcasApp\\Imagenes\\logo.png"));
            logo.SetWidth(160);
            tablaPadre.AddCell(new Cell().Add(logo.SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetBorder(Border.NO_BORDER));
            tablaPadre.AddCell(new Cell().Add(new Paragraph("Direccion: 3ra Calle 1-20 zona 4 San Pedro Sac. San Marcos")).SetBorder(Border.NO_BORDER).SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0));
            tablaPadre.AddCell(new Cell().Add(new Paragraph("Telefonos: 45137111  58392363").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
            tablaPadre.AddCell(new Cell().Add(new Paragraph($"Fecha COTIZACION {DateTime.Now.ToString("d", nfi)}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
            tablaPadre.AddCell(new Cell().Add(new Paragraph($"Nombre: {nombre}").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));


            Table tablaProductos = new Table(3).UseAllAvailableWidth().SetPadding(0);
            tablaProductos.SetWidth(220);
            tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("Producto").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(0.5f)));
            tablaProductos.AddCell(new Cell().Add(new Paragraph("Cantidad").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
            tablaProductos.AddCell(new Cell().Add(new Paragraph("Precio").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
            tablaProductos.AddCell(new Cell().Add(new Paragraph("SubTotal").SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));


            decimal total = 0;
            foreach (var item in productos)
            {
                tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph(item.Producto.Nombre).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(0.5f)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Cantidad.ToString()).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph(item.PrecioVenta.ToString("c", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));
                tablaProductos.AddCell(new Cell().Add(new Paragraph(item.Total.ToString("c", nfi)).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(0.5f)));

                total += item.Total;
            }
            tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + Services.Conversores.NumeroALetrasConversor.NumeroALetras(total)).SetFont(font).SetFontSize(9).SetTextAlignment(TextAlignment.LEFT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));
            tablaProductos.AddCell(new Cell(1, 3).Add(new Paragraph("TOTAL: " + total.ToString("c", nfi)).SetFont(bold).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT).SetMargin(0).SetPadding(0)).SetBorder(Border.NO_BORDER));

            documento.Add(tablaPadre);
            documento.Add(tablaProductos);

            IRenderer tableRenderer = tablaPadre.CreateRendererSubTree().SetParent(documento.GetRenderer());
            LayoutResult tableLayoutResult = tableRenderer.Layout(new LayoutContext(new LayoutArea(0, new Rectangle(220, 2000))));
            IRenderer tableRenderer2 = tablaProductos.CreateRendererSubTree().SetParent(documento.GetRenderer());
            LayoutResult tableLayoutResult2 = tableRenderer2.Layout(new LayoutContext(new LayoutArea(0, new Rectangle(220, 2000))));

            float heigth = tableLayoutResult.GetOccupiedArea().GetBBox().GetHeight();
            heigth += tableLayoutResult2.GetOccupiedArea().GetBBox().GetHeight();

            documento.Close();

            return heigth;
        }
    }
}

        