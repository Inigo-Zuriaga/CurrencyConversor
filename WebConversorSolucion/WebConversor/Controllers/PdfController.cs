
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
        
        
        // Endpoint para generar un PDF con el historial de conversiones
        [HttpPost]
        public IActionResult GenerarListado([FromBody] List<HistoryRequest> history)
        {
            var pdfBytes = _pdfService.GenerarListadoPdf(history);
            return File(pdfBytes, "application/pdf", "Historial_Conversiones.pdf");
        }

        //Datos cargados de prueba
        // private List<HistoryRequest> ObtenerDatos()
        // {
        //     return new List<HistoryRequest>
        //     {
        //         new() { FromAmount=20,FromCoin = "USD", ToAmount =30, ToCoin="EUR", Date=DateTime.Now,Email="adrian@gmail.com",Name="Adrian", LastName="Garcia" },
        //         new() { FromAmount=30,FromCoin = "LIB", ToAmount =90, ToCoin="USD", Date=DateTime.Now,Email="adrian@gmail.com",Name="Adrian", LastName="Garcia" },
        //         new() { FromAmount=560,FromCoin = "EUR", ToAmount =730, ToCoin="USD", Date=DateTime.Now,Email="adrian@gmail.com", Name="Adrian", LastName="Garcia" },
        //     };
        // }
}

