using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace pdf_merge;

public class FileMerger {
    public static void MergeFiles(List<string> files, string outputName, bool verbose) {
        // Create Output PDF document
        PdfDocument outPdf = new PdfDocument();

        // Copy all files to output document
        foreach (string file in files) {
            PdfDocument pdf = PdfReader.Open(file, PdfDocumentOpenMode.Import);

            CopyPages(pdf, outPdf);
        }

        // Save document
        outPdf.Save(outputName + ".pdf");
    }

    private static void CopyPages(PdfDocument source, PdfDocument target) {
        // Copy each page in the source file to the target file
        for (int i = 0; i < source.PageCount; i++) {
            target.AddPage(source.Pages[i]);
        }
    }

}
