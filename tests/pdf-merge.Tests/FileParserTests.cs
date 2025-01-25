namespace pdf_merge.Tests;

public class FileParserTests {
    /// <summary>
    /// Path to the TestData directory containing dummy .pdf and .txt files
    /// </summary>
    private string path = TestContext.CurrentContext.TestDirectory + "/TestData";

    [Test]
    public void ValidDirectoryDoesNotThrowException() {
        List<string> testList = new List<string>();
        Assert.DoesNotThrow(() => FileParser.FilesFromDirectory(path, ref testList));
    }

    [Test]
    public void InvalidDirectoryThrowsException() {
        List<string> testList = new List<string>();
        Assert.Throws<IOException>(() => FileParser.FilesFromDirectory(path + "/PathThatDoesNotExist", ref testList));
    }

    [Test]
    public void OnlyPDFFilesAreReturned() {
        List<string> testList = new List<string>();
        FileParser.FilesFromDirectory(path, ref testList);

        var result = testList.Select(x => Path.GetFileName(x));
        List<string> expected = ["file1.pdf", "file2.pdf", "file3.pdf"];

        Assert.That(expected.SequenceEqual(result));
    }

}
