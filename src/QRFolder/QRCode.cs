using System;
using System.IO;
using System.Text;
using Net.Codecrete.QrCodeGenerator;

namespace QuickRecipes.WebSite.QRFolder
{
    public class QRCode
    {
        public QRCode()
        {
            var qr = QrCode.EncodeText("Hello, world!", QrCode.Ecc.Medium);
            string svg = qr.ToSvgString(4);
            File.WriteAllText("hello-world-qr.svg", svg, Encoding.UTF8);
        }
    }
}

