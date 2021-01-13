using iText.Html2pdf;

using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using System;
using System.IO;

namespace ConsoleApp15
{
    class Program
    {
        static String outputPdf = "outputPdfFile.pdf";
        static String inputHtml = "inputHtml.html";
        static void Main(string[] args)
        {
            MemoryStream ms = new MemoryStream();
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(ms));
            pdfDocument.SetDefaultPageSize(PageSize.A3);
            using (FileStream htmlSource = File.Open(inputHtml, FileMode.Open))
            using (FileStream pdfDest = File.Open(outputPdf, FileMode.OpenOrCreate))
            {
                ConverterProperties converterProperties = new ConverterProperties();
                HtmlConverter.ConvertToPdf(htmlSource, pdfDocument,converterProperties);
            }
            PdfDocument resultantDocument = new PdfDocument(new PdfWriter(outputPdf));
            resultantDocument.SetDefaultPageSize(PageSize.A4);
            MemoryStream k = new MemoryStream(ms.ToArray());
            pdfDocument = new PdfDocument(new PdfReader(k));
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
            {
                PdfPage page = pdfDocument.GetPage(i);
                PdfFormXObject formXObject = page.CopyAsFormXObject(resultantDocument);
                PdfCanvas pdfCanvas = new PdfCanvas(resultantDocument.AddNewPage());
                // 3a and 3b
                pdfCanvas.AddXObject(formXObject, 0.4f, 0, 0, 0.4f, 6, 350);
            }
            pdfDocument.Close();
            resultantDocument.Close();
        }
    }
}
