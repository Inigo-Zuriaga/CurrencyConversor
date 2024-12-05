using PuppeteerSharp;
using System.Text;

namespace WebConversor.Helpers
{
    public class PdfHelper
    {
        private static Browser? browser;
        private static readonly BrowserFetcher fetcher = new();
        private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);

        public static async Task<Browser> GetBrowserAsync()
        {
            if (browser == null)
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    if (browser == null)
                    {
                        await fetcher.DownloadAsync();
                        browser = (Browser)await Puppeteer.LaunchAsync(new LaunchOptions
                        {
                            Headless = true,
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

        public static async Task<byte[]> GetProduct(List<HistoryRequest> data)
        {
            var content = await System.IO.File.ReadAllTextAsync(Path.Combine("HtmlTemplates", "History.html"));
            var pdfOption = new PdfOptions()
            {
                Format = PuppeteerSharp.Media.PaperFormat.A4,
                
                //*****
                OmitBackground = false,
                PrintBackground = true
            };
            content = ReplaceProductWithData(content, data);
            return await GeneratePdfAsync(content, pdfOption);
        }
        private static string ReplaceProductWithData(string content, List<HistoryRequest> data)
        {
            //List<HistoryRequest> data
            var products = new List<HistoryRequest>()
            {
                new() { FromAmount=20,FromCoin = "USD", ToAmount =30, ToCoin="EUR", Date=DateTime.Now,Email="adrian@gmail.com" },
                new() { FromAmount=30,FromCoin = "LIB", ToAmount =90, ToCoin="USD", Date=DateTime.Now,Email="adrian@gmail.com" },
                new() { FromAmount=560,FromCoin = "EUR", ToAmount =730, ToCoin="USD", Date=DateTime.Now,Email="adrian@gmail.com" },
                
            };
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
