using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace pdf_merge;

public class FileMerger {
    public static void MergeFiles(List<string> files, string outputName, bool enableVerbose) {
        // Create Output PDF document
        PdfDocument outPdf = new PdfDocument();

        // Copy all files to output document
        for (int i = 0; i < files.Count; i++) {
            PdfDocument pdf = PdfReader.Open(files[i], PdfDocumentOpenMode.Import);
            CopyPages(pdf, outPdf);

            ConsoleOutput.ProgressBar(i + 1, files.Count, "Merging PDF files  ");
        }

        // Save document
        outPdf.Save(outputName + ".pdf");
        if (enableVerbose) ConsoleOutput.Success($"PDF files merged and saved as {outputName + ".pdf"}");
    }

    private static void CopyPages(PdfDocument source, PdfDocument target) {
        // Copy each page in the source file to the target file
        for (int i = 0; i < source.PageCount; i++) {
            target.AddPage(source.Pages[i]);
        }
    }

}
