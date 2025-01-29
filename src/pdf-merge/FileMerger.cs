using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace pdf_merge;

public class FileMerger {

    /// <summary>
    /// Merges a list of PDF files into a single PDF file with the name "<outputName>.pdf". 
    /// All the specified files must exist, otherwise this method will throw an IOException.
    /// </summary>
    /// <param name="files">The list of paths for each of the PDF files to merge</param>
    /// <param name="outputName">Name of merged PDF file</param>
    /// <param name="enableVerbose">Enables verbose mode to print extra messages to console</param>
    /// <exception cref="IOException"></exception>
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

    /// <summary>
    /// Copies all pages from the `source` PDF document to the end of the `target` document in order
    /// </summary>
    /// <param name="source">PDF document to copy pages from</param>
    /// <param name="target">PDF document to copy pages to</param>
    private static void CopyPages(PdfDocument source, PdfDocument target) {
        // Copy each page in the source file to the target file
        for (int i = 0; i < source.PageCount; i++) {
            target.AddPage(source.Pages[i]);
        }
    }

}
