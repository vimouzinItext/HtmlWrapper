using iText.Html2pdf;
using iText.IO.Font;
using iText.IO.Source;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.License;
using iTextSharp.tool.xml.xtra.xfa;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp15
{
    class Program
    {
        static String inputFile = "C:\\Users\\Vitor.Mouzinho\\IdeaProjects\\GettingStarted\\Test\\src\\main\\java\\HebrewAddPage\\input for Vitor.pdf";
        static String outputFile = "C:\\Users\\Vitor.Mouzinho\\IdeaProjects\\GettingStarted\\Test\\src\\main\\java\\HebrewAddPage\\test.pdf";
        static String fontFile = "C:\\Users\\Vitor.Mouzinho\\IdeaProjects\\GettingStarted\\Test\\src\\main\\java\\HebrewAddPage\\test.ttf";
        static String pathToLicense = "C:\\Users\\Vitor.Mouzinho\\IdeaProjects\\GettingStarted\\Test\\Vitor_iText_Developer_key.xml";
        static String inputFilePath = "C:\\Users\\Vitor.Mouzinho\\IdeaProjects\\GettingStarted\\Test\\src\\main\\java\\MetaDataInfo\\021072700070689191_001_1 (1).pdf";
        static String outputPdf = "C:\\Users\\Vitor.Mouzinho\\IdeaProjects\\GettingStarted\\Test\\src\\main\\java\\BrokenTable\\csharp.pdf";
        static String inputHtml = "C:\\Users\\Vitor.Mouzinho\\IdeaProjects\\GettingStarted\\Test\\src\\main\\java\\BrokenTable\\pdf (3).html";
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
