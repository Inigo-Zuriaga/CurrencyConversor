using PuppeteerSharp;
using System.Text;


namespace WebConversor.Helpers
{
    //public class PdfHelper
    //{
    //    private static Browser? browser;
    //    private static readonly BrowserFetcher fetcher = new();
    //    private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);


    //    private static async Task<string> DownloadChromiumAsync()
    //    {
    //        var tempPath = Path.Combine(Path.GetTempPath(), "chromium");
    //        var chromiumZip = Path.Combine(tempPath, "chromium.zip");

    //        if (!Directory.Exists(tempPath))
    //            Directory.CreateDirectory(tempPath);

    //        var chromiumUrl = "https://storage.googleapis.com/chromium-browser-snapshots/Win_x64/1036824/chrome-win.zip";

    //        using var httpClient = new HttpClient();
    //        var chromiumData = await httpClient.GetByteArrayAsync(chromiumUrl);

    //        await File.WriteAllBytesAsync(chromiumZip, chromiumData);

    //        System.IO.Compression.ZipFile.ExtractToDirectory(chromiumZip, tempPath);

    //        return Path.Combine(tempPath, "chrome-win", "chrome.exe");
    //    }


    //    public static async Task<Browser> GetBrowserAsync()
    //    {


    //        if (browser == null)
    //        {
    //            await semaphoreSlim.WaitAsync();
    //            try
    //            {
    //                //if (browser == null)
    //                //{
    //                //    //await fetcher.DownloadAsync();
    //                //    browser = (Browser)await Puppeteer.LaunchAsync(new LaunchOptions
    //                //    {
    //                //        Headless = true,
    //                //        Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" },
    //                //        ExecutablePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"

    //                //    });
    //                //}

    //                var chromiumPath = await DownloadChromiumAsync();

    //                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
    //                {
    //                    Headless = true,
    //                    ExecutablePath = chromiumPath,
    //                    Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
    //                });

    //            }
    //            finally
    //            {
    //                semaphoreSlim.Release();
    //            }
    //        }
    //        return browser;
    //    }

    //    private static async Task<byte[]> GeneratePdfAsync(string content, PdfOptions pdfOptions)
    //    {
    //        var browser1 = await GetBrowserAsync();
    //        await using var page = await browser1.NewPageAsync();
    //        await page.SetContentAsync(content);

    //        var pdfStream = await page.PdfStreamAsync(pdfOptions);
    //        var result = ToByteArray(pdfStream);
    //        pdfStream.Dispose();

    //        return result;
    //    }

    //    public static async Task<byte[]> GetProduct(List<HistoryRequest> data)
    //    {
    //        var content = await System.IO.File.ReadAllTextAsync(Path.Combine("HtmlTemplates", "History.html"));
    //        var pdfOption = new PdfOptions()
    //        {
    //            Format = PuppeteerSharp.Media.PaperFormat.A4,

    //            //*****
    //            OmitBackground = false,
    //            PrintBackground = true
    //        };
    //        content = ReplaceProductWithData(content, data);
    //        return await GeneratePdfAsync(content, pdfOption);
    //    }
    //    private static string ReplaceProductWithData(string content, List<HistoryRequest> data)
    //    {
    //        //List<HistoryRequest> data
    //        var products = new List<HistoryRequest>()
    //        {
    //            new() { FromAmount=20,FromCoin = "USD", ToAmount =30, ToCoin="EUR", Date=DateTime.Now,Email="adrian@gmail.com" },
    //            new() { FromAmount=30,FromCoin = "LIB", ToAmount =90, ToCoin="USD", Date=DateTime.Now,Email="adrian@gmail.com" },
    //            new() { FromAmount=560,FromCoin = "EUR", ToAmount =730, ToCoin="USD", Date=DateTime.Now,Email="adrian@gmail.com" },

    //        };
    //        var content1 = new StringBuilder();
    //        int no = 1;
    //        foreach (var product in data)
    //        {
    //            content1.Append("<tr>");
    //            content1.Append("<td>" + (no++) + "</td>");
    //            content1.Append("<td>" + product.FromCoin + "</td>");
    //            content1.Append("<td>" + product.ToCoin + "</td>");
    //            content1.Append("<td>" + product.Date + "</td>");
    //            content1.Append("<td>" + product.Email + "</td>");
    //            content1.Append("</tr>");
    //        }
    //        content = content.Replace("{{content}}", content1.ToString());
    //        return content;
    //    }

    //    public static byte[] ToByteArray(Stream input)
    //    {
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            input.CopyTo(ms);
    //            return ms.ToArray();
    //        }
    //    }
    //}


    // public class PdfHelper
    // {
    //     private static Browser? browser;
    //     private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);
    //
    //     // Ruta del archivo ZIP preexistente (ajusta según tu estructura de proyecto)
    //     private static readonly string ChromiumZipPath = Path.Combine(Directory.GetCurrentDirectory(), "chrome-win64.zip");
    //
    //     // Carpeta donde se descomprimirá Chromium
    //     private static readonly string ChromiumExtractedPath = Path.Combine(Directory.GetCurrentDirectory(), "chrome-win64");
    //
    //     /// <summary>
    //     /// Descomprime Chromium desde el ZIP si no está ya descomprimido.
    //     /// </summary>
    //     private static string PrepareChromium()
    //     {
    //         // Si la carpeta descomprimida ya existe, no hacemos nada
    //         if (!Directory.Exists(ChromiumExtractedPath))
    //         {
    //             Console.WriteLine("Descomprimiendo Chromium...");
    //             System.IO.Compression.ZipFile.ExtractToDirectory(ChromiumZipPath, ChromiumExtractedPath);
    //             Console.WriteLine("Chromium descomprimido.");
    //         }
    //
    //         // Devuelve la ruta al ejecutable
    //         return Path.Combine(ChromiumExtractedPath, "chrome.exe");
    //     }
    //
    //     /// <summary>
    //     /// Devuelve una instancia de Puppeteer Browser, reutilizando si ya existe.
    //     /// </summary>
    //     public static async Task<Browser> GetBrowserAsync()
    //     {
    //         if (browser == null)
    //         {
    //             await semaphoreSlim.WaitAsync();
    //             try
    //             {
    //                 if (browser == null)
    //                 {
    //                     var chromiumPath = PrepareChromium(); // Asegura que Chromium está descomprimido
    //
    //                     browser = (Browser)await Puppeteer.LaunchAsync(new LaunchOptions
    //                     {
    //                         Headless = true,
    //                         ExecutablePath = chromiumPath, // Usa el ejecutable de la carpeta descomprimida
    //                         Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
    //                     });
    //
    //                 }
    //             }
    //             finally
    //             {
    //                 semaphoreSlim.Release();
    //             }
    //         }
    //         return browser;
    //     }
    //
    //     /// <summary>
    //     /// Genera un PDF basado en contenido HTML.
    //     /// </summary>
    //     private static async Task<byte[]> GeneratePdfAsync(string content, PdfOptions pdfOptions)
    //     {
    //         var browser1 = await GetBrowserAsync();
    //         await using var page = await browser1.NewPageAsync();
    //         await page.SetContentAsync(content);
    //
    //         var pdfStream = await page.PdfStreamAsync(pdfOptions);
    //         var result = ToByteArray(pdfStream);
    //         pdfStream.Dispose();
    //
    //         return result;
    //     }
    //
    //     /// <summary>
    //     /// Genera un PDF con datos específicos.
    //     /// </summary>
    //     public static async Task<byte[]> GetProduct(List<HistoryRequest> data)
    //     {
    //         var content = await System.IO.File.ReadAllTextAsync(Path.Combine("HtmlTemplates", "History.html"));
    //         var pdfOption = new PdfOptions()
    //         {
    //             Format = PuppeteerSharp.Media.PaperFormat.A4,
    //             OmitBackground = false,
    //             PrintBackground = true
    //         };
    //
    //         content = ReplaceProductWithData(content, data);
    //         return await GeneratePdfAsync(content, pdfOption);
    //     }
    //
    //     /// <summary>
    //     /// Reemplaza datos dinámicos en la plantilla HTML.
    //     /// </summary>
    //     private static string ReplaceProductWithData(string content, List<HistoryRequest> data)
    //     {
    //         var content1 = new StringBuilder();
    //         int no = 1;
    //
    //         foreach (var product in data)
    //         {
    //             content1.Append("<tr>");
    //             content1.Append("<td>" + (no++) + "</td>");
    //             content1.Append("<td>" + product.FromCoin + "</td>");
    //             content1.Append("<td>" + product.ToCoin + "</td>");
    //             content1.Append("<td>" + product.Date + "</td>");
    //             content1.Append("<td>" + product.Email + "</td>");
    //             content1.Append("</tr>");
    //         }
    //
    //         content = content.Replace("{{content}}", content1.ToString());
    //         return content;
    //     }
    //
    //     /// <summary>
    //     /// Convierte un Stream en un array de bytes.
    //     /// </summary>
    //     public static byte[] ToByteArray(Stream input)
    //     {
    //         using (MemoryStream ms = new MemoryStream())
    //         {
    //             input.CopyTo(ms);
    //             return ms.ToArray();
    //         }
    //     }
    // }

    public class PdfHelper
    {
        private static Browser? browser;
        private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);

        // Ruta donde está descomprimido Chromium (ajusta si es necesario)
        private static readonly string ChromiumPath =
            Path.Combine(Directory.GetCurrentDirectory(), "chrome-win64", "chrome.exe");

        /// <summary>
        /// Devuelve una instancia de Puppeteer Browser, reutilizando si ya existe.
        /// </summary>
        public static async Task<Browser> GetBrowserAsync()
        {
            if (browser == null)
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    if (browser == null)
                    {
                        // Verifica que el ejecutable exista
                        if (!File.Exists(ChromiumPath))
                        {
                            throw new FileNotFoundException(
                                $"No se encontró el ejecutable de Chrome en la ruta: {ChromiumPath}");
                        }

                        // Configura Puppeteer para usar el ejecutable de Chromium
                        browser = (Browser)await Puppeteer.LaunchAsync(new LaunchOptions
                        {
                            Headless = true,
                            ExecutablePath = ChromiumPath,
                            Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
                        });
                    }
                }
                finally
                {
                    semaphoreSlim.Release();
                }
            }

            return browser;
        }

        /// <summary>
        /// Genera un PDF basado en contenido HTML.
        /// </summary>
        private static async Task<byte[]> GeneratePdfAsync(string content, PdfOptions pdfOptions)
        {
            var browser1 = await GetBrowserAsync();
            await using var page = await browser1.NewPageAsync();
            await page.SetContentAsync(content);

            var pdfStream = await page.PdfStreamAsync(pdfOptions);
            var result = ToByteArray(pdfStream);
            pdfStream.Dispose();

            return result;
        }

        /// <summary>
        /// Genera un PDF con datos específicos.
        /// </summary>
        public static async Task<byte[]> GetProduct(List<HistoryRequest> data)
        {
            var content = await System.IO.File.ReadAllTextAsync(Path.Combine("HtmlTemplates", "History.html"));
            var pdfOption = new PdfOptions()
            {
                Format = PuppeteerSharp.Media.PaperFormat.A4,
                OmitBackground = false,
                PrintBackground = true
            };

            content = ReplaceProductWithData(content, data);
            return await GeneratePdfAsync(content, pdfOption);
        }

        /// <summary>
        /// Reemplaza datos dinámicos en la plantilla HTML.
        /// </summary>
        private static string ReplaceProductWithData(string content, List<HistoryRequest> data)
        {
            var content1 = new StringBuilder();
            int no = 1;

            foreach (var product in data)
            {
                content1.Append("<tr>");
                content1.Append("<td>" + (no++) + "</td>");
                content1.Append("<td>" + product.FromCoin + "</td>");
                content1.Append("<td>" + product.ToCoin + "</td>");
                content1.Append("<td>" + product.Date + "</td>");
                content1.Append("<td>" + product.Email + "</td>");
                content1.Append("</tr>");
            }

            content = content.Replace("{{content}}", content1.ToString());
            return content;
        }

        /// <summary>
        /// Convierte un Stream en un array de bytes.
        /// </summary>
        public static byte[] ToByteArray(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }



}
