

namespace WebConversor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContexto _dbContexto;

        public HomeController(ILogger<HomeController> logger,DbContexto contexto)
        {
            _logger = logger;
            _dbContexto = contexto;
        }

        public IActionResult Index()
        {

            List<Moneda> lista = _dbContexto.Monedas.ToList();
            return View(lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AcercaDe()
        {
            ViewBag.Descripcion = "La descripcion esta BACANA";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
