using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace WebConversor.Services;

public class PdfService
{
    public byte[] GenerarListadoPdf(List<HistoryRequest> data)
    {
        QuestPDF.Settings.License = LicenseType.Community;

     
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "icono-pdf.png");

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                // Configuración de la página
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Inch);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                // Encabezado
                page.Header().Row(row =>
                {
                    row.ConstantItem(150)
                        .Image(imagePath);
                    
                    row.RelativeItem()
                        .AlignMiddle()
                        .Column(column =>
                        {
                            column.Item().Text("Datos Cliente").FontSize(20).Bold();
                            column.Item().Text("Nombre: El nombre").FontSize(10);
                            column.Item().Text("Apellido: El apellido");
                            column.Item().Text("Correo: El correo");
                        });
            
                });

                // Contenido principal
                page.Content().Stack(content =>
                {
                    content.Spacing(10); // Espaciado entre secciones

                    // // Sección "From" y "For"
                    // content.Item().Row(row =>
                    // {
                    //     row.RelativeColumn().Stack(stack =>
                    //     {
                    //         stack.Spacing(5);
                    //         stack.Item().Text("From").Bold();
                    //         stack.Item().Text("Asumenda Recusandae");
                    //         stack.Item().Text("Consequatur eligendi");
                    //         stack.Item().Text("molestias78@example.com");
                    //         stack.Item().Text("215-792-3344");
                    //     });
                    //
                    //     row.RelativeColumn().Stack(stack =>
                    //     {
                    //         stack.Spacing(5);
                    //         stack.Item().Text("For").Bold();
                    //         stack.Item().Text("Obcaecati Consequuntur");
                    //         stack.Item().Text("Illo itaque debitis");
                    //         stack.Item().Text("laudantium19@inventore.com");
                    //         stack.Item().Text("559-400-7892");
                    //     });
                    // });
                    content.Item()
                        // .AlignCenter()
                        
                        .Text("Historial de Cambios").FontSize(20).Bold();
                    // Espaciado antes de la tabla
                    content.Item().PaddingVertical(6);

                    // Tabla
                    content.Item().Table(table =>
                    {
                        // Definir columnas
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1); // Producto
                            columns.RelativeColumn(1); // Precio Unitario
                            columns.RelativeColumn(1); // Total
                        });

                        // Encabezado de la tabla
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Moneda Base").Bold();
                            header.Cell().Element(CellStyle).Text("Cambio Obtenido").Bold();
                            header.Cell().Element(CellStyle).Text("Fecha").Bold();
                        });

                        // Filas dinámicas
                        foreach (var item in data)
                        {
                            table.Cell().Element(CellStyle).Text(item.FromAmount +" "+ item.FromCoin); // Producto
                            // table.Cell().Element(CellStyle).Text($"{item.FromAmount:C}"); // Precio Unitario
                            table.Cell().Element(CellStyle).Text(item.ToAmount+" "+item.ToCoin); // Total
                            table.Cell().Element(CellStyle).Text(item.Date.ToString("dd/MM/yyyy HH:mm")); // Total
                        }

                        // Estilo de celdas
                        static IContainer CellStyle(IContainer container) =>
                            container
                                .Padding(0)
                                .PaddingTop(3)
                                .PaddingBottom(3)
                                .BorderBottom(1)
                                // .Background(Colors.Grey.Lighten3)
                                .BorderColor(Colors.Grey.Lighten2);
                        
                    });
                });

                // Pie de página
                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Page ");
                    text.CurrentPageNumber();
                    text.Span(" of ");
                    text.TotalPages();
                });
            });
        });
        
        return documento.GeneratePdf();
    }
}