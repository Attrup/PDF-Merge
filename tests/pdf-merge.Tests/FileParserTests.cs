namespace pdf_merge.Tests;

public class FileParserTests {
    /// <summary>
    /// Path to the TestData directory containing dummy .pdf and .txt files
    /// </summary>
    private string testDir = "TestData";

    // FilesFromDirectory Tests
    [Test]
    public void ValidDirectoryDoesNotThrowException() {
        List<string> testList = new List<string>();
        Assert.DoesNotThrow(() => FileParser.FilesFromDirectory(testDir, ref testList, false));
    }

    [Test]
    public void InvalidDirectoryThrowsException() {
        List<string> testList = new List<string>();
        Assert.Throws<IOException>(() => FileParser.FilesFromDirectory(testDir + "/PathThatDoesNotExist", ref testList, false));
    }

    [Test]
    public void OnlyPDFFilesAreReturned() {
        List<string> testList = new List<string>();
        FileParser.FilesFromDirectory(testDir, ref testList, false);

        var result = testList.Select(x => Path.GetFileName(x));
        List<string> expected = ["file1.pdf", "file2.pdf", "file3.pdf"];

        Assert.That(expected.SequenceEqual(result));
    }

    // FilesFromList Tests
    [Test]
    public void NonPDFFileThrowsException() {
        List<string> testList = new List<string>();
        List<string> input = ["file4.txt"];

        Assert.Throws<IOException>(() => FileParser.FilesFromList(input, ref testList, false));
    }

    [Test]
    public void InvalidFilePathThrowsException() {
        List<string> testList = new List<string>();
        List<string> input = ["NonExistingFile.pdf"];

        Assert.Throws<IOException>(() => FileParser.FilesFromList(input, ref testList, false));
    }

    [Test]
    public void ValidFilesAreReturned() {
        List<string> testList = new List<string>();
        List<string> input = [testDir + "/file2.pdf", testDir + "/file1.pdf"];

        FileParser.FilesFromList(input, ref testList, false);

        var result = testList.Select(x => Path.GetFileName(x));
        List<string> expected = ["file2.pdf", "file1.pdf"];

        Assert.That(expected.SequenceEqual(result));
    }

}