
using DinkToPdf;
using DinkToPdf.Contracts;
using WebConversor.Helpers;

namespace WebConversor.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        
        private readonly PdfService _pdfService;

        public PdfController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }
        
        [HttpPost]
        public IActionResult GenerarListado([FromBody] List<HistoryRequest> history)
        {
            var pdfBytes = _pdfService.GenerarListadoPdf(history);
            return File(pdfBytes, "application/pdf", "Historial_Conversiones.pdf");
        }

        private List<HistoryRequest> ObtenerDatos()
        {
            return new List<HistoryRequest>
            {
                new() { FromAmount=20,FromCoin = "USD", ToAmount =30, ToCoin="EUR", Date=DateTime.Now,Email="adrian@gmail.com",Name="Adrian", LastName="Garcia" },
                new() { FromAmount=30,FromCoin = "LIB", ToAmount =90, ToCoin="USD", Date=DateTime.Now,Email="adrian@gmail.com",Name="Adrian", LastName="Garcia" },
                new() { FromAmount=560,FromCoin = "EUR", ToAmount =730, ToCoin="USD", Date=DateTime.Now,Email="adrian@gmail.com", Name="Adrian", LastName="Garcia" },
            };
        }
        
        
        //private readonly IConverter _converter;
        
        //public PdfController(IConverter converter)
        //{
        //    _converter = converter;
        //}

        //[HttpGet]
        //[Route("generate")]
        //public async Task<IActionResult> Generate()
        //{
        //    await PdfHelper.Example();
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("generate-from-html")]
        //public async Task<IActionResult> GenerateFromHtml()
        //{
        //    await PdfHelper.GenerateProduct();
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("generate")]
        //public async Task<IActionResult> DownloadV2()
        //{
        //    var result = await PdfHelper.GetProduct();
        //    string filename = $"product-report-{DateTime.Now.ToString("yyMMddHHmmss")}.pdf";
        //    return File(result, MediaTypeNames.Application.Pdf, filename);
        //}

        
        // [HttpPost]
        // public async Task<IActionResult> GeneratePdf([FromBody] List<HistoryRequest> history)
        // {
        //     var result = await PdfHelper.GetProduct(history);
        //     string filename = $"product-report-{DateTime.Now.ToString("yyMMddHHmmss")}.pdf";
        //     return File(result, MediaTypeNames.Application.Pdf, filename);
        // }


    //EL PRIMER PDF
    //[HttpPost]
    //public async Task<IActionResult> GeneratePdf([FromBody] List<HistoryRequest> history)
    //{


    //    string filename= "test.pdf";
    //    var glb=new GlobalSettings()
    //    {
    //        ColorMode = ColorMode.Color,
    //        Orientation = Orientation.Portrait,
    //        PaperSize = PaperKind.A4,
    //        Margins = new MarginSettings()
    //        {
    //            Bottom = 10,
    //            Left = 10,
    //            Right = 10,
    //            Top = 10
    //        },
    //        DocumentTitle = "PDF Report",
    //        // Out = Path.Combine(Directory.GetCurrentDirectory(), "Exports", filename)
    //        // Out = Path.Combine(Directory.GetCurrentDirectory(), filename)
    //    };
    //    var objectSettings = new ObjectSettings()
    //    {
    //        PagesCount = true,
    //        HtmlContent = HistoryService.ToHtmlFile(history),
    //        WebSettings = {DefaultEncoding = "utf-8", UserStyleSheet = null}
    //    };
    //    var pdf = new HtmlToPdfDocument()
    //    {
    //        GlobalSettings = glb,
    //        Objects = {objectSettings}
    //    };
    //    var pdfDoc=_converter.Convert(pdf);


    //    return File(pdfDoc, "application/pdf", "test.pdf");
    //}

    
    
    

}

