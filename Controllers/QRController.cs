using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    public class QRController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string kod)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator kodUret = new QRCodeGenerator();
                QRCodeGenerator.QRCode kareKod = kodUret.CreateQrCode(kod, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap resim = kareKod.GetGraphic(10))
                {
                    resim.Save(memoryStream, ImageFormat.Png);
                    ViewBag.kareKodResim = "data:image/png;base64,"
                        + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View();
        }
    }
}