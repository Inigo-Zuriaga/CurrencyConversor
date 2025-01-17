using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace WebConversor.Services;

public class PdfService
{
    public byte[] GenerarListadoPdf(List<HistoryRequest> data)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Logo_Nombre2.png");

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                // Configuración de la página
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                // Encabezado
                page.Header().Row(row =>
                {
                    row.ConstantItem(250)
                        .Image(imagePath);
                        
                    
                    row.RelativeItem()
                        .AlignMiddle()
                        .AlignRight()
                        .Column(column =>
                        {
                            column.Item().Text("Datos Cliente").FontSize(20).Bold();
                            column.Item().Text($"Nombre: {data[0].Name}").FontSize(10);
                            column.Item().Text($"Apellido: {data[0].LastName}").FontSize(10);
                            column.Item().Text($"Correo: {data[0].Email}").FontSize(10);
                            column.Item().Text($"Fecha Creación: {DateTime.Now.ToString("dd/MM/yyyy")}").FontSize(10);
                            
                        });
                
                });
        
                // Contenido principal
                page.Content().Stack(content =>
                {
                    content.Spacing(10); // Espaciado entre secciones
                    content.Item()
                        .PaddingTop(30)
                        .Text("Historial de Cambios").FontSize(20).Bold();
                    
                    // Espaciado antes de la tabla
                    content.Item().PaddingVertical(6);
                    
                    // Tabla
                    content.Item().Table(table =>
                    {
                        // Definir columnas
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });

                        // Encabezado de la tabla
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).AlignMiddle().Text("Moneda Base").Bold();
                            header.Cell().Element(CellStyle).AlignMiddle().Text("Cambio Obtenido").Bold();
                            header.Cell().Element(CellStyle).AlignMiddle().Text("Fecha").Bold();
                        });
                        
                        for (var i=0;i< data.Count; i++)
                        {
                         
                                table.Cell().Element(c => Cell(c, i)).Text(data[i].FromAmount + " " + data[i].FromCoin); // Producto
                                table.Cell().Element(c => Cell(c, i)).Text(data[i].ToAmount+" "+data[i].ToCoin); // Total
                                table.Cell().Element(c => Cell(c, i)).Text(data[i].Date.ToString("dd/MM/yyyy HH:mm")); // Total
                        }
                        
                        // Estilo de celdas
                        static IContainer CellStyle(IContainer container)
                        {
                            return container
                                .Border(1)
                                .Background(Colors.Grey.Lighten3)
                                .BorderColor(Colors.Grey.Darken1)
                                .PaddingVertical(5)
                                .PaddingHorizontal(10)
                                .AlignCenter()
                                .AlignMiddle();
                        }

                        static IContainer Cell( IContainer container,int index)
                        {
                            return container
                                .Border(1)
                                .Background(index % 2 == 0 ? Colors.LightBlue.Lighten5 : Colors.Teal.Lighten4) // Alterna el color según el índice
                                .Padding(6)
                                .AlignCenter();
                        }
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