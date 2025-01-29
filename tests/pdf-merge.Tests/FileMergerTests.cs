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

        _ = Setup();
        FileMerger.MergeFiles(filePaths, "TestOutput", false);
        Restore();

        Assert.That(pageCount.Equals(PdfReader.Open("TestOutput.pdf", PdfDocumentOpenMode.Import).PageCount));
    }

    /// <summary>
    /// Set up Console to output to StringWriter object and not Std Out
    /// </summary>
    /// <returns>StringWriter object that console has been set to write to</returns>
    private StringWriter Setup() {
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        return sw;
    }

    /// <summary>
    /// Restores Console to output to output to Std Out
    /// </summary>
    private void Restore() {
        var stdOut = new StreamWriter(Console.OpenStandardOutput());
        stdOut.AutoFlush = true;
        Console.SetOut(stdOut);
    }
}