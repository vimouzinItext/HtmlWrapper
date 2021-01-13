
import com.itextpdf.html2pdf.HtmlConverter;
import com.itextpdf.kernel.geom.PageSize;
import com.itextpdf.kernel.pdf.PdfDocument;
import com.itextpdf.kernel.pdf.PdfPage;
import com.itextpdf.kernel.pdf.PdfReader;
import com.itextpdf.kernel.pdf.PdfWriter;
import com.itextpdf.kernel.pdf.canvas.PdfCanvas;
import com.itextpdf.kernel.pdf.xobject.PdfFormXObject;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.FileInputStream;
import java.io.IOException;

public class Resize {
    static String inputHtml ="inputHtml.html";
    static String outputPdf = "outputPdf.pdf";
    public static void main(String[] args) throws IOException {
        ByteArrayOutputStream ms = new ByteArrayOutputStream();
        PdfDocument pdfDocument = new PdfDocument(new PdfWriter(ms));
        pdfDocument.setDefaultPageSize(PageSize.A3);
        HtmlConverter.convertToPdf(new FileInputStream(inputHtml),pdfDocument);
        PdfDocument resultantDocument = new PdfDocument(new PdfWriter(outputPdf));
        resultantDocument.setDefaultPageSize(PageSize.A4);
        pdfDocument = new PdfDocument(new PdfReader(new ByteArrayInputStream(ms.toByteArray())));
        for (int i = 1; i <= pdfDocument.getNumberOfPages(); i++) {
            PdfPage page = pdfDocument.getPage(i);
            PdfFormXObject formXObject = page.copyAsFormXObject(resultantDocument);
            PdfCanvas pdfCanvas = new PdfCanvas(resultantDocument.addNewPage());
            pdfCanvas.addXObject(formXObject, 0.4f, 0, 0, 0.4f, 6, 350);
        }
        pdfDocument.close();
        resultantDocument.close();
    }
}
