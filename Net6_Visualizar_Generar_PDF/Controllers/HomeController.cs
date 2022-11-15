using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Net6_Visualizar_Generar_PDF.Models;
using System.Diagnostics;

namespace Net6_Visualizar_Generar_PDF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConverter _converter;

        public HomeController(ILogger<HomeController> logger, IConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult VistaParaPDF()
        {
            return View();
        }


        public IActionResult MostrarPDFenPagina()
        {
            string pagina_actual = HttpContext.Request.Path; // Home/MostrarPDFenPagina
            string url_pagina = HttpContext.Request.GetEncodedUrl(); // URL
            url_pagina = url_pagina.Replace(pagina_actual, "");
            url_pagina = $"{url_pagina}/Home/VistaParaPDF";

            var pdf = new HtmlToPdfDocument() {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait

                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page = url_pagina
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf); 

            return File(archivoPDF, "application/pdf");
        }


        public IActionResult DescargarPDF()
        {
            string pagina_actual = HttpContext.Request.Path; // Home/MostrarPDFenPagina
            string url_pagina = HttpContext.Request.GetEncodedUrl(); // URL
            url_pagina = url_pagina.Replace(pagina_actual, "");
            url_pagina = $"{url_pagina}/Home/VistaParaPDF";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait

                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page = url_pagina
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);
            string nombrePdf = "Pruebas_PDF_" + DateTime.Now +".pdf";

            return File(archivoPDF, "application/pdf", nombrePdf);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}