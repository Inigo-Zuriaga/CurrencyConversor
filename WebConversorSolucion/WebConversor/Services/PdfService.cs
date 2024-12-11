using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace WebConversor.Services;

public class PdfService
{
    public byte[] GenerarListadoPdf(List<HistoryRequest> data)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Inch);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Text("Listado de Items").FontSize(20).Bold().AlignCenter();
                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("ID").Bold();
                        header.Cell().Text("Nombre").Bold();
                        header.Cell().Text("Descripción").Bold();
                    });

                    foreach (var item in data)
                    {
                        table.Cell().Text(item.FromAmount);
                        table.Cell().Text(item.FromCoin);
                        table.Cell().Text(item.ToCoin);
                    }
                });

                page.Footer().AlignRight().Text(x =>
                {
                    x.Span("Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        });

        return documento.GeneratePdf();
    }
}