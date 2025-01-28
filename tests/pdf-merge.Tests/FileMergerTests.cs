using NUnit.Framework;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace pdf_merge.Tests;

public class FileMergerTests {
    [Test]
    public void MergeFilesCopiesAllPages() {
        List<string> filePaths = ["TestData/file1.pdf", "TestData/file2.pdf"];

        int pageCount = 0;
        foreach (string file in filePaths) {
            pageCount += PdfReader.Open(file, PdfDocumentOpenMode.Import).PageCount;
        }

        FileMerger.MergeFiles(filePaths, "TestOutput", false);

        Assert.That(pageCount.Equals(PdfReader.Open("TestOutput.pdf", PdfDocumentOpenMode.Import).PageCount));
    }
}